using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace wfMod.Items.Weapons
{
    internal class Cernos : wfWeapon
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Arrows penetrate an enemy");
        }
        public override void SetDefaults()
        {
            Item.damage = 15;
            Item.crit = 32;
            Item.knockBack = 5;
            Item.DamageType = DamageClass.Ranged;
            Item.noMelee = true;
            Item.width = 36;
            Item.height = 54;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item5;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.rare = 2;
            Item.value = 6000;
            Item.shoot = ProjectileID.WoodenArrowFriendly;
            Item.shootSpeed = 16f;
            Item.useAmmo = ItemID.WoodenArrow;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-4, 0);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.SilverBow, 1);
            recipe.AddIngredient(ItemID.MeteoriteBar, 11);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.TungstenBow, 1);
            recipe.AddIngredient(ItemID.MeteoriteBar, 11);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            var proj = ShootWith(position, speedX, speedY, type, damage, knockBack, offset: Item.width - 4);
            if (proj.penetrate != -1) proj.penetrate++;
            proj.usesLocalNPCImmunity = true;
            proj.localNPCHitCooldown = -1;
            return false;
        }
    }
}