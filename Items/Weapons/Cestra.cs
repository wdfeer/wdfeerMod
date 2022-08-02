using Terraria;
using Terraria.DataStructures;
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
        bool dualWield => Item.stack == 2;
        int baseUseTime = 15;
        int spooledUseTime => dualWield ? 5 : 8;
        public override void SetDefaults()
        {
            pathToSound = "Sounds/SupraVandalSound";
            Item.damage = 10;
            Item.crit = 2;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 30;
            Item.height = 14;
            Item.useTime = baseUseTime;
            Item.useAnimation = baseUseTime;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 3.8f;
            Item.value = Item.buyPrice(gold: 4);
            Item.rare = 3;
            Item.autoReuse = true;
            Item.shoot = 10;
            Item.shootSpeed = 16f;
            Item.useAmmo = AmmoID.Bullet;
            Item.maxStack = 2;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-1, 0);
        }
        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            if (Main.rand.Next(0, 100) <= 40) return false;
            return base.CanConsumeAmmo(player);
        }
        int lastShotTime = 0;
        int timeSinceLastShot = 60;
        public override bool CanUseItem(Player player)
        {
            timeSinceLastShot = player.GetModPlayer<wfPlayer>().longTimer - lastShotTime;
            if (Item.useTime > spooledUseTime)
            {
                Item.useTime -= Main.rand.Next(2, 3);
                Item.useAnimation = Item.useTime;
                if (Item.useTime < spooledUseTime)
                {
                    Item.useTime = spooledUseTime;
                    Item.useAnimation = spooledUseTime;
                }
            }
            else if (timeSinceLastShot > spooledUseTime * 2)
            {
                Item.useTime += timeSinceLastShot / 3;
                Item.useAnimation += timeSinceLastShot / 3;
                if (Item.useTime > baseUseTime)
                {
                    Item.useTime = baseUseTime;
                    Item.useAnimation = baseUseTime;
                }
            }
            lastShotTime = player.GetModPlayer<wfPlayer>().longTimer;

            return base.CanUseItem(player);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            PlaySound(Main.rand.NextFloat(0.1f, 0.3f), 0.75f);

            var proj = ShootWith(position, speedX, speedY, type, damage - (dualWield ? 1 : 0), knockBack, (timeSinceLastShot > 50 ? 0 : (dualWield ? 0.14f : 0.044f)), 24);
            proj.extraUpdates = 1;
            var gProj = proj.GetGlobalProjectile<Projectiles.wfGlobalProj>();
            gProj.critMult = 0.8f;
            return false;
        }
    }
}