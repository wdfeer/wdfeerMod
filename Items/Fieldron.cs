using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;

namespace wfMod.Items
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
            Item.rare = 8; 
            Item.value = Item.buyPrice(gold: 3);
            Item.maxStack = 99;
            Item.width = 32;
            Item.height = 32;
        }
    }
}