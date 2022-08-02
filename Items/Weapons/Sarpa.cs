using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace wfMod.Items.Weapons
{
    public class Sarpa : wfWeapon
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Fires 5 rounds in a burst\n28% chance to proc Slash\nDamage Falloff starts at 25 tiles, stops after 50");
        }
        public override void SetDefaults()
        {
            Item.damage = 21;
            Item.crit = 10;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.noMelee = true;
            Item.width = 48;
            Item.height = 24;
            Item.scale = 1f;
            Item.useTime = 60;
            Item.useAnimation = 60;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 2;
            Item.value = Item.buyPrice(gold: 1);
            Item.rare = 3;
            Item.autoReuse = true;
            Item.shoot = ProjectileID.Bullet;
            Item.shootSpeed = 16f;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-4, 2);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Handgun, 1);
            recipe.AddIngredient(ItemID.MeteoriteBar, 8);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            var projectile = ShootWith(position, speedX, speedY, type, damage, knockBack, spreadMult: 0.02f, offset: Item.width, bursts: 5, burstInterval: 3, sound: SoundID.Item11);
            projectile.ranged = false/* tModPorter Suggestion: Remove. See Item.DamageType */;
            projectile.DamageType = DamageClass.Melee;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 2;
            var gProj = projectile.GetGlobalProjectile<Projectiles.wfGlobalProj>();
            gProj.SetFalloff(projectile.position, 500, 1000, 0.86f);
            gProj.AddProcChance(new ProcChance(Mod.Find<ModBuff>("SlashProc").Type, 28));

            return false;
        }
    }
}