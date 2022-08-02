using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using wfMod.Items.Accessories;
using wfMod.Items.Weapons;
using System.IO;

namespace wfMod.Items
{
    public class wfGlobalItem : GlobalItem
    {
        public override bool InstancePerEntity => true;
        public bool energized;
        public wfGlobalItem()
        {
            energized = false;
        }
        public override GlobalItem Clone(Item item, Item itemClone)
        {
            wfGlobalItem globalItem = new wfGlobalItem();
            wfGlobalItem oldGlobalItem = item.GetGlobalItem<wfGlobalItem>();
            globalItem.energized = oldGlobalItem.energized;
            return globalItem;
        }
        public override void NetSend(Item item, BinaryWriter writer)
        {
            writer.Write(energized);
        }
        public override void NetReceive(Item item, BinaryReader reader)
        {
            energized = reader.ReadBoolean();
        }
        public override void SetDefaults(Item item)
        {
            base.SetDefaults(item);
            if (item.type == ItemID.Grenade) item.ammo = item.type;
        }
        public override bool Shoot(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 offset = new Vector2(speedX, -speedY);
            offset *= Main.rand.NextFloat(-player.GetModPlayer<wfPlayer>().spreadMult, player.GetModPlayer<wfPlayer>().spreadMult);
            speedX += offset.X;
            speedY += offset.Y;
            if (Mod != null)
                if (player.HasBuff(Mod.Find<ModBuff>("EnergyConversionBuff").Type) && item.CountsAsClass(DamageClass.Magic))
                {
                    player.DelBuff(player.FindBuffIndex(Mod.Find<ModBuff>("EnergyConversionBuff").Type));
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
    }
}