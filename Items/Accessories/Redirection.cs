using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace wfMod.Items.Accessories
{
    
    public class Redirection : ExclusiveAccessory
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("+10 Max Shields");
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = 2;
            Item.value = Item.buyPrice(silver: 25);
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            var shieldPlayer = player.GetModPlayer<wfPlayerShields>();
            shieldPlayer.maxUnderShield += 10;
        }
    }
}