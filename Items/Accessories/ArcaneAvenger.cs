using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace wdfeerMod.Items.Accessories
{
    // Here we add our accessories, note that they inherit from ExclusiveAccessory, and not ModItem

    public class ArcaneAvenger : ExclusiveAccessory
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("21% Chance to get +45% Critical Chance for 12s after getting hit for more than 4 damage\nMay drop from any boss in Expert Mode");
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
            player.GetModPlayer<wdfeerPlayer>().avenger = true;
        }

        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
            scale /= 2;
            return base.PreDrawInWorld(spriteBatch, lightColor, alphaColor, ref rotation, ref scale, whoAmI);
        }
    }
}