using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace wfMod.Items.Weapons
{
    public class Karyst : wdfeerWeapon
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("A heavy throwing dagger that inflicts poison\n+10% Movement Speed while held");
        }
        public override void SetDefaults()
        {
            item.damage = 29;
            item.crit = 6;
            item.melee = true;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.width = 32;
            item.height = 32;
            item.useTime = 57;
            item.useAnimation = 57;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.knockBack = 7;
            item.value = Item.buyPrice(); 
            item.rare = 4;
            item.shoot = ModContent.ProjectileType<Projectiles.KarystProj>();
            item.autoReuse = true;
            item.shootSpeed = 16f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("IronBar",8);
            recipe.AddIngredient(ItemID.Stinger, 4);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
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