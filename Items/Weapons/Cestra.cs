using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;

namespace wfMod.Items.Weapons
{
    public class Cestra : wfWeapon
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Quickly shoots high-velocity bullets after a short spool-up\nDouble stack to increase fire rate at the cost of accuraccy\n-20% Critical Damage\n40% Chance not to consume ammo");
        }
        int baseUseTime = 15;
        int spooledUseTime = 8;
        public override void SetDefaults()
        {
            item.damage = 12;
            item.crit = 2;
            item.ranged = true;
            item.width = 30;
            item.height = 14;
            item.useTime = baseUseTime;
            item.useAnimation = baseUseTime;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.noMelee = true;
            item.knockBack = 3.8f;
            item.value = Item.buyPrice(gold: 4);
            item.rare = 3;
            item.autoReuse = true;
            item.shoot = 10;
            item.shootSpeed = 16f;
            item.useAmmo = AmmoID.Bullet;
            item.maxStack = 2;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-1, 0);
        }
        public override bool ConsumeAmmo(Player player)
        {
            if (Main.rand.Next(0, 100) <= 40) return false;
            return base.ConsumeAmmo(player);
        }
        int lastShotTime = 0;
        int timeSinceLastShot = 60;
        public override bool CanUseItem(Player player)
        {
            if (item.stack == 2) spooledUseTime = 5;
            else spooledUseTime = 8;

            timeSinceLastShot = player.GetModPlayer<wfPlayer>().longTimer - lastShotTime;
            if (item.useTime > spooledUseTime)
            {
                item.useTime -= Main.rand.Next(2, 3);
                item.useAnimation = item.useTime;
                if (item.useTime < spooledUseTime)
                {
                    item.useTime = spooledUseTime;
                    item.useAnimation = spooledUseTime;
                }
            }
            else if (timeSinceLastShot > spooledUseTime * 2)
            {
                item.useTime += timeSinceLastShot / 3;
                item.useAnimation += timeSinceLastShot / 3;
                if (item.useTime > baseUseTime)
                {
                    item.useTime = baseUseTime;
                    item.useAnimation = baseUseTime;
                }
            }
            lastShotTime = player.GetModPlayer<wfPlayer>().longTimer;

            return base.CanUseItem(player);
        }
        Microsoft.Xna.Framework.Audio.SoundEffectInstance sound;
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            sound = mod.GetSound("Sounds/SupraVandalSound").CreateInstance();
            sound.Volume = 0.4f;
            sound.Pitch += Main.rand.NextFloat(0.1f, 0.3f);
            sound.Play();

            var proj = ShootWith(position, speedX, speedY, type, damage, knockBack, (timeSinceLastShot > 50 ? 0 : (item.stack == 2 ? 0.1f : 0.044f)), 24);
            proj.extraUpdates = 1;
            var gProj = proj.GetGlobalProjectile<Projectiles.wfGlobalProj>();
            gProj.critMult = 0.8f;
            return false;
        }
    }
}