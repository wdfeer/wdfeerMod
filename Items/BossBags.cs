using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using wfMod.Items.Accessories;
using wfMod.Items.Weapons;

namespace wfMod.Items
{
    public class BossBags : GlobalItem
    {
        public override void OpenVanillaBag(string context, Player player, int arg)
        {
            if (context != "bossBag") return;
            switch (arg)
            {
                case ItemID.EyeOfCthulhuBossBag when wfMod.Roll(50):
                    DropItem(player, new int[] { ModContent.ItemType<Sobek>(), ModContent.ItemType<Redirection>() });
                    break;
                case ItemID.EaterOfWorldsBossBag when wfMod.Roll(40):
                    DropItem(player, new int[] {ModContent.ItemType<Tetra>(), mod.ItemType("Vigor")});
                    break;
                case ItemID.BrainOfCthulhuBossBag when wfMod.Roll(40):
                    DropItem(player, new int[] { ModContent.ItemType<GorgonWraith>(), mod.ItemType("Vigor")});
                    break;
                case ItemID.QueenBeeBossBag when wfMod.Roll(33):
                    DropItem(player, new int[] { ModContent.ItemType<Shred>(), ModContent.ItemType<Kohm>() });
                    break;
                case ItemID.SkeletronBossBag:
                    DropItem(player, new int[] { ModContent.ItemType<InternalBleeding>(), ModContent.ItemType<Cestra>(), mod.ItemType("Desecrate") });
                    break;
                case ItemID.WallOfFleshBossBag when wfMod.Roll(33):
                    DropItem(player, new int[] { ModContent.ItemType<QuickThinking>(), ModContent.ItemType<EnergyConversion>() });
                    break;
                case ItemID.DestroyerBossBag when wfMod.Roll(33):
                    DropItem(player, ModContent.ItemType<StasisField>());
                    break;
                case ItemID.SkeletronPrimeBossBag when wfMod.Roll(25):
                    DropItem(player, ModContent.ItemType<SecuraPenta>());
                    break;
                case ItemID.TwinsBossBag when wfMod.Roll(15):
                    DropItem(player, ModContent.ItemType<Acceltra>());
                    break;
                default:
                    break;
            }
        }
        private void DropItem(Player player, int type)
        {
            player.QuickSpawnItem(type);
        }
        private void DropItem(Player player, int[] options)
        {
            int rand = Main.rand.Next(options.Length);
            DropItem(player, options[rand]);
        }
    }
}