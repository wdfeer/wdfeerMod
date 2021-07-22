using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace wdfeerMod.Items.Accessories
{
    // Here we add our accessories, note that they inherit from ExclusiveAccessory, and not ModItem

    public class ConditionOverload : ExclusiveAccessory
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("+10% Damage for each Unique Status Effect on the Target");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();           
            item.rare = 6;
        }

        public override void AddRecipes()
        {
            // because we don't call base.AddRecipes(), we erase the previously defined recipe and can now make a different one
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.AnkhCharm,1);
            recipe.AddIngredient(ItemID.AvengerEmblem, 1);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
		{
			// Here we add an additional effect
			player.GetModPlayer<wdfeerPlayer>().condOv = true;
		}
    }
}