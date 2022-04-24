using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace wfMod.Items.Accessories
{
    
    public class Desecrate : ExclusiveAccessory
    {
        public const int lifeConsumption = 7;
        public const float maxDistance = 600;
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault($"Whenever an enemy dies nearby, consume {lifeConsumption} life and double the loot");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.rare = ItemRarityID.Expert;
            item.value = Item.sellPrice(gold: lifeConsumption);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            item.rare = ItemRarityID.Expert;
            // Here we add an additional effect
            player.GetModPlayer<wfPlayer>().desecrate = true;
        }
    }
}