using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace wfMod.Items.Accessories
{
    
    public class HollowPoint : ExclusiveAccessory
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("+15% (+25% in Hardmode) critical damage, but -10% total damage");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.rare = 4;
            item.width = 32;
            item.height = 32;
            item.value = Item.buyPrice(gold: 2);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<wfPlayer>().critDmgMult += Main.hardMode ? 0.25f : 0.15f;
            player.allDamageMult -= 0.1f;
        }
    }
}