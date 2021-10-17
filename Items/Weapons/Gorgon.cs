using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;

namespace wfMod.Items.Weapons
{
    public class Gorgon : wfWeapon
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Takes a while to spool up, shoots rapidly but inaccurately\n-25% Critical Damage\n40% Chance not to consume ammo");
        }
        public override void SetDefaults()
        {
            item.damage = 5;
            item.crit = 13;
            item.ranged = true;
            item.width = 50;
            item.height = 19;
            item.useTime = 22;
            item.useAnimation = 22;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.noMelee = true;
            item.knockBack = 1;
            item.value = Item.buyPrice(gold: 3);
            item.rare = 3;
            item.UseSound = SoundID.Item11.WithVolume(0.75f);
            item.autoReuse = true;
            item.shoot = 10;
            item.shootSpeed = 14f;
            item.useAmmo = AmmoID.Bullet;
        }
        public override bool ConsumeAmmo(Player player)
        {
            if (Main.rand.Next(0, 100) <= 40) return false;
            return base.ConsumeAmmo(player);
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Minishark);
            recipe.AddIngredient(ItemID.JungleSpores,9);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        int lastShotTime = 0;
        int timeSinceLastShot = 60;
        public override bool CanUseItem(Player player)
        {
            timeSinceLastShot = player.GetModPlayer<wfPlayer>().longTimer - lastShotTime;
            if (item.useTime > 6)
            {
                item.useTime -= 2;
                item.useAnimation -= 2;
                if (item.useTime < 6)
                {
                    item.useTime = 6;
                    item.useAnimation = 6;
                }
            }
            else if (timeSinceLastShot > 14)
            {
                item.useTime += timeSinceLastShot / 3;
                item.useAnimation += timeSinceLastShot / 3;
                if (item.useTime > 22)
                {
                    item.useTime = 22;
                    item.useAnimation = 22;
                }
            }
            lastShotTime = player.GetModPlayer<wfPlayer>().longTimer;

            return base.CanUseItem(player);
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            var proj = ShootWith(position, speedX, speedY, type, damage, knockBack, (timeSinceLastShot > 40 ? 0.03f : 0.08f), 52);
            var gProj = proj.GetGlobalProjectile<Projectiles.wfGlobalProj>();
            gProj.critMult = 0.75f;
            return false;
        }
    }
}