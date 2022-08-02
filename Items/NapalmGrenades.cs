using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;

namespace wfMod.Items
{
    public class NapalmGrenades : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Use to make Penta's and Secura Penta's grenades explode on impact, leaving a damage-dealing AoE for 5 seconds that sets enemies on fire\nUse again to reverse the change");
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = 3;
            Item.value = Item.buyPrice(gold: 2);
            Item.maxStack = 1;
            Item.width = 44;
            Item.height = 64;
            Item.scale = 0.4f;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.useTime = 25;
            Item.useAnimation = 25;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.HellstoneBar, 9);
            recipe.AddIngredient(ItemID.Obsidian, 16);
            recipe.AddTile(TileID.Hellforge);
            recipe.Register();
        }
        public override bool CanUseItem(Player player)
        {
            wfPlayer modPlayer = player.GetModPlayer<wfPlayer>();
            modPlayer.napalmGrenades = modPlayer.napalmGrenades ? false : true;
            if (modPlayer.napalmGrenades) SoundEngine.PlaySound(SoundID.Item20);
            else SoundEngine.PlaySound(SoundID.Item29);
            return base.CanUseItem(player);
        }
    }
}