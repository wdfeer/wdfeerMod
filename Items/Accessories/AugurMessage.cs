using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace wfMod.Items.Accessories
{
    
    public class AugurMessage : ExclusiveAccessory
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("+24% projectile lifetime, +5% of mana consumed is converted to shields");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = 2;
            Item.value = Item.buyPrice(gold: 2);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            var wfPlayer = player.GetModPlayer<wfPlayer>();
            wfPlayer.projLifetime += 0.24f;
            var shieldPl = player.GetModPlayer<wfPlayerShields>();
            shieldPl.consumedManaToShieldConversion += 0.05f;
        }
    }
}