using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;

namespace wfMod.Items.Weapons
{
    public class SupraVandal : wfWeapon
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Rapidly shoots nano bullets with a 20% chance to inflict Weak\n69% Chance not to consume ammo");
        }
        public override void SetDefaults()
        {
            pathToSound = "Sounds/SupraVandalSound";
            Item.damage = 61;
            Item.crit = 12;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 17;
            Item.height = 47;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 2;
            Item.value = Item.buyPrice(gold: 14);
            Item.rare = 10;
            Item.autoReuse = true;
            Item.shoot = ProjectileID.MartianWalkerLaser;
            Item.shootSpeed = 16f;
            Item.useAmmo = AmmoID.Bullet;
        }
        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            if (Main.rand.Next(0, 100) <= 69) return false;
            return base.CanConsumeAmmo(player);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(Mod.Find<ModItem>("Supra").Type);
            recipe.AddIngredient(ItemID.FragmentVortex, 8);
            recipe.AddTile(412);
            recipe.Register();
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
            else if (timeSinceLastShot > 20)
            {
                Item.useTime += timeSinceLastShot / 3;
                Item.useAnimation += timeSinceLastShot / 3;
                if (Item.useTime > 15)
                {
                    Item.useTime = 15;
                    Item.useAnimation = 15;
                }
            }
            lastShotTime = player.GetModPlayer<wfPlayer>().longTimer;

            return base.CanUseItem(player);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            var volume = 0.75f;
            var pitch = Main.rand.NextFloat(-0.05f, 0.1f);
            PlaySound(pitch, volume);

            var proj = ShootWith(position, speedX, speedY, ProjectileID.NanoBullet, damage, knockBack, (timeSinceLastShot > 20 ? 0 : 0.06f), 48);
            proj.extraUpdates = 3;
            var gProj = proj.GetGlobalProjectile<Projectiles.wfGlobalProj>();
            gProj.AddProcChance(new ProcChance(BuffID.Weak, 20, 200));
            return false;
        }
    }
}