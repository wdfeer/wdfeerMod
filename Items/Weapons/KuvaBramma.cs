using Terraria;
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
            item.damage = 419;
            item.crit = 31;
            item.knockBack = 3;
            item.ranged = true;
            item.noMelee = true;
            item.width = 30;
            item.height = 48;
            item.scale = 1.2f;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.UseSound = SoundID.Item38;
            item.useTime = 30;
            item.useAnimation = 30;
            item.rare = 10;
            item.value = 120000;
            item.shoot = mod.ProjectileType("KuvaBrammaProj");
            item.shootSpeed = 20f;
            item.useAmmo = ItemID.Grenade;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-6, 0);
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("Kuva"), 12);
            recipe.AddIngredient(ItemID.GrenadeLauncher, 1);
            recipe.AddIngredient(ItemID.Phantasm, 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            var proj = ShootWith(position, speedX, speedY, mod.ProjectileType("KuvaBrammaProj"), damage, knockBack, offset: item.width - 6);

            return false;
        }
    }
}