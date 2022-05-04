using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;

namespace wfMod.Items.Weapons
{
    public class OpticorVandal : BaseOpticor
    {
        protected override float critDmg => 1.3f;
        protected override string soundPath => "Sounds/OpticorVandalSound";
        protected override int getBaseProjTimeLeft()
            => 170;
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Charges to shoot an annihilating beam\nFire rate cannot be increased\n+30% Critical Damage");
        }
        public override void SetDefaults()
        {
            item.damage = 1400;
            item.crit = 20;
            item.magic = true;
            item.mana = 42;
            item.width = 48;
            item.height = 16;
            item.useTime = 76;
            item.useAnimation = 76;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.noMelee = true;
            item.knockBack = 20;
            item.value = Item.buyPrice(gold: 15);
            item.rare = 10;
            item.autoReuse = false;
            item.shoot = mod.ProjectileType("OpticorProj");
            item.shootSpeed = 16f;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("Opticor"), 1);
            recipe.AddIngredient(mod.ItemType("Fieldron"));
            recipe.AddIngredient(ItemID.FragmentNebula, 12);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}