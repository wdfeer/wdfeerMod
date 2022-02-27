using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;

namespace wfMod.Items
{
    public class wfGlobalItem : GlobalItem
    {
        public override bool InstancePerEntity => true;
        public override bool CloneNewInstances => true;
        public bool energized = false;
        public override void SetDefaults(Item item)
        {
            base.SetDefaults(item);
            if (item.type == ItemID.Grenade) item.ammo = item.type;
        }
        public override bool Shoot(Item item, Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 offset = new Vector2(speedX, -speedY);
            offset *= Main.rand.NextFloat(-player.GetModPlayer<wfPlayer>().spreadMult, player.GetModPlayer<wfPlayer>().spreadMult);
            speedX += offset.X;
            speedY += offset.Y;

            if (player.HasBuff(mod.BuffType("EnergyConversionBuff")) && item.magic)
            {
                player.DelBuff(player.FindBuffIndex(mod.BuffType("EnergyConversionBuff")));
            }

            return base.Shoot(item, player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
        }
        public override void VerticalWingSpeeds(Item item, Player player, ref float ascentWhenFalling, ref float ascentWhenRising, ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
        {
            if (player.GetModPlayer<wfPlayer>().hypeThrusters)
            {
                maxAscentMultiplier *= 1.25f;
                constantAscend *= 1.25f;
            }
        }
        public override void OpenVanillaBag(string context, Player player, int arg)
        {
            switch (arg)
            {
                case ItemID.EaterOfWorldsBossBag when wfMod.Roll(33):
                    DropItem(player, ModContent.ItemType<Weapons.Tetra>());
                    break;
                case ItemID.QueenBeeBossBag when wfMod.Roll(40):
                    DropItem(player, new int[] { ModContent.ItemType<Accessories.Shred>(), ModContent.ItemType<Weapons.Sobek>() });
                    break;
                case ItemID.BrainOfCthulhuBossBag when wfMod.Roll(33):
                    DropItem(player, ModContent.ItemType<Items.Weapons.GorgonWraith>());
                    break;
                case ItemID.SkeletronBossBag when wfMod.Roll(50):
                    DropItem(player, new int[] { ModContent.ItemType<Items.Accessories.InternalBleeding>(), ModContent.ItemType<Items.Weapons.Cestra>() });
                    break;
                case ItemID.WallOfFleshBossBag when wfMod.Roll(33):
                    DropItem(player, new int[] { ModContent.ItemType<Items.Accessories.QuickThinking>(), ModContent.ItemType<Items.Accessories.EnergyConversion>() });
                    break;
                case ItemID.SkeletronPrimeBossBag when wfMod.Roll(25):
                    DropItem(player, ModContent.ItemType<Items.Weapons.SecuraPenta>());
                    break;
                default:
                    break;
            }
        }
        private void DropItem(Player player, int type)
        {
            Item.NewItem(player.getRect(), type);
        }
        private void DropItem(Player player, int[] options)
        {
            int rand = Main.rand.Next(options.Length);
            Item.NewItem(player.getRect(), options[rand]);
        }
    }
}