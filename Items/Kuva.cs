using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;

namespace wdfeerMod.Items
{
    public class Kuva : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Material for some weapons");
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            item.rare = 5; // the color that the item's name will be in-game
            item.maxStack = 99;
            item.width = 16;
            item.height = 15;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.SoulofNight, 1);
            recipe.AddIngredient(ItemID.CursedFlames, 2);
            recipe.AddTile(TileID.AlchemyTable);
            recipe.SetResult(this);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.SoulofNight, 1);
            recipe.AddIngredient(ItemID.Ichor, 2);
            recipe.AddTile(TileID.AlchemyTable);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}