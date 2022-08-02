using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace wfMod.Items.Accessories
{
    
    public class PrimedVigor : ExclusiveAccessory
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("+35 Life, +35 Shields");
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = 5;
            Item.value = Item.buyPrice(gold: 15);
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (PlayerHasAccessory(player, Mod.Find<ModItem>("Vigor").Type))
                return;
            player.statLifeMax2 += 35;
            var shieldPlayer = player.GetModPlayer<wfPlayerShields>();
            shieldPlayer.maxUnderShield += 35;
        }
    }
}