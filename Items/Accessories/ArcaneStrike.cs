using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace wdfeerMod.Items.Accessories
{
    // Here we add our accessories, note that they inherit from ExclusiveAccessory, and not ModItem

    public class ArcaneStrike : ExclusiveAccessory
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("15% On hit for +18% Melee Speed for 18s\nMay drop from any boss in Expert Mode");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.rare = -12;
            item.width = 32;
            item.height = 32;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            // Here we add an additional effect
            player.GetModPlayer<wdfeerPlayer>().arcaneStrike = true;
        }
    }
}