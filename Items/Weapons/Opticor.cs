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
            var modPl = Main.player[Item.playerIndexTheItemIsReservedFor].GetModPlayer<wfPlayer>();
            return (int)(146 + 54 / modPl.fireRateMult);
        }
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Charges to shoot a devastating beam\nFire rate cannot be increased\n+25% Critical Damage");
        }
        public override void SetDefaults()
        {
            Item.damage = 690;
            Item.crit = 16;
            Item.DamageType = DamageClass.Magic;
            Item.mana = 77;
            Item.width = 48;
            Item.height = 16;
            Item.useTime = 124;
            Item.useAnimation = 124;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 20;
            Item.value = Item.buyPrice(gold: 10);
            Item.rare = 4;
            Item.autoReuse = false;
            Item.shoot = Mod.Find<ModProjectile>("OpticorProj").Type;
            Item.shootSpeed = 16f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.SoulofSight, 7);
            recipe.AddIngredient(ItemID.TitaniumBar, 11);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.SoulofSight, 7);
            recipe.AddIngredient(ItemID.AdamantiteBar, 11);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}