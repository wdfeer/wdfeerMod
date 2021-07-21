using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace wdfeerMod.Items.Accessories
{
    // Here we add our accessories, note that they inherit from ExclusiveAccessory, and not ModItem

    public class CorrosiveProjection : ExclusiveAccessory
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Weapons ignore 18% of enemy's Defense");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            // Not calling base.SetDefaults() will override everything
            // Here we inherit all the properties from our abstract item and just change the rarity            
            item.rare = ItemRarityID.Yellow;
        }

        public override void AddRecipes()
        {
            // because we don't call base.AddRecipes(), we erase the previously defined recipe and can now make a different one
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.SoulofLight, 8);
            recipe.AddIngredient(ItemID.AvengerEmblem, 1);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
		{
			// Here we add an additional effect
			player.GetModPlayer<wdfeerPlayer>().corrProj=true;
		}
    }
}