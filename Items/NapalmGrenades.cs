using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;

namespace wfMod.Items
{
    public class NapalmGrenades : ModItem
    {
        bool napalm => Main.player[item.owner].GetModPlayer<wdfeerPlayer>().napalmGrenades;
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Use to make Penta's and Secura Penta's grenades explode on impact, leaving a damage-dealing AoE for 5 seconds that sets enemies on fire\nUse again to reverse the change");
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            item.rare = 3;
            item.value = Item.buyPrice(gold: 2);
            item.maxStack = 1;
            item.width = 44;
            item.height = 64;
            item.scale = 0.4f;
            item.useStyle = ItemUseStyleID.HoldingUp;
            item.useTime = 25;
            item.useAnimation = 25;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.HellstoneBar, 9);
            recipe.AddIngredient(ItemID.Obsidian, 16);
            recipe.SetResult(this);
            recipe.AddTile(TileID.Hellforge);
            recipe.AddRecipe();
        }
        public override bool CanUseItem(Player player)
        {
            player.GetModPlayer<wdfeerPlayer>().napalmGrenades = napalm ? false : true;
            if (napalm) Main.PlaySound(SoundID.Item20);
            else Main.PlaySound(SoundID.Item29);
            return base.CanUseItem(player);
        }
    }
}