using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;

namespace wfMod.Items
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
            Item.rare = 5;
            Item.value = Item.buyPrice(silver: 10);
            Item.maxStack = 99;
            Item.width = 32;
            Item.height = 32;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.SoulofNight, 1);
            recipe.AddIngredient(ItemID.CursedFlame, 2);
            recipe.AddTile(TileID.AlchemyTable);
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.SoulofNight, 1);
            recipe.AddIngredient(ItemID.Ichor, 2);
            recipe.AddTile(TileID.AlchemyTable);
            recipe.Register();
        }
    }
}