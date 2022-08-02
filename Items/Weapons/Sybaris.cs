using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace wfMod.Items.Weapons
{
    public class Sybaris : wfWeapon
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Fires 2 rounds in a quick burst while only consuming 1 ammo");
        }
        public override void SetDefaults()
        {
            Item.damage = 25;
            Item.crit = 21;
            Item.DamageType = DamageClass.Ranged;
            Item.noMelee = true;
            Item.width = 46;
            Item.height = 14;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 2.4f;
            Item.value = Item.buyPrice(gold: 1, silver: 80);
            Item.rare = 2;
            Item.autoReuse = false;
            Item.shoot = ProjectileID.Bullet;
            Item.shootSpeed = 16f;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(0, -0.5f);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Musket);
            recipe.AddIngredient(ItemID.FlintlockPistol);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            var projectile = ShootWith(position, speedX, speedY, type, damage, knockBack, offset: 48, bursts: 2, burstInterval: 3, sound: SoundID.Item11,spreadMult: 0.008f);
            var gProj = projectile.GetGlobalProjectile<Projectiles.wfGlobalProj>();

            return false;
        }
    }
}