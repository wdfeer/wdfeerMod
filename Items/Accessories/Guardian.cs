using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace wfMod.Items.Accessories
{

    public class Guardian : ExclusiveAccessory
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("When your shield is depleted, boost it by 8% for each active minion, up to 64%");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.rare = 2;
            item.value = Item.buyPrice(gold: 5);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            var shieldPl = player.GetModPlayer<wfPlayerShields>();
            shieldPl.onShieldsDamaged.Add((shieldDmg, lifeDmg) =>
            {
                if (shieldPl.shield == 0 && shieldDmg >= shieldPl.maxUnderShield)
                {
                    float shieldRegain = 0.08f * player.numMinions;
                    shieldRegain = shieldRegain > 0.64f ? 0.64f : shieldRegain;
                    shieldRegain *= shieldPl.maxUnderShield;
                    shieldPl.shield += shieldRegain;
                }
            }
            );
        }
    }
}