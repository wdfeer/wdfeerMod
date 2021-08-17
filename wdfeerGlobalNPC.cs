using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using System;

namespace wdfeerMod
{
    public class StackableProc
    {
        public int type; //0 for Slash, 1 for Electro
        public int timeLeft = 300;
        public int dmg = 0;

        public StackableProc(int Type, int damage, int duration = 300)
        {
            type = Type;
            timeLeft = duration;
            dmg = damage;
        }

        public void Update()
        {
            if (dmg == 0) return;
            timeLeft -= 1;
            if (timeLeft <= 0) dmg = 0;
        }
    }
    public class wdfeerGlobalNPC : GlobalNPC
    {
        public override bool InstancePerEntity => true;
        public StackableProc[] procs = new StackableProc[999];
        public int procCounter
        {
            get => ProcCounter;
            set
            {
                if (value < 999) ProcCounter = value;
                else ProcCounter = 0;
            }
        }
        int ProcCounter = 0;
        public void AddStackableProc(string name, int duration, ref int damage)
        {
            switch (name)
            {
                case "slash":
                    procs[procCounter] = new StackableProc(0, damage, duration: duration);
                    procCounter++;
                    break;
                case "electro":
                    procs[procCounter] = new StackableProc(1, damage, duration: duration);
                    procCounter++;
                    break;
                default:
                    break;
            }
        }
        public bool eximus => eximusType != -1;
        public int eximusType = -1;
        public override void SetDefaults(NPC npc)
        {
            base.SetDefaults(npc);
            if (ModContent.GetInstance<wdfeerConfig>().eximusSpawn && !npc.friendly && !BossAlive() && npc.type != NPCID.TargetDummy && Main.rand.Next(100) < 5)
                eximusType = Main.rand.Next(0, 0);
            if (eximus)
            {
                if (npc.life == npc.lifeMax) npc.life = (int)(npc.lifeMax * 1.25f);
                npc.lifeMax = (int)(npc.lifeMax * 1.75f);
                npc.defense = (int)(npc.defense * 1.25f);
                npc.damage = (int)(npc.damage * 2f);
                npc.value *= 3f;
            }
        }
        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            if ((npc.HasBuff(BuffID.Electrified) || npc.HasBuff(mod.BuffType("SlashProc"))))
                npc.lifeRegen = 0;
            else procCounter = 0;
            if (npc.HasBuff(BuffID.Electrified))
            {
                int totalDamage = 0;
                for (int i = 0; i < procCounter; i++)
                    totalDamage += procs[i].type == 1 ? procs[i].dmg : 0;
                npc.lifeRegen -= totalDamage;
            }
            if (npc.HasBuff(mod.BuffType("SlashProc")))
            {
                int totalDamage = 0;
                for (int i = 0; i < procCounter; i++)
                    totalDamage += procs[i].type == 0 ? procs[i].dmg : 0;
                npc.lifeRegen -= totalDamage;
                if (npc.lifeRegenExpectedLossPerSecond < totalDamage)
                    npc.lifeRegenExpectedLossPerSecond = totalDamage;
            }
        }
        public bool BossAlive()
        {
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                if (Main.npc[i].boss && Main.npc[i].active) return true;
            }
            return false;
        }
        public override void AI(NPC npc)
        {
            if (eximusType == 0 && !BossAlive())
            {
                for (int i = 0; i < Main.player.Length; i++)
                {
                    Player player = Main.player[i];
                    if (!player.active || player.dead || Vector2.Distance(npc.position, player.position) > 400) continue;

                    player.manaRegen = 0;
                    if (player.statMana > 1 && Main.rand.Next(0, 100) < 32)
                    {
                        player.statMana -= 1;

                        Vector2 dist = npc.Center - player.Center;
                        for (int i1 = 0; i1 < dist.Length() / 10; i1++)
                        {
                            var dust = Main.dust[Dust.NewDust(player.Center + dist * Main.rand.NextFloat(0, 1), 1, 1, 88)];
                            dust.noGravity = true;
                            dust.velocity *= 0;
                        }
                    }
                }
            }

            if (npc.HasBuff(BuffID.Frozen) && !npc.boss)
            {
                npc.velocity *= 0f;

                for (int i = 0; i < (npc.width < 48 ? 1 : npc.width / 48); i++)
                {
                    int dustIndex = Dust.NewDust(npc.position, npc.width, npc.height, 67, 0f, 0f, 67, default(Color), 1f);
                    var dust = Main.dust[dustIndex];
                    dust.noGravity = true;
                }
            }
            else if (npc.HasBuff(BuffID.Slow) && !npc.boss)
            {
                npc.velocity *= 0.9f;

                for (int i = 0; i < (npc.width < 48 ? 1 : npc.width / 48); i++)
                {
                    int dustIndex = Dust.NewDust(npc.position, npc.width, npc.height, 68, 0f, 0f, 67, default(Color), 0.6f);
                    var dust = Main.dust[dustIndex];
                    dust.velocity *= 0.2f;
                    dust.noGravity = true;
                }
            }

            if (npc.HasBuff(BuffID.Electrified))
            {
                for (int i = 0; i < (npc.width < 32 ? 1 : npc.width / 32); i++)
                {
                    int dustIndex = Dust.NewDust(npc.position, npc.width, npc.height, 226, 0f, 0f, 67, default(Color), 0.5f);
                    var dust = Main.dust[dustIndex];
                    dust.velocity *= 0.6f;
                    dust.noGravity = true;
                }
            }
            if (npc.HasBuff(BuffID.Weak))
            {
                for (int i = 0; i < (npc.width < 48 ? 1 : npc.width / 48); i++)
                {
                    int dustIndex = Dust.NewDust(npc.position, npc.width, npc.height, 1, 0f, 0f, 67, default(Color), 0.35f);
                    var dust = Main.dust[dustIndex];
                    dust.velocity *= 0.2f;
                    dust.noGravity = true;
                }
            }
            if (npc.HasBuff(BuffID.Electrified) || npc.HasBuff(mod.BuffType("SlashProc")))
                for (int i = 0; i < procCounter; i++)
                    procs[i].Update();
        }
        public override void ModifyHitByProjectile(NPC npc, Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (Main.player[projectile.owner].GetModPlayer<wdfeerPlayer>().synthDeconstruct && projectile.minion)
                heartDropChance = 15;
        }
        public int[] martianTypes = { NPCID.MartianDrone, NPCID.MartianEngineer, NPCID.MartianSaucerCore, NPCID.MartianTurret, NPCID.MartianOfficer, NPCID.MartianWalker };
        public int[] goblins = { NPCID.GoblinArcher, NPCID.GoblinSorcerer, NPCID.GoblinWarrior, NPCID.GoblinThief, NPCID.GoblinSummoner };
        public int heartDropChance = 0; //Percent
        public override void NPCLoot(NPC npc)
        {
            if (npc.SpawnedFromStatue) return;

            if (npc.type == NPCID.DemonEye && Main.rand.NextFloat(100) < 0.75f)
                Item.NewItem(npc.getRect(), ModContent.ItemType<Items.Accessories.PiercingHit>());
            else if (npc.type == NPCID.BigMimicCorruption && Main.rand.Next(100) < 20)
                Item.NewItem(npc.getRect(), ModContent.ItemType<Items.Accessories.ArgonScope>());
            else if (npc.type == NPCID.BigMimicCrimson && Main.rand.Next(100) < 20)
                Item.NewItem(npc.getRect(), ModContent.ItemType<Items.Accessories.ArgonScope>());
            else if (npc.type == NPCID.FireImp && Main.rand.Next(100) < 5)
                Item.NewItem(npc.getRect(), ModContent.ItemType<Items.Accessories.Blaze>());
            else if (martianTypes.Contains<int>(npc.type) && Main.rand.Next(100) < 3)
                Item.NewItem(npc.getRect(), ModContent.ItemType<Items.Fieldron>());
            else if (goblins.Contains<int>(npc.type) && Main.rand.Next(100) < 4)
            {
                var rand = Main.rand.Next(3);
                switch (rand)
                {
                    case 0:
                        Item.NewItem(npc.getRect(), ModContent.ItemType<Items.Accessories.HunterMunitions>());
                        break;
                    case 1:
                        Item.NewItem(npc.getRect(), ModContent.ItemType<Items.Weapons.Penta>());
                        break;
                    default:
                        Item.NewItem(npc.getRect(), ModContent.ItemType<Items.Weapons.Tonkor>());
                        break;
                }

            }
            else if (npc.type == NPCID.BrainofCthulhu && Main.rand.Next(100) < 33)
                Item.NewItem(npc.getRect(), ModContent.ItemType<Items.Weapons.GorgonWraith>());
            else if (npc.type == NPCID.QueenBee && Main.rand.Next(100) < 33)
                Item.NewItem(npc.getRect(), ModContent.ItemType<Items.Accessories.Shred>());
            else if (npc.type == NPCID.SkeletronHead && Main.rand.Next(100) < 33)
            {
                int rand = Main.rand.Next(100);
                if (rand < 25)
                    Item.NewItem(npc.getRect(), ModContent.ItemType<Items.Accessories.InternalBleeding>());
                else if (rand < 60)
                    Item.NewItem(npc.getRect(), ModContent.ItemType<Items.Weapons.Cestra>());
            }
            else if (npc.type == NPCID.WallofFlesh && Main.rand.Next(100) < 15)
                Item.NewItem(npc.getRect(), ModContent.ItemType<Items.Accessories.QuickThinking>());
            else if (npc.type == NPCID.SkeletronPrime && Main.rand.Next(100) < 25)
                Item.NewItem(npc.getRect(), ModContent.ItemType<Items.Weapons.SecuraPenta>());

            if (npc.rarity > 0 && !npc.friendly && Main.rand.Next(100) < 33)
            {
                var rand = Main.rand.Next(4);
                switch (rand)
                {
                    case 0:
                        Item.NewItem(npc.getRect(), ModContent.ItemType<Items.Accessories.VileAcceleration>());
                        break;
                    case 1:
                        Item.NewItem(npc.getRect(), ModContent.ItemType<Items.Accessories.CriticalDelay>());
                        break;
                    case 2:
                        Item.NewItem(npc.getRect(), ModContent.ItemType<Items.Accessories.HollowPoint>());
                        break;
                    case 3:
                        Item.NewItem(npc.getRect(), ModContent.ItemType<Items.Accessories.HeavyCaliber>());
                        break;
                }
            }
            if (npc.boss && Main.expertMode && Main.rand.Next(100) < 12)
            {
                var rand = Main.rand.Next(6);
                switch (rand)
                {
                    case 0:
                        Item.NewItem(npc.getRect(), ModContent.ItemType<Items.Accessories.ArcaneAvenger>());
                        break;
                    case 1:
                        Item.NewItem(npc.getRect(), ModContent.ItemType<Items.Accessories.ArcaneGuardian>());
                        break;
                    case 2:
                        Item.NewItem(npc.getRect(), ModContent.ItemType<Items.Accessories.ArcaneAcceleration>());
                        break;
                    case 3:
                        Item.NewItem(npc.getRect(), ModContent.ItemType<Items.Accessories.ArcaneStrike>());
                        break;
                    case 4:
                        Item.NewItem(npc.getRect(), ModContent.ItemType<Items.Accessories.ArcaneEnergize>());
                        break;
                    case 5:
                        Item.NewItem(npc.getRect(), ModContent.ItemType<Items.Accessories.ArcanePulse>());
                        break;
                }
            }

            if (Main.rand.Next(100) < heartDropChance)
                Item.NewItem(npc.getRect(), ItemID.Heart);
        }
    }
}