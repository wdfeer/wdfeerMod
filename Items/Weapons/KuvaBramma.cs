using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace wfMod.Items.Weapons
{
    internal class KuvaBramma : wfWeapon
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Shoots an explosive arrow that spawns 3 smaller explosions on impact\nUses grenades as Ammo");
        }
        public override void SetDefaults()
        {
            Item.damage = 419;
            Item.crit = 31;
            Item.knockBack = 3;
            Item.DamageType = DamageClass.Ranged;
            Item.noMelee = true;
            Item.width = 30;
            Item.height = 48;
            Item.scale = 1.2f;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item38;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.rare = 10;
            Item.value = 120000;
            Item.shoot = Mod.Find<ModProjectile>("KuvaBrammaProj").Type;
            Item.shootSpeed = 20f;
            Item.useAmmo = ItemID.Grenade;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-6, 0);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(Mod.Find<ModItem>("Kuva").Type, 12);
            recipe.AddIngredient(ItemID.GrenadeLauncher, 1);
            recipe.AddIngredient(ItemID.Phantasm, 1);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            var proj = ShootWith(position, speedX, speedY, Mod.Find<ModProjectile>("KuvaBrammaProj").Type, damage, knockBack, offset: Item.width - 6);

            return false;
        }
    }
}