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
            Item.rare = 4;
            Item.width = 32;
            Item.height = 32;
            Item.value = Item.buyPrice(gold: 2);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<wfPlayer>().critDmgMult += Main.hardMode ? 0.25f : 0.15f;
            player.GetDamage(DamageClass.Generic) -= 0.1f;
        }
    }
}