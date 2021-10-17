using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace wfMod.Items.Weapons
{
    public class Xoris : wdfeerWeapon
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Can explode mid-flight with an 18% Electricity proc chance \nEvery fourth explosion deals 3x the damage");
        }
        public override void SetDefaults()
        {
            item.damage = 332; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
            item.crit = 20;
            item.melee = true; // sets the damage type to ranged
            item.noMelee = true;
            item.noUseGraphic = true;
            item.width = 32; // hitbox width of the item
            item.height = 32; // hitbox height of the item
            item.useTime = 9; // The item's use time in ticks (60 ticks == 1 second.)
            item.useAnimation = 9; // The length of the item's use animation in ticks (60 ticks == 1 second.)
            item.useStyle = ItemUseStyleID.SwingThrow; // how you use the item (swinging, holding out, etc)
            item.knockBack = 4; // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
            item.value = 750000; // how much the item sells for (measured in copper)
            item.rare = 10; // the color that the item's name will be in-game
            item.shoot = ModContent.ProjectileType<Projectiles.XorisProj>(); //idk why but all the guns in the vanilla source have this
            item.shootSpeed = 24f; // the speed of the projectile (measured in pixels per frame)
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