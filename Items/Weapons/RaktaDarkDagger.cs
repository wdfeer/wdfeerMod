using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace wfMod.Items.Weapons
{
    public class RaktaDarkDagger : wdfeerWeapon
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Inflicts Confused and replenishes mana on hit\nSpare mana will increase the damage\nMana Sickness reduces mana gain by 60%");
        }
        public override void SetDefaults()
        {
            item.damage = 24;
            item.crit = 8;
            item.magic = true;
            item.mana = 2;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.width = 11;
            item.height = 48;
            item.useTime = 24;
            item.useAnimation = 24;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.knockBack = 4;
            item.value = 750000;
            item.rare = 4;
            item.shoot = ModContent.ProjectileType<Projectiles.RaktaDarkDaggerProj>();
            item.autoReuse = true;
            item.shootSpeed = 16f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.MagicDagger, 1);
            recipe.AddIngredient(ItemID.SoulofNight, 16);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            var projectile = ShootWith(position, speedX, speedY, type, damage, knockBack, sound: SoundID.Item1);

            return false;
        }
    }
}