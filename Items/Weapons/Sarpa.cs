using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace wdfeerMod.Items.Weapons
{
    public class Sarpa : wdfeerWeapon
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Fires 5 rounds in a burst\n28% chance to proc Slash\nDamage Falloff starts at 25 tiles, stops after 50");
        }
        public override void SetDefaults()
        {
            item.damage = 17;
            item.crit = 10;
            item.melee = true;
            item.noMelee = true;
            item.width = 48;
            item.height = 24;
            item.scale = 1f;
            item.useTime = 60;
            item.useAnimation = 60;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.knockBack = 2;
            item.value = Item.buyPrice(gold: 1);
            item.rare = 3;
            item.autoReuse = true;
            item.shoot = ProjectileID.Bullet;
            item.shootSpeed = 16f;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-4, 2);
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.MeteoriteBar, 8);
            recipe.AddIngredient(ItemID.Handgun, 1);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            var projectile = ShootWith(position, speedX, speedY, type, damage, knockBack, offset: 64, bursts: 5, burstInterval: 3, sound: SoundID.Item11);
            projectile.ranged = false;
            projectile.melee = true;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 2;
            var gProj = projectile.GetGlobalProjectile<Projectiles.wdfeerGlobalProj>();
            gProj.SetFalloff(projectile.position, 500, 1000, 0.86f);
            gProj.procChances.Add(new ProcChance(mod.BuffType("SlashProc"), 28));

            return false;
        }
    }
}