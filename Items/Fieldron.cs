using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;

namespace wdfeerMod.Items
{
    public class Fieldron : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Material for some weapons\nA rare drop from the Martian Madness event");
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            item.rare = 8; 
            item.value = Item.buyPrice(gold: 3);
            item.maxStack = 99;
            item.width = 32;
            item.height = 32;
        }
    }
}