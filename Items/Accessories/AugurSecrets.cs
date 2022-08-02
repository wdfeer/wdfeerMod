using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace wfMod.Items.Accessories
{
    
    public class AugurSecrets : ExclusiveAccessory
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("+7% magic damage, +5% of mana consumed is converted to shields");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = 2;
            Item.value = Item.buyPrice(gold: 4);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(DamageClass.Magic) += 0.07f;
            var shieldPl = player.GetModPlayer<wfPlayerShields>();
            shieldPl.consumedManaToShieldConversion += 0.05f;
        }
    }
}