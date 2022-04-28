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
            item.rare = 5;
            item.value = Item.buyPrice(silver: 10);
            item.maxStack = 99;
            item.width = 32;
            item.height = 32;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.SoulofNight, 1);
            recipe.AddIngredient(ItemID.CursedFlame, 2);
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