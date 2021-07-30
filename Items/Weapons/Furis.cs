using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;

namespace wdfeerMod.Items.Weapons
{
    public class Furis : wdfeerWeapon
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("50% Chance not to consume ammo\n-50% Damage");
        }
        public override void SetDefaults()
        {
            item.damage = 7;
            item.ranged = true;
            item.width = 16;
            item.height = 14;
            item.useTime = 6;
            item.useAnimation = 6;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.noMelee = true;
            item.knockBack = 0;
            item.value = 1500;
            item.rare = 3;
            item.UseSound = SoundID.Item11.WithVolume(0.8f);
            item.autoReuse = true;
            item.shoot = 10;
            item.shootSpeed = 15f;
            item.useAmmo = AmmoID.Bullet;
        }
        public override bool ConsumeAmmo(Player player)
        {
            if (Main.rand.Next(0, 100) <= 50) return false;
            return base.ConsumeAmmo(player);
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Megashark, 1);
            recipe.AddIngredient(ItemID.TinBar, 20);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Megashark, 1);
            recipe.AddIngredient(ItemID.CopperBar, 20);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            damage /= 2;
            ShootWith(position, speedX, speedY, type, damage, knockBack, 0.025f);
            return false;
        }
    }
}