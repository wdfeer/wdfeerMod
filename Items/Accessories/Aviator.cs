using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace wfMod.Items.Accessories
{
    
    public class Aviator : ExclusiveAccessory
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("+25% Damage Resistance while airborne. Doesn't stack with the Worm Scarf\nExpert Exclusive");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = ItemRarityID.Orange;
            Item.value = Item.buyPrice(gold: 1);
        }

        public override void AddRecipes()
        {
            // because we don't call base.AddRecipes(), we erase the previously defined recipe and can now make a different one
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.WormScarf, 1);
            recipe.AddIngredient(ItemID.CloudinaBottle, 1);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!PlayerHasAccessory(player, ItemID.WormScarf)) player.GetModPlayer<wfPlayer>().aviator = true;
        }
    }
}