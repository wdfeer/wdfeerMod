using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace wdfeerMod.Items.Accessories
{
    // Here we add our accessories, note that they inherit from ExclusiveAccessory, and not ModItem

    public class Aviator : ExclusiveAccessory
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("+25% Damage Resistance while airborne. Doesn't stack with the Worm Scarf");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();       
            item.rare = ItemRarityID.Orange;
        }

        public override void AddRecipes()
        {
            // because we don't call base.AddRecipes(), we erase the previously defined recipe and can now make a different one
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.WormScarf, 1);
            recipe.AddIngredient(ItemID.CloudinaBottle, 1);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
		{
            if (!FoundAccessoryWithId(ItemID.WormScarf)) player.GetModPlayer<wdfeerPlayer>().aviator=true;			
		}

        public bool FoundAccessoryWithId(int id)
		{
			int maxAccessoryIndex = 5 + Main.LocalPlayer.extraAccessorySlots;
            for (int i = 3; i < 3 + maxAccessoryIndex; i++)
            {
            
                Item otherAccessory = Main.LocalPlayer.armor[i];
                // IsAir makes sure we don't check for "empty" slots
                // IsTheSameAs() compares two items and returns true if their types match
                // "is ExclusiveAccessory" is a way of performing pattern matching
                // Here, inheritance helps us determine if the given item is indeed one of our ExclusiveAccessory ones
                if (!otherAccessory.IsAir &&
                    otherAccessory.netID==id)
                {
                    // If we find an item that matches these criteria, return both the index and the item itself
                    // The second argument is just for convenience, technically we don't need it since we can get the item from just i
                    return true;
                }
            }
            // If no item is found, we return default values for index and item, always check one of them with this default when you call this method!
            return false;
		}
    }
}