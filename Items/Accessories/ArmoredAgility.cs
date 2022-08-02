using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace wfMod.Items.Accessories
{
    
    public class ArmoredAgility : ExclusiveAccessory
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("+3 Defense\n+20% Movement Speed");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = 3;
            Item.value = Item.buyPrice(silver: 40);
        }

        public override void AddRecipes()
        {
            // because we don't call base.AddRecipes(), we erase the previously defined recipe and can now make a different one
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Shackle, 1);
            recipe.AddIngredient(ItemID.AnkletoftheWind, 1);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statDefense += 3;
            player.moveSpeed += 0.2f;
        }
    }
}