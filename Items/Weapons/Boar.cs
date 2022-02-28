using Terraria;
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
            item.damage = 4;
            item.crit = 6;
            item.ranged = true;
            item.width = 40;
            item.height = 17;
            item.scale = 1.2f;
            item.useTime = 16;
            item.useAnimation = 16;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.noMelee = true;
            item.knockBack = 1;
            item.value = 15000;
            item.rare = ItemRarityID.Green;
            item.autoReuse = true;
            item.shoot = 10;
            item.shootSpeed = 20f;
            item.useAmmo = AmmoID.Bullet; // The "ammo Id" of the ammo item that this weapon uses. Note that this is not an item Id, but just a magic value.
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("IronBar", 12);
            recipe.AddIngredient(ItemID.PlatinumBar, 6);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("IronBar", 16);
            recipe.AddIngredient(ItemID.SilverBar, 12);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            PlaySound(Main.rand.NextFloat(-0.1f, 0.2f), 0.5f);

            for (int i = 0; i < 3; i++)
            {
                var proj = ShootWith(position, speedX, speedY, type, damage, knockBack, 0.12f, item.width);
            }
            return false;
        }
    }
}