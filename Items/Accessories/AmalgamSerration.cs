using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace wfMod.Items.Accessories
{
    // Here we add our accessories, note that they inherit from ExclusiveAccessory, and not ModItem

    public class AmalgamSerration : ExclusiveAccessory
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("+10% Ranged and Magic damage\nThe player can run quite fast (75% of the Hermes Boots effect)");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.rare = 5;
            item.value = Item.buyPrice(gold: 8);
        }

        public override void AddRecipes()
        {
            // because we don't call base.AddRecipes(), we erase the previously defined recipe and can now make a different one
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.SorcererEmblem, 1);
            recipe.AddIngredient(ItemID.RangerEmblem, 1);
            recipe.AddIngredient(ItemID.HermesBoots, 1);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.magicDamageMult += 0.1f;
            player.rangedDamageMult += 0.1f;
            player.moveSpeed += 0.75f;
        }
    }
}