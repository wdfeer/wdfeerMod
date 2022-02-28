using Terraria;
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
            item.damage = 332;
            item.crit = 20;
            item.melee = true;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.width = 32;
            item.height = 32;
            item.useTime = 9;
            item.useAnimation = 9;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.knockBack = 4;
            item.value = 750000;
            item.rare = 10;
            item.shoot = ModContent.ProjectileType<Projectiles.XorisProj>();
            item.shootSpeed = 24f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            
            recipe.AddIngredient(mod.ItemType("Falcor"), 1);
            recipe.AddIngredient(ItemID.FragmentSolar, 8);
            
            recipe.AddTile(412);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        Projectile proj;
        int explosionCount = 0;
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (proj != null && proj.modProjectile != null && proj.active)
            {
                bool bigBoom = false;
                if (explosionCount >= 3)
                {
                    bigBoom = true;
                    explosionCount = 0;
                }
                Projectiles.XorisProj modProj = proj.modProjectile as Projectiles.XorisProj;
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