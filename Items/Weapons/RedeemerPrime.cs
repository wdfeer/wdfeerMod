using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;

namespace wfMod.Items.Weapons
{
    public class RedeemerPrime : wfWeapon
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Fires 6 golden pellets without consuming ammo\nDamage Falloff starts at 15 tiles, stops after 45");
        }
        public override void SetDefaults()
        {
            pathToSound = "Sounds/RedeemerPrimeSound";
            Item.damage = 44;
            Item.crit = 20;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.noMelee = true;
            Item.width = 48;
            Item.height = 24;
            Item.scale = 1f;
            Item.useTime = 60;
            Item.useAnimation = 60;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 7;
            Item.value = 15000;
            Item.rare = 5;
            Item.autoReuse = true;
            Item.shoot = ProjectileID.GoldenBullet;
            Item.shootSpeed = 16f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(Mod.Find<ModItem>("Redeemer").Type);
            recipe.AddIngredient(ItemID.HallowedBar, 8);
            recipe.AddIngredient(ItemID.SoulofFright, 4);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            PlaySound(Main.rand.NextFloat(-0.1f, 0.1f), 0.45f);

            for (int i = 0; i < 6; i++)
            {
                var projectile = ShootWith(position, speedX, speedY, type, damage, knockBack, 0.13f, Item.width);
                projectile.ranged = false/* tModPorter Suggestion: Remove. See Item.DamageType */;
                projectile.DamageType = DamageClass.Melee;
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