using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;

namespace wdfeerMod.Items.Weapons
{
    public class PrismaGorgon : wdfeerWeapon
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Shoots rapidly but inaccurately, converts ammo into luminite bullets\n+15% Critical Damage\n66% Chance not to consume ammo");
        }
        public override void SetDefaults()
        {
            item.damage = 77;
            item.crit = 26;
            item.ranged = true;
            item.width = 17;
            item.height = 51;
            item.useTime = 16;
            item.useAnimation = 16;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.noMelee = true;
            item.knockBack = 2.2f;
            item.value = Item.buyPrice(gold: 17);
            item.rare = 10;
            item.UseSound = SoundID.Item11.WithVolume(0.7f);
            item.autoReuse = true;
            item.shoot = 10;
            item.shootSpeed = 16f;
            item.useAmmo = AmmoID.Bullet;
        }
        public override bool ConsumeAmmo(Player player)
        {
            if (Main.rand.Next(0, 100) <= 66) return false;
            return base.ConsumeAmmo(player);
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("Gorgon"));
            recipe.AddIngredient(ItemID.LunarBar, 7);
            recipe.AddTile(412);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        int lastShotTime = 0;
        int timeSinceLastShot = 60;
        public override bool CanUseItem(Player player)
        {
            timeSinceLastShot = player.GetModPlayer<wdfeerPlayer>().longTimer - lastShotTime;
            if (item.useTime > 5)
            {
                item.useTime -= 3;
                item.useAnimation -= 3;
                if (item.useTime < 5)
                {
                    item.useTime = 5;
                    item.useAnimation = 5;
                }
            }
            else if (timeSinceLastShot > 21)
            {
                item.useTime += timeSinceLastShot / 3;
                item.useAnimation += timeSinceLastShot / 3;
                if (item.useTime > 16)
                {
                    item.useTime = 16;
                    item.useAnimation = 16;
                }
            }
            lastShotTime = player.GetModPlayer<wdfeerPlayer>().longTimer;

            return base.CanUseItem(player);
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            var proj = ShootWith(position, speedX, speedY, ProjectileID.MoonlordBullet, damage, knockBack, (timeSinceLastShot > 21 ? 0 : 0.055f), 53);
            var gProj = proj.GetGlobalProjectile<Projectiles.wdfeerGlobalProj>();
            gProj.critMult = 1.15f;
            return false;
        }
    }
}