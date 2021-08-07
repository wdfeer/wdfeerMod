using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace wdfeerMod.Items.Accessories
{
    // Here we add our accessories, note that they inherit from ExclusiveAccessory, and not ModItem

    public class PiercingHit : ExclusiveAccessory
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("+4% Chance to inflict the Weakened debuff for 7 seconds");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.rare = 2;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<wdfeerPlayer>().procChances.Add(new ProcChance(BuffID.Weak, 4, 420));
        }
    }
}