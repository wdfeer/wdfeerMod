using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace wfMod.Items.Accessories
{
    
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
            player.magicDamage += 0.16f;
            player.rangedDamage += 0.16f;
            player.GetModPlayer<wfPlayer>().spreadMult += 0.12f;
        }
    }
}