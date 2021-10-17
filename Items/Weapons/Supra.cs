using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;

namespace wfMod.Items.Weapons
{
    public class Supra : wfWeapon
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Rapidly shoots nano bullets\n-10% Critical Damage\n69% Chance not to consume ammo");
        }
        public override void SetDefaults()
        {
            item.damage = 47;
            item.crit = 8;
            item.ranged = true;
            item.width = 17;
            item.height = 48;
            item.useTime = 16;
            item.useAnimation = 16;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.noMelee = true;
            item.knockBack = 2;
            item.value = Item.buyPrice(gold: 5);
            item.rare = 9;
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
            recipe.AddIngredient(ItemID.LaserMachinegun);
            recipe.AddIngredient(mod.ItemType("Fieldron"), 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
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
            else if (timeSinceLastShot > 15)
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
        Microsoft.Xna.Framework.Audio.SoundEffectInstance sound;
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            sound = mod.GetSound("Sounds/SupraVandalSound").CreateInstance();
            sound.Volume = 0.5f;
            sound.Pitch += Main.rand.NextFloat(0f, 0.2f);
            sound.Play();

            var proj = ShootWith(position, speedX, speedY, ProjectileID.NanoBullet, damage, knockBack, (timeSinceLastShot > 30 ? 0 : 0.065f), 50);
            proj.extraUpdates = 2;
            var gProj = proj.GetGlobalProjectile<Projectiles.wfGlobalProj>();
            gProj.critMult = 0.9f;
            return false;
        }
    }
}