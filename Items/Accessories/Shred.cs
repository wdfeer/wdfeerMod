using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace wfMod.Items.Accessories
{
    
    public class Shred : ExclusiveAccessory
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("+10% Fire Rate, +1 Projectile Penetration");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = 3;
            Item.value = Item.buyPrice(gold: 2);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<wfPlayer>().fireRateMult += 0.1f;
            player.GetModPlayer<wfPlayer>().penetrate += 1;
        }
    }
}