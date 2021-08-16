using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace wdfeerMod.Items.Weapons
{
    public class Sybaris : wdfeerWeapon
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Fires 2 rounds in a quick burst while only consuming 1 ammo");
        }
        public override void SetDefaults()
        {
            item.damage = 21;
            item.crit = 21;
            item.ranged = true;
            item.noMelee = true;
            item.width = 46;
            item.height = 14;
            item.useTime = 30;
            item.useAnimation = 30;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.knockBack = 2.4f;
            item.value = Item.buyPrice(gold: 1, silver: 80);
            item.rare = 2;
            item.autoReuse = false;
            item.shoot = ProjectileID.Bullet;
            item.shootSpeed = 16f;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(0, -0.5f);
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Musket);
            recipe.AddIngredient(ItemID.FlintlockPistol);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            var projectile = ShootWith(position, speedX, speedY, type, damage, knockBack, offset: 48, bursts: 2, burstInterval: 3, sound: SoundID.Item11,spreadMult: 0.008f);
            var gProj = projectile.GetGlobalProjectile<Projectiles.wdfeerGlobalProj>();

            return false;
        }
    }
}