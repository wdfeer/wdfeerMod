using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace wfMod.Items.Weapons
{
    public class RaktaDarkDagger : wfWeapon
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Inflicts Confused and replenishes mana on hit\nSpare mana will increase the damage\nMana Sickness reduces mana gain by 60%");
        }
        public override void SetDefaults()
        {
            Item.damage = 24;
            Item.crit = 8;
            Item.DamageType = DamageClass.Magic;
            Item.mana = 2;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.width = 11;
            Item.height = 48;
            Item.useTime = 24;
            Item.useAnimation = 24;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 4;
            Item.value = 750000;
            Item.rare = 4;
            Item.shoot = ModContent.ProjectileType<Projectiles.RaktaDarkDaggerProj>();
            Item.autoReuse = true;
            Item.shootSpeed = 16f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.MagicDagger, 1);
            recipe.AddIngredient(ItemID.SoulofNight, 16);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            var projectile = ShootWith(position, speedX, speedY, type, damage, knockBack, sound: SoundID.Item1);

            return false;
        }
    }
}