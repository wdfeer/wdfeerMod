using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace wfMod.Items.Accessories
{
    
    public class ThermiteRounds : ExclusiveAccessory
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("+300% damage from the On Fire debuff globally, +20% Chance to put enemies on Fire");
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            item.rare = 3;
            item.value = Item.buyPrice(gold: 3);
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            wfPlayer modPl = player.GetModPlayer<wfPlayer>();
            wfPlayer.thermiteRounds = true;
            modPl.AddProcChance(new ProcChance(BuffID.OnFire, 20));
        }
    }
}