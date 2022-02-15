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
        public int EximusChance => Main.bloodMoon ? 12 : 8;
        const int energyLeechTimer = 360;
        const int arsonTimer = 1000;
        int ArsonProj = 0;
        Projectile arsonProj => Main.projectile[ArsonProj];
        const int arcticTimer = 750;
        Projectiles.ArcticEximus ArcticEximus => ArcticNPC.modProjectile as Projectiles.ArcticEximus;
        Projectile ArcticNPC;
        int abilityTimer = 0;
        public override void SetDefaults(NPC npc)
        {
            base.SetDefaults(npc);
            if (ModContent.GetInstance<wfConfig>().eximusSpawn && !npc.friendly && !BossAlive() && npc.type != NPCID.TargetDummy && Main.rand.Next(100) < EximusChance)
                type = (EximusType)Main.rand.Next(1, 4);
            if (eximus)
            {
                npc.lifeMax = (int)(npc.lifeMax * 1.75f);
                npc.life = npc.lifeMax;
                npc.defense = (int)(npc.defense * 1.2f);
                npc.damage = (int)(npc.damage * 2f);
                npc.value *= 4f;
            }
        }
        public override void AI(NPC npc)
        {
            if (!eximus || BossAlive() || !npc.HasPlayerTarget || npc.life <= 0)
                return;

            int DeltaAbilityTimer = npc.lifeMax / npc.life;
            if (DeltaAbilityTimer > 5)
                DeltaAbilityTimer = 5;
            switch (type)
            {
                case EximusType.EnergyLeech:
                    abilityTimer++;
                    if (abilityTimer > energyLeechTimer)
                        abilityTimer = 0;

                    if (abilityTimer < energyLeechTimer / 3)
                        break;
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
                    if ((ArcticNPC == null || !ArcticNPC.active || ArcticNPC.type != mod.ProjectileType("ArcticEximus")) && abilityTimer > arcticTimer)
                    {
                        abilityTimer = 0;
                        ArcticNPC = Main.projectile[Projectile.NewProjectile(npc.Center, Vector2.Zero, mod.ProjectileType("ArcticEximus"), 1000, 1, ai0: npc.whoAmI)];
                        ArcticEximus.parentNPC = npc;
                    }
                    break;
                default:
                    break;
            }
        }
        public override void HitEffect(NPC npc, int hitDirection, double damage)
        {
            if (type == EximusType.EnergyLeech && damage > 0)
                abilityTimer = 0;
        }
        public override void ModifyHitByProjectile(NPC npc, Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (ArcticNPC != null && ArcticNPC.active && ArcticNPC.type == mod.ProjectileType("ArcticEximus")) damage /= 4;
        }
    }
}
