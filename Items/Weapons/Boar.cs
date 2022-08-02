using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace wfMod.Items.Weapons
{
    public class Boar : wfWeapon
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("An automatic shotgun, shoots 3 pellets at once");
        }
        public override void SetDefaults()
        {
            pathToSound = "Sounds/BoarPrimeSound";
            Item.damage = 4;
            Item.crit = 6;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 40;
            Item.height = 17;
            Item.scale = 1.2f;
            Item.useTime = 16;
            Item.useAnimation = 16;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 1;
            Item.value = 15000;
            Item.rare = ItemRarityID.Green;
            Item.autoReuse = true;
            Item.shoot = 10;
            Item.shootSpeed = 20f;
            Item.useAmmo = AmmoID.Bullet; // The "ammo Id" of the ammo item that this weapon uses. Note that this is not an item Id, but just a magic value.
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddRecipeGroup("IronBar", 12);
            recipe.AddIngredient(ItemID.PlatinumBar, 6);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddRecipeGroup("IronBar", 16);
            recipe.AddIngredient(ItemID.SilverBar, 12);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            PlaySound(Main.rand.NextFloat(-0.1f, 0.2f), 0.5f);

            for (int i = 0; i < 3; i++)
            {
                var proj = ShootWith(position, speedX, speedY, type, damage, knockBack, 0.12f, Item.width);
            }
            return false;
        }
    }
}