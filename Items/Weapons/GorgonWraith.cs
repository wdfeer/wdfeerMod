using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;

namespace wfMod.Items.Weapons
{
    public class GorgonWraith : wfWeapon
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Shoots rapidly but inaccurately\n-5% Critical Damage\n60% Chance not to consume ammo");
        }
        public override void SetDefaults()
        {
            Item.damage = 8;
            Item.crit = 11;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 17;
            Item.height = 49;
            Item.useTime = 19;
            Item.useAnimation = 19;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 3;
            Item.value = Item.buyPrice(gold: 6);
            Item.rare = 3;
            Item.UseSound = SoundID.Item11.WithVolume(0.7f);
            Item.autoReuse = true;
            Item.shoot = 10;
            Item.shootSpeed = 16f;
            Item.useAmmo = AmmoID.Bullet;
        }
        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            if (Main.rand.Next(0, 100) <= 60) return false;
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
            else if (timeSinceLastShot > 17)
            {
                Item.useTime += timeSinceLastShot / 3;
                Item.useAnimation += timeSinceLastShot / 3;
                if (Item.useTime > 19)
                {
                    Item.useTime = 19;
                    Item.useAnimation = 19;
                }
            }
            lastShotTime = player.GetModPlayer<wfPlayer>().longTimer;

            return base.CanUseItem(player);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            var proj = ShootWith(position, speedX, speedY, type, damage, knockBack, (timeSinceLastShot > 40 ? 0.02f : 0.07f), 52);
            var gProj = proj.GetGlobalProjectile<Projectiles.wfGlobalProj>();
            gProj.critMult = 0.95f;
            return false;
        }
    }
}