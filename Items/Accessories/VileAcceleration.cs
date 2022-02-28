using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace wfMod.Items.Accessories
{
    
    public class VileAcceleration : ExclusiveAccessory
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("+20% Fire Rate, but -10% Damage");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.rare = 4;
            item.value = Item.buyPrice(gold: 2);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<wfPlayer>().fireRateMult += 0.2f;
            player.allDamageMult -= 0.1f;
        }
    }
}