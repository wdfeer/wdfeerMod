using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace wfMod.NPCs
{
    public class EximusGlobalNPC : GlobalNPC
    {
        public bool eximus => type != EximusType.None;
        public EximusType type = EximusType.None;
        public override bool InstancePerEntity => true;
        public static bool BossAlive()
        {
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                if (Main.npc[i].boss && Main.npc[i].active) return true;
            }
            return false;
        }
        const int arsonTimer = 1000;
        int ArsonProj = 0;
        Projectile arsonProj => Main.projectile[ArsonProj];
        const int arcticTimer = 750;
        int ArcticNPC;
        NPC arcticNPC => Main.npc[ArcticNPC];
        int abilityTimer = 0;
        public override void SetDefaults(NPC npc)
        {
            base.SetDefaults(npc);

            if (ModContent.GetInstance<wfConfig>().eximusSpawn && !npc.friendly && !BossAlive() && npc.type != NPCID.TargetDummy && !(npc.modNPC is NPCs.ArcticEximus) && Main.rand.Next(100) < 7)
                type = (EximusType)Main.rand.Next(1, 4);
            if (eximus)
            {
                npc.lifeMax = (int)(npc.lifeMax * 1.75f);
                npc.life = npc.lifeMax;
                npc.defense = (int)(npc.defense * 1.2f);
                npc.damage = (int)(npc.damage * 2f);
                npc.value *= 3f;
            }
        }
        public override void AI(NPC npc)
        {
            if (eximus && !BossAlive())
            {
                int DeltaAbilityTimer = npc.lifeMax / npc.life;
                if (DeltaAbilityTimer > 4)
                    DeltaAbilityTimer = 4;
                switch (type)
                {
                    case EximusType.EnergyLeech:
                        if (!npc.HasPlayerTarget) break;
                        Player player = Main.player[npc.FindClosestPlayer()];
                        if (Vector2.Distance(player.position, npc.position) > 640) break;
                        player.manaRegen = 0;
                        if (player.statMana > 1)
                        {
                            player.statMana -= 1;
                            Vector2 dist = npc.Center - player.Center;
                            for (int i1 = 0; i1 < dist.Length() / 15; i1++)
                            {
                                var dust = Main.dust[Dust.NewDust(player.Center + dist * Main.rand.NextFloat(0, 1), 1, 1, 88)];
                                dust.noGravity = true;
                                dust.velocity *= 0;
                            }
                        }
                        break;
                    case EximusType.Arson:
                        abilityTimer += DeltaAbilityTimer;
                        if (!npc.HasPlayerTarget) break;
                        if (abilityTimer >= arsonTimer)
                        {
                            abilityTimer = 0;
                            ArsonProj = Projectile.NewProjectile(npc.Center, Vector2.Zero, mod.ProjectileType("ArsonEximusProj"), Main.hardMode ? 10 : 2, 10f, Owner: npc.whoAmI);
                            Main.PlaySound(SoundID.Item20, npc.Center);
                            npc.velocity *= 0.2f;
                        }
                        break;
                    case EximusType.Arctic:
                        abilityTimer += DeltaAbilityTimer;
                        if (!npc.HasPlayerTarget) break;
                        if ((arcticNPC == null || !arcticNPC.active || arcticNPC.type != ModContent.NPCType<NPCs.ArcticEximus>()) && abilityTimer > arcticTimer)
                        {
                            abilityTimer = 0;

                            ArcticNPC = NPC.NewNPC((int)npc.position.X, (int)npc.position.Y, ModContent.NPCType<NPCs.ArcticEximus>());
                            arcticNPC.lifeMax = npc.lifeMax;
                            arcticNPC.life = npc.lifeMax;
                            arcticNPC.defense = (int)(npc.defense * 1.1f);
                            var arcticModNPC = arcticNPC.modNPC as NPCs.ArcticEximus;
                            arcticModNPC.parentNPC = npc;
                        }
                        break;
                    default:
                        break;
                }
            }
        }
        public override void ModifyHitByProjectile(NPC npc, Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (arcticNPC != null && arcticNPC.active && arcticNPC.type == ModContent.NPCType<NPCs.ArcticEximus>()) damage /= 4;
        }
    }
}
