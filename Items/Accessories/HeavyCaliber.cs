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
            Tooltip.SetDefault("+12% (+16% in Hardmode) Ranged, Magic and Throwing damage, but -24% Accuracy");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();        
            Item.rare = 4;
            Item.value = Item.buyPrice(gold: 2);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            var damageMod = Main.hardMode ? 0.16f : 0.12f;
            player.GetDamage(DamageClass.Magic) += damageMod;
            player.GetDamage(DamageClass.Ranged) += damageMod;
            player.GetDamage(DamageClass.Throwing) += damageMod;
            player.GetModPlayer<wfPlayer>().spreadMult += 0.24f;
        }
    }
}