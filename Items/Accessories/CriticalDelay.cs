using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace wdfeerMod.Items.Accessories
{
    // Here we add our accessories, note that they inherit from ExclusiveAccessory, and not ModItem

    public class CriticalDelay : ExclusiveAccessory
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("+20% Critical Chance, but -10% Fire Rate");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();        
            item.rare = 4;
            item.value = Item.buyPrice(gold: 2);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.magicCrit += 20;
            player.meleeCrit += 20;
            player.rangedCrit += 20;
            player.GetModPlayer<wdfeerPlayer>().fireRateMult -= 0.1f;
        }
    }
}