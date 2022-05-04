using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;
using wfMod.Projectiles;

namespace wfMod.Items.Weapons
{
    public class Opticor : BaseOpticor
    {
        protected override float critDmg => 1.25f;
        protected override string soundPath => "Sounds/OpticorSound";
        protected override int getBaseProjTimeLeft()
        {
            var modPl = Main.player[item.owner].GetModPlayer<wfPlayer>();
            return (int)(146 + 54 / modPl.fireRateMult);
        }
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Charges to shoot a devastating beam\nFire rate cannot be increased\n+25% Critical Damage");
        }
        public override void SetDefaults()
        {
            item.damage = 840;
            item.crit = 16;
            item.magic = true;
            item.mana = 77;
            item.width = 48;
            item.height = 16;
            item.useTime = 124;
            item.useAnimation = 124;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.noMelee = true;
            item.knockBack = 20;
            item.value = Item.buyPrice(gold: 10);
            item.rare = 4;
            item.autoReuse = false;
            item.shoot = mod.ProjectileType("OpticorProj");
            item.shootSpeed = 16f;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.SoulofSight, 7);
            recipe.AddIngredient(ItemID.TitaniumBar, 11);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.SoulofSight, 7);
            recipe.AddIngredient(ItemID.AdamantiteBar, 11);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}