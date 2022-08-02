using Terraria;
using Terraria.DataStructures;
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
            Item.damage = 15;
            Item.crit = 26;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 17;
            Item.height = 51;
            Item.useTime = 16;
            Item.useAnimation = 16;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 2.2f;
            Item.value = Item.buyPrice(gold: 60);
            Item.rare = 5;
            Item.UseSound = SoundID.Item11.WithVolume(0.7f);
            Item.autoReuse = true;
            Item.shoot = 10;
            Item.shootSpeed = 16f;
            Item.useAmmo = AmmoID.Bullet;
        }
        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            if (Main.rand.Next(0, 100) <= 66) return false;
            return base.CanConsumeAmmo(player);
        }
        int lastShotTime = 0;
        int timeSinceLastShot = 60;
        public override bool CanUseItem(Player player)
        {
            timeSinceLastShot = player.GetModPlayer<wfPlayer>().longTimer - lastShotTime;
            if (Item.useTime > 5)
            {
                Item.useTime -= 3;
                Item.useAnimation -= 3;
                if (Item.useTime < 5)
                {
                    Item.useTime = 5;
                    Item.useAnimation = 5;
                }
            }
            else if (timeSinceLastShot > 21)
            {
                Item.useTime += timeSinceLastShot / 3;
                Item.useAnimation += timeSinceLastShot / 3;
                if (Item.useTime > 16)
                {
                    Item.useTime = 16;
                    Item.useAnimation = 16;
                }
            }
            lastShotTime = player.GetModPlayer<wfPlayer>().longTimer;

            return base.CanUseItem(player);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            var proj = ShootWith(position, speedX, speedY, type, damage, knockBack, (timeSinceLastShot > 21 ? 0 : 0.06f), 53);
            var gProj = proj.GetGlobalProjectile<Projectiles.wfGlobalProj>();
            gProj.critMult = 1.15f;
            return false;
        }
    }
}