using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace wfMod.Items.Accessories
{
    // Here we add our accessories, note that they inherit from ExclusiveAccessory, and not ModItem

    public class HeavyCaliber : ExclusiveAccessory
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("+16% Ranged and Magic damage, but -12% Accuracy");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();        
            item.rare = 4;
            item.value = Item.buyPrice(gold: 2);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.magicDamageMult += 0.16f;
            player.rangedDamageMult += 0.16f;
            player.GetModPlayer<wfPlayer>().spreadMult += 0.12f;
        }
    }
}