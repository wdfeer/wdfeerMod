using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace wfMod.Items.Accessories
{
    // Here we add our accessories, note that they inherit from ExclusiveAccessory, and not ModItem

    public class Whirlwind : ExclusiveAccessory
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("+200% Projectile speed");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            // Not calling base.SetDefaults() will override everything
            // Here we inherit all the properties from our abstract item and just change the rarity            
            item.rare = 5;
            item.value = Item.buyPrice(gold: 4);
        }

        public override void AddRecipes()
        {
            // because we don't call base.AddRecipes(), we erase the previously defined recipe and can now make a different one
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.HallowedBar, 8);
            recipe.AddIngredient(ItemID.SoulofFright, 2);
            recipe.AddIngredient(ItemID.SoulofMight, 2);
            recipe.AddIngredient(ItemID.SoulofSight, 2);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            // Here we add an additional effect
            player.GetModPlayer<wfPlayer>().projExtraUpdates += 2;
        }
    }
}