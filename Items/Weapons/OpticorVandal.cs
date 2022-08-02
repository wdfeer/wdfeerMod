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
            Item.damage = 900;
            Item.crit = 20;
            Item.DamageType = DamageClass.Magic;
            Item.mana = 42;
            Item.width = 48;
            Item.height = 16;
            Item.useTime = 76;
            Item.useAnimation = 76;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 20;
            Item.value = Item.buyPrice(gold: 15);
            Item.rare = 10;
            Item.autoReuse = false;
            Item.shoot = Mod.Find<ModProjectile>("OpticorProj").Type;
            Item.shootSpeed = 16f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(Mod.Find<ModItem>("Opticor").Type, 1);
            recipe.AddIngredient(Mod.Find<ModItem>("Fieldron").Type);
            recipe.AddIngredient(ItemID.FragmentNebula, 12);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}