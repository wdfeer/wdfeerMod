using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace wfMod.Items.Accessories
{
    
    public class Blaze : ExclusiveAccessory
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("+9% Damage, +12% Chance to put enemies on Fire");
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = 3;
            Item.value = Item.buyPrice(gold: 3);
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(DamageClass.Generic) += 0.09f;
            player.GetModPlayer<wfPlayer>().AddProcChance(new ProcChance(BuffID.OnFire, 12));
        }
    }
}