using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace wdfeerMod.Items.Weapons
{
    internal class Cernos : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Arrows penetrate an enemy");
        }
        public override void SetDefaults()
        {
            item.damage = 15;
            item.crit = 32;
            item.knockBack = 5;
            item.ranged = true;
            item.noMelee = true;
            item.width = 36;
            item.height = 54;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.UseSound = SoundID.Item5;
            item.useTime = 30;
            item.useAnimation = 30;
            item.rare = 2;
            item.value = 6000;
            item.shoot = ProjectileID.WoodenArrowFriendly;
            item.shootSpeed = 17f;
            item.useAmmo = ItemID.WoodenArrow;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-4,0);
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.DemonBow,1);
            recipe.AddIngredient(ItemID.MeteoriteBar, 8);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.TendonBow,1);
            recipe.AddIngredient(ItemID.MeteoriteBar, 8);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 spawnOffset = new Vector2(speedX, speedY);
            spawnOffset.Normalize();
            spawnOffset *= 60;
            position += spawnOffset;

            int proj = Projectile.NewProjectile(position, new Vector2(speedX, speedY), type, damage, knockBack, Main.LocalPlayer.cHead);
            if (Main.projectile[proj].penetrate < 2 && Main.projectile[proj].penetrate != -1)Main.projectile[proj].penetrate = 2;
            Main.projectile[proj].usesLocalNPCImmunity = true;
            Main.projectile[proj].localNPCHitCooldown = -1;
            return false;
        }
    }
}