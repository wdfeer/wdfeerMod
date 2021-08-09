using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace wdfeerMod.Items.Accessories
{
    // Here we add our accessories, note that they inherit from ExclusiveAccessory, and not ModItem

    public class HeavyCaliber : ExclusiveAccessory
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("+20% Ranged and Magic damage, but -10% Accuracy");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();        
            item.rare = 4;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.magicDamageMult += 0.2f;
            player.rangedDamageMult += 0.2f;
            player.GetModPlayer<wdfeerPlayer>().spreadMult += 0.1f;
        }
    }
}