using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;

namespace wfMod.Items.Weapons
{
    public class PrismaGorgon : wfWeapon
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Shoots rapidly but inaccurately\n+15% Critical Damage\n66% Chance not to consume ammo");
        }
        public override void SetDefaults()
        {
            item.damage = 15;
            item.crit = 26;
            item.ranged = true;
            item.width = 17;
            item.height = 51;
            item.useTime = 16;
            item.useAnimation = 16;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.noMelee = true;
            item.knockBack = 2.2f;
            item.value = Item.buyPrice(gold: 60);
            item.rare = 5;
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
        int lastShotTime = 0;
        int timeSinceLastShot = 60;
        public override bool CanUseItem(Player player)
        {
            timeSinceLastShot = player.GetModPlayer<wfPlayer>().longTimer - lastShotTime;
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
            lastShotTime = player.GetModPlayer<wfPlayer>().longTimer;

            return base.CanUseItem(player);
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            var proj = ShootWith(position, speedX, speedY, type, damage, knockBack, (timeSinceLastShot > 21 ? 0 : 0.06f), 53);
            var gProj = proj.GetGlobalProjectile<Projectiles.wfGlobalProj>();
            gProj.critMult = 1.15f;
            return false;
        }
    }
}