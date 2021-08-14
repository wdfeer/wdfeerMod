using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;

namespace wdfeerMod.Items.Weapons
{
    public class SupraVandal : wdfeerWeapon
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Rapidly shoots nano bullets with a 20% chance to inflict Weak\n69% Chance not to consume ammo");
        }
        public override void SetDefaults()
        {
            item.damage = 69;
            item.crit = 12;
            item.ranged = true;
            item.width = 17;
            item.height = 47;
            item.useTime = 15;
            item.useAnimation = 15;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.noMelee = true;
            item.knockBack = 2;
            item.value = Item.buyPrice(gold: 14);
            item.rare = 9;
            item.UseSound = SoundID.Item75.WithPitchVariance(-0.12f).WithVolume(0.3f);
            item.autoReuse = true;
            item.shoot = ProjectileID.MartianWalkerLaser;
            item.shootSpeed = 16f;
            item.useAmmo = AmmoID.Bullet;
        }
        public override bool ConsumeAmmo(Player player)
        {
            if (Main.rand.Next(0, 100) <= 69) return false;
            return base.ConsumeAmmo(player);
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("Supra"));
            recipe.AddIngredient(ItemID.FragmentVortex, 8);
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
            else if (timeSinceLastShot > 20)
            {
                item.useTime += timeSinceLastShot / 3;
                item.useAnimation += timeSinceLastShot / 3;
                if (item.useTime > 15)
                {
                    item.useTime = 15;
                    item.useAnimation = 15;
                }
            }
            lastShotTime = player.GetModPlayer<wdfeerPlayer>().longTimer;

            return base.CanUseItem(player);
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            var proj = ShootWith(position, speedX, speedY, ProjectileID.NanoBullet, damage, knockBack, (timeSinceLastShot > 20 ? 0 : 0.06f), 48);
            proj.extraUpdates = 3;
            var gProj = proj.GetGlobalProjectile<Projectiles.wdfeerGlobalProj>();
            gProj.procChances.Add(new ProcChance(BuffID.Weak, 20, 200));
            return false;
        }
    }
}