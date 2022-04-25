using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace wfMod.Items.Accessories
{
    
    public class Vigor : ExclusiveAccessory
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("+15 Life, +15 Shield");
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            item.rare = 3;
            item.value = Item.buyPrice(gold: 3);
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statLifeMax2 += 15;
            var shieldPlayer = player.GetModPlayer<wfPlayerShields>();
            shieldPlayer.maxUnderShield += 15;
        }
    }
}