using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace wfMod.Items.Accessories
{
    
    public class CriticalDelay : ExclusiveAccessory
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("+12% (+20% in Hardmode) Critical Chance, but -10% Fire Rate");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = 4;
            Item.value = Item.buyPrice(gold: 2);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            int critBoost = Main.hardMode ? 20 : 12;
            player.GetCritChance(DamageClass.Magic) += critBoost;
            player.GetCritChance(DamageClass.Generic) += critBoost;
            player.GetCritChance(DamageClass.Ranged) += critBoost;
            player.GetModPlayer<wfPlayer>().fireRateMult -= 0.1f;
        }
    }
}