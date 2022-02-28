using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;

namespace wfMod.Items.Weapons
{
    public class Kohm : wfWeapon
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Takes a while to spool up while increasing Multishot up to 5 pellets\n-50% Ammunition damage\nDamage Falloff starts at 15 tiles, stops after 25, reducing damage by 73%\n30% Slash chance\n+15% Critical Damage");
        }
        const int maxUseTime = 82;
        const int minUseTime = 17;
        const int maxMultishot = 5;
        public override void SetDefaults()
        {
            pathToSound = "Sounds/KuvaKohmSound";
            item.damage = 5;
            item.crit = 7;
            item.ranged = true;
            item.width = 47;
            item.height = 16;
            item.useTime = maxUseTime;
            item.useAnimation = maxUseTime;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.noMelee = true;
            item.knockBack = 1;
            item.value = Item.buyPrice(gold: 6);
            item.rare = 3;
            item.autoReuse = true;
            item.shoot = 10;
            item.shootSpeed = 14f;
            item.useAmmo = AmmoID.Bullet;
        }
        int lastShotTime = 0;
        int timeSinceLastShot = 60;
        int multishot = 1;
        public override bool CanUseItem(Player player)
        {
            timeSinceLastShot = player.GetModPlayer<wfPlayer>().longTimer - lastShotTime;

            if (timeSinceLastShot < 24)
                multishot = maxMultishot > multishot ? multishot + 1 : maxMultishot;
            else multishot = 1;

            if (item.useTime > minUseTime)
            {
                item.useTime = item.useTime * 2 / 3;
                item.useAnimation = item.useTime;

                if (item.useTime < minUseTime)
                {
                    item.useTime = minUseTime;
                    item.useAnimation = minUseTime;
                }
            }
            else if (timeSinceLastShot > 16)
            {
                item.useTime += timeSinceLastShot / 3;
                item.useAnimation += timeSinceLastShot / 3;
                if (item.useTime > maxUseTime)
                {
                    item.useTime = maxUseTime;
                    item.useAnimation = maxUseTime;
                }
            }
            lastShotTime = player.GetModPlayer<wfPlayer>().longTimer;

            return base.CanUseItem(player);
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            PlaySound(Main.rand.NextFloat(-0.1f, 0.1f));

            float ammoDamage = (damage / player.rangedDamageMult) / player.rangedDamage - item.damage;
            damage = (int)((item.damage + ammoDamage / 2) * player.rangedDamageMult * player.rangedDamage);
            for (int i = 0; i < multishot; i++)
            {
                var proj = ShootWith(position, speedX, speedY, type, damage, knockBack, (timeSinceLastShot > 46 ? 0.015f : 0.08f), 52);
                var gProj = proj.GetGlobalProjectile<Projectiles.wfGlobalProj>();
                gProj.initialPosition = position;
                gProj.falloffStartDist = 300;
                gProj.falloffMaxDist = 500;
                gProj.falloffMax = 0.73f;
                gProj.critMult = 1.15f;
                gProj.AddProcChance(new ProcChance(mod.BuffType("SlashProc"), 30));
            }
            return false;
        }
    }
}