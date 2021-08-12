using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace wdfeerMod.Items.Accessories
{
    // Here we add our accessories, note that they inherit from ExclusiveAccessory, and not ModItem

    public class PiercingCaliber : ExclusiveAccessory
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("+12% Chance to inflict the Weakened debuff for 7 seconds");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.rare = 5;
            item.value = Item.buyPrice(gold: 2);
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("PiercingHit"));
            recipe.AddIngredient(ItemID.HallowedBar,4);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<wdfeerPlayer>().procChances.Add(new ProcChance(BuffID.Weak, 12, 420));
        }
    }
}