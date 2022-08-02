using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace wfMod.Items.Accessories
{
    
    public class ShieldCharger : ExclusiveAccessory
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("+4 Shield and +4% Shield regen for each active minion");
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = 3;
            Item.value = Item.buyPrice(gold: 4);
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            var shieldPlayer = player.GetModPlayer<wfPlayerShields>();
            shieldPlayer.maxUnderShield += 4 * player.numMinions;
            shieldPlayer.shieldRegen += 0.04f * player.numMinions;
        }
    }
}