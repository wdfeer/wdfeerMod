using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using System;
using System.Collections.Generic;

namespace wfMod
{
    public class wfGlobalNPC : GlobalNPC
    {
        public override bool InstancePerEntity => true;
        public List<StackableProc> procs = new List<StackableProc>();
        public void AddStackableProc(ProcType type, int duration, int damage)
        {
            StackableProc proc = new StackableProc(type, damage, null, duration);
            proc.OnEnd = () => procs.Remove(proc);
            procs.Add(proc);
        }
        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            if ((npc.HasBuff(BuffID.Electrified) || npc.HasBuff(mod.BuffType("SlashProc"))))
                npc.lifeRegen = 0;
            else procs = new List<StackableProc>();
            if (npc.HasBuff(BuffID.Electrified))
            {
                int totalDamage = 0;
                for (int i = 0; i < procs.Count; i++)
                    totalDamage += procs[i].type == ProcType.Electricity ? procs[i].dmg : 0;
                npc.lifeRegen -= totalDamage;
            }
            if (npc.HasBuff(mod.BuffType("SlashProc")))
            {
                int totalDamage = 0;
                for (int i = 0; i < procs.Count; i++)
                    totalDamage += procs[i].type == ProcType.Slash ? procs[i].dmg : 0;
                npc.lifeRegen -= totalDamage;
                if (npc.lifeRegenExpectedLossPerSecond < totalDamage)
                    npc.lifeRegenExpectedLossPerSecond = totalDamage;
            }
        }
        public override void AI(NPC npc)
        {
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
                for (int i = 0; i < procs.Count; i++)
                    procs[i].Update();
        }
        public override void ModifyHitByProjectile(NPC npc, Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (Main.player[projectile.owner].GetModPlayer<wfPlayer>().synthDeconstruct && projectile.minion)
                heartDropChance = 15;
        }
        public int[] martianTypes = { NPCID.MartianDrone, NPCID.MartianEngineer, NPCID.MartianSaucerCore, NPCID.MartianTurret, NPCID.MartianOfficer, NPCID.MartianWalker };
        public int[] goblins = { NPCID.GoblinArcher, NPCID.GoblinSorcerer, NPCID.GoblinWarrior, NPCID.GoblinThief, NPCID.GoblinSummoner };
        public int heartDropChance = 0; //Percent
        public override void NPCLoot(NPC npc)
        {
            if (npc.SpawnedFromStatue) return;

            switch (npc.type)
            {
                case NPCID.DemonEye when wfMod.Roll(0.75f):
                    Item.NewItem(npc.getRect(), ModContent.ItemType<Items.Accessories.PiercingHit>());
                    break;
                case NPCID.BigMimicCorruption when wfMod.Roll(20):
                    Item.NewItem(npc.getRect(), ModContent.ItemType<Items.Accessories.ArgonScope>());
                    break;
                case NPCID.BigMimicCrimson when wfMod.Roll(20):
                    Item.NewItem(npc.getRect(), ModContent.ItemType<Items.Accessories.ArgonScope>());
                    break;
                case NPCID.FireImp when Main.rand.Next(100) < 6:
                    Item.NewItem(npc.getRect(), ModContent.ItemType<Items.Accessories.Blaze>());
                    break;
                case NPCID.BrainofCthulhu when Main.rand.Next(100) < 33:
                    Item.NewItem(npc.getRect(), ModContent.ItemType<Items.Weapons.GorgonWraith>());
                    break;
                case NPCID.QueenBee when Main.rand.Next(100) < 33:
                    Item.NewItem(npc.getRect(), ModContent.ItemType<Items.Accessories.Shred>());
                    break;
                case NPCID.SkeletronHead when Main.rand.Next(100) < 40:
                    int rand = Main.rand.Next(5);
                    switch (rand)
                    {
                        case 0:
                            Item.NewItem(npc.getRect(), ModContent.ItemType<Items.Accessories.InternalBleeding>());
                            break;
                        case 1:
                            Item.NewItem(npc.getRect(), ModContent.ItemType<Items.Weapons.Cestra>());
                            break;
                        default:
                            break;
                    }
                    break;
                case NPCID.WallofFlesh when Main.rand.Next(100) < 15:
                    Item.NewItem(npc.getRect(), ModContent.ItemType<Items.Accessories.QuickThinking>());
                    break;
                case NPCID.SkeletronPrime when Main.rand.Next(100) < 25:
                    Item.NewItem(npc.getRect(), ModContent.ItemType<Items.Weapons.SecuraPenta>());
                    break;
                default:
                    if (martianTypes.Contains(npc.type) && Main.rand.Next(100) < 3)
                        Item.NewItem(npc.getRect(), ModContent.ItemType<Items.Fieldron>());
                    else if (goblins.Contains(npc.type) && Main.rand.Next(100) < 4)
                    {
                        rand = Main.rand.Next(3);
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
                    break;
            }

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
            if (npc.boss && Main.expertMode && Main.rand.Next(100) < 15)
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