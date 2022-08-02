using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace wfMod.Items.Accessories
{

    public class PiercingCaliber : ExclusiveAccessory
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("+12% Chance to inflict the Weakened debuff for 7 seconds");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = 5;
            Item.value = Item.buyPrice(gold: 2);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(Mod.Find<ModItem>("PiercingHit").Type);
            recipe.AddIngredient(ItemID.HallowedBar, 4);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<wfPlayer>().AddProcChance(new ProcChance(BuffID.Weak, 12, 420));
        }
    }
}