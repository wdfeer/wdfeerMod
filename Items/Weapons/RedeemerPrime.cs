using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;

namespace wfMod.Items.Weapons
{
    public class RedeemerPrime : wfWeapon
    {
        Random rand = new Random();
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Fires 6 golden pellets without consuming ammo\nDamage Falloff starts at 15 tiles, stops after 45");
        }
        public override void SetDefaults()
        {
            pathToSound = "Sounds/RedeemerPrimeSound";
            item.damage = 44;
            item.crit = 20;
            item.melee = true;
            item.noMelee = true;
            item.width = 48;
            item.height = 24;
            item.scale = 1f;
            item.useTime = 60;
            item.useAnimation = 60;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.knockBack = 7;
            item.value = 15000;
            item.rare = 5;
            item.autoReuse = true;
            item.shoot = ProjectileID.GoldenBullet;
            item.shootSpeed = 16f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("Redeemer"));
            recipe.AddIngredient(ItemID.HallowedBar, 8);
            recipe.AddIngredient(ItemID.SoulofFright, 4);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            PlaySound(Main.rand.NextFloat(-0.1f, 0.1f), 0.45f);

            for (int i = 0; i < 6; i++)
            {
                var projectile = ShootWith(position, speedX, speedY, type, damage, knockBack, 0.13f, item.width);
                projectile.ranged = false;
                projectile.melee = true;
                var gProj = projectile.GetGlobalProjectile<Projectiles.wfGlobalProj>();
                gProj.critMult = 1.1f;
                gProj.initialPosition = projectile.position;
                gProj.falloffStartDist = 300;
                gProj.falloffMaxDist = 900;
                gProj.falloffMax = 0.94f;
            }
            return false;
        }
    }
}