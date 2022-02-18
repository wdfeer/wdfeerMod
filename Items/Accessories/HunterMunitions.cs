using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace wfMod.Items.Accessories
{
    // Here we add our accessories, note that they inherit from ExclusiveAccessory, and not ModItem

    public class HunterMunitions : ExclusiveAccessory
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("30% Chance to proc Slash on Critical Hits\n+50% Proc duration on True Melee hits");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            // Not calling base.SetDefaults() will override everything
            // Here we inherit all the properties from our abstract item and just change the rarity            
            item.rare = 2;
            item.value = Item.sellPrice(silver: 80);
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            // Here we add an additional effect
            player.GetModPlayer<wfPlayer>().hunterMuni = true;
        }
    }
}