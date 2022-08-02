using Terraria;
using Terraria.DataStructures;
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
            Item.damage = 17;
            Item.crit = 10;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.noMelee = true;
            Item.width = 48;
            Item.height = 24;
            Item.scale = 1f;
            Item.useTime = 72;
            Item.useAnimation = 72;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 3;
            Item.value = 15000;
            Item.rare = ItemRarityID.Green;
            Item.autoReuse = true;
            Item.shoot = ProjectileID.Bullet;
            Item.shootSpeed = 16f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddRecipeGroup("IronBar", 18);
            recipe.AddIngredient(ItemID.IllegalGunParts, 1);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            PlaySound(Main.rand.NextFloat(0f, 0.16f), 0.4f);

            for (int i = 0; i < 6; i++)
            {
                var projectile = ShootWith(position, speedX, speedY, type, damage, knockBack, 0.14f, Item.width);
                projectile.ranged = false/* tModPorter Suggestion: Remove. See Item.DamageType */;
                projectile.DamageType = DamageClass.Melee;
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