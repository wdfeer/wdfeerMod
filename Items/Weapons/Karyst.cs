using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace wfMod.Items.Weapons
{
    public class Karyst : wfWeapon
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("A heavy throwing dagger that inflicts poison\n+10% Movement Speed while held");
        }
        public override void SetDefaults()
        {
            Item.damage = 29;
            Item.crit = 6;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.width = 32;
            Item.height = 32;
            Item.useTime = 57;
            Item.useAnimation = 57;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 7;
            Item.value = Item.buyPrice(); 
            Item.rare = 4;
            Item.shoot = ModContent.ProjectileType<Projectiles.KarystProj>();
            Item.autoReuse = true;
            Item.shootSpeed = 16f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddRecipeGroup("IronBar",8);
            recipe.AddIngredient(ItemID.Stinger, 4);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            var projectile = ShootWith(position, speedX, speedY, type, damage, knockBack, sound: SoundID.Item1);

            return false;
        }
        public override void HoldItem(Player player)
        {
            player.moveSpeed += 0.11f;
        }
    }
}