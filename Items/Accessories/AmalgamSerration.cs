using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace wfMod.Items.Accessories
{
    
    public class AmalgamSerration : ExclusiveAccessory
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("+10% Ranged and Magic damage\nThe player can run quite fast (Hermes Boots effect)");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = 5;
            Item.value = Item.buyPrice(gold: 8);
        }

        public override void AddRecipes()
        {
            // because we don't call base.AddRecipes(), we erase the previously defined recipe and can now make a different one
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.SorcererEmblem, 1);
            recipe.AddIngredient(ItemID.RangerEmblem, 1);
            recipe.AddIngredient(ItemID.HermesBoots, 1);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(DamageClass.Magic) += 0.1f;
            player.GetDamage(DamageClass.Ranged) += 0.1f;
            player.moveSpeed += 1f;
        }
    }
}