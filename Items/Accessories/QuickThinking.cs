using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace wdfeerMod.Items.Accessories
{
    // Here we add our accessories, note that they inherit from ExclusiveAccessory, and not ModItem

    public class QuickThinking : ExclusiveAccessory
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("When in lethal danger, drain mana instead of health with an 8:1 ratio\nDoesn't work when affected by Mana Sickness\nWill not mitigate debuff damage");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();     
            item.rare = 5;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            // Here we add an additional effect
            player.GetModPlayer<wdfeerPlayer>().quickThink = true;
        }
    }
}