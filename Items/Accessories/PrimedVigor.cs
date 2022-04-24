using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace wfMod.Items.Accessories
{
    
    public class PrimedVigor : ExclusiveAccessory
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("+35 Max Life, +35 Max Shields");
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            item.rare = 5;
            item.value = Item.buyPrice(gold: 15);
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (PlayerHasAccessory(player, mod.ItemType("Vigor")))
                return;
            player.statLifeMax2 += 35;
            var shieldPlayer = player.GetModPlayer<wfPlayerShields>();
            shieldPlayer.maxShield += 35;
        }
    }
}