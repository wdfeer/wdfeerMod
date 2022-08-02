using Terraria;
using Terraria.DataStructures;
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
            Item.damage = 5;
            Item.crit = 7;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 47;
            Item.height = 16;
            Item.useTime = maxUseTime;
            Item.useAnimation = maxUseTime;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 1;
            Item.value = Item.buyPrice(gold: 6);
            Item.rare = 3;
            Item.autoReuse = true;
            Item.shoot = 10;
            Item.shootSpeed = 14f;
            Item.useAmmo = AmmoID.Bullet;
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

            if (Item.useTime > minUseTime)
            {
                Item.useTime = Item.useTime * 2 / 3;
                Item.useAnimation = Item.useTime;

                if (Item.useTime < minUseTime)
                {
                    Item.useTime = minUseTime;
                    Item.useAnimation = minUseTime;
                }
            }
            else if (timeSinceLastShot > 16)
            {
                Item.useTime += timeSinceLastShot / 3;
                Item.useAnimation += timeSinceLastShot / 3;
                if (Item.useTime > maxUseTime)
                {
                    Item.useTime = maxUseTime;
                    Item.useAnimation = maxUseTime;
                }
            }
            lastShotTime = player.GetModPlayer<wfPlayer>().longTimer;

            return base.CanUseItem(player);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            PlaySound(Main.rand.NextFloat(-0.1f, 0.1f));

            float ammoDamage = (damage / player.GetDamage(DamageClass.Ranged)) / player.GetDamage(DamageClass.Ranged) - Item.damage;
            damage = (int)((Item.damage + ammoDamage / 2) * player.GetDamage(DamageClass.Ranged) * player.GetDamage(DamageClass.Ranged));
            for (int i = 0; i < multishot; i++)
            {
                var proj = ShootWith(position, speedX, speedY, type, damage, knockBack, (timeSinceLastShot > 46 ? 0.015f : 0.08f), 52);
                var gProj = proj.GetGlobalProjectile<Projectiles.wfGlobalProj>();
                gProj.initialPosition = position;
                gProj.falloffStartDist = 300;
                gProj.falloffMaxDist = 500;
                gProj.falloffMax = 0.73f;
                gProj.critMult = 1.15f;
                gProj.AddProcChance(new ProcChance(Mod.Find<ModBuff>("SlashProc").Type, 30));
            }
            return false;
        }
    }
}