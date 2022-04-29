using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace wfMod.Items.Accessories
{

    public class HighVoltage : ExclusiveAccessory
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("+12% Damage from Electricity debuffs\n+12% Chance to inflict Electricity debuffs");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.rare = 2;
            item.value = Item.buyPrice(gold: 3);
        }

        public override void AddRecipes()
        {
            // because we don't call base.AddRecipes(), we erase the previously defined recipe and can now make a different one
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.SoulofMight, 4);
            recipe.AddIngredient(ItemID.SoulofLight, 12);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            // Here we add an additional effect
            player.GetModPlayer<wfPlayer>().electroMult += 0.12f;
            player.GetModPlayer<wfPlayer>().AddProcChance(new ProcChance(BuffID.Electrified, 12));
        }
    }
}