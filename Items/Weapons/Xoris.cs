using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace wfMod.Items.Weapons
{
    public class Xoris : wfWeapon
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Can explode mid-flight with an 18% Electricity proc chance \nEvery fourth explosion deals 3x the damage");
        }
        public override void SetDefaults()
        {
            Item.damage = 332;
            Item.crit = 20;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.width = 32;
            Item.height = 32;
            Item.useTime = 9;
            Item.useAnimation = 9;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 4;
            Item.value = 750000;
            Item.rare = 10;
            Item.shoot = ModContent.ProjectileType<Projectiles.XorisProj>();
            Item.shootSpeed = 24f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            
            recipe.AddIngredient(Mod.Find<ModItem>("Falcor").Type, 1);
            recipe.AddIngredient(ItemID.FragmentSolar, 8);
            
            recipe.AddTile(412);
            recipe.Register();
        }

        Projectile proj;
        int explosionCount = 0;
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (proj != null && proj.ModProjectile != null && proj.active)
            {
                bool bigBoom = false;
                if (explosionCount >= 3)
                {
                    bigBoom = true;
                    explosionCount = 0;
                }
                Projectiles.XorisProj modProj = proj.ModProjectile as Projectiles.XorisProj;
                modProj.Explode(bigBoom);
                explosionCount++;
            }
            else
            {
                proj = ShootWith(position, speedX, speedY, type, damage, knockBack, sound: SoundID.Item1);
            }

            return false;
        }
    }
}