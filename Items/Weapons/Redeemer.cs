using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;

namespace wfMod.Items.Weapons
{
    public class Redeemer : wfWeapon
    {
        Random rand = new Random();
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Fires 6 pellets without consuming ammo\nDamage Falloff starts at 15 tiles, stops after 30");
        }
        public override void SetDefaults()
        {
            pathToSound = "Sounds/RedeemerPrimeSound";
            item.damage = 17;
            item.crit = 10;
            item.melee = true;
            item.noMelee = true;
            item.width = 48;
            item.height = 24;
            item.scale = 1f;
            item.useTime = 72;
            item.useAnimation = 72;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.knockBack = 3;
            item.value = 15000;
            item.rare = ItemRarityID.Green;
            item.autoReuse = true;
            item.shoot = ProjectileID.Bullet;
            item.shootSpeed = 16f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("IronBar", 18);
            recipe.AddIngredient(ItemID.IllegalGunParts, 1);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            PlaySound(Main.rand.NextFloat(0f, 0.16f), 0.4f);

            for (int i = 0; i < 6; i++)
            {
                var projectile = ShootWith(position, speedX, speedY, type, damage, knockBack, 0.14f, item.width);
                projectile.ranged = false;
                projectile.melee = true;
                var gProj = projectile.GetGlobalProjectile<Projectiles.wfGlobalProj>();
                gProj.initialPosition = projectile.position;
                gProj.falloffStartDist = 300;
                gProj.falloffMaxDist = 600;
                gProj.falloffMax = 0.83f;
            }
            return false;
        }
    }
}