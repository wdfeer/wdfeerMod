using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace wfMod.Items.Accessories
{
    // Here we add our accessories, note that they inherit from ExclusiveAccessory, and not ModItem

    public class ArgonScope : ExclusiveAccessory
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("+15% Critical Chance for 9 seconds after hitting an enemy with a projectile when they move in opposite directions\nCan't be triggered by most AoE effects");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.rare = 4;
            item.width = 32;
            item.height = 32;
            item.value = Item.buyPrice(gold: 1);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            // Here we add an additional effect
            player.GetModPlayer<wdfeerPlayer>().argonScope = true;
        }
    }
}