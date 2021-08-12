using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace wdfeerMod.Items.Accessories
{
    // Here we add our accessories, note that they inherit from ExclusiveAccessory, and not ModItem

    public class HollowPoint : ExclusiveAccessory
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("+25% Critical Damage, but -10% Damage");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();        
            item.rare = 4;
            item.width = 32;
            item.height = 32;
            item.value = Item.buyPrice(gold: 2);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<wdfeerPlayer>().critDmgMult += 0.25f;
            player.GetModPlayer<wdfeerPlayer>().fireRateMult -= 0.1f;
        }
    }
}