using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace wfMod.Items.Accessories
{
    
    public class AugurAccord : ExclusiveAccessory
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("+15 Shield, +5% of mana consumed is converted to shields");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = 2;
            Item.value = Item.buyPrice(gold: 2, silver: 20);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            var shieldPl = player.GetModPlayer<wfPlayerShields>();
            shieldPl.maxUnderShield += 15;
            shieldPl.consumedManaToShieldConversion += 0.05f;
        }
    }
}