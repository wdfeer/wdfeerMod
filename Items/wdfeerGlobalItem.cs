using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace wdfeerMod.Items
{
    public class wdfeerGlobalItem : GlobalItem
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
            offset *= Main.rand.NextFloat(-player.GetModPlayer<wdfeerPlayer>().spreadMult, player.GetModPlayer<wdfeerPlayer>().spreadMult);
            speedX += offset.X;
            speedY += offset.Y;
            return base.Shoot(item, player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
        }
        public override void VerticalWingSpeeds(Item item, Player player, ref float ascentWhenFalling, ref float ascentWhenRising, ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
        {
            if (player.GetModPlayer<wdfeerPlayer>().hypeThrusters)
            {
                maxAscentMultiplier *= 1.25f;
                constantAscend *= 1.25f;
            }
        }
    }
}