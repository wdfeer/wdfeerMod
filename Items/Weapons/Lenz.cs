using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace wdfeerMod.Items.Weapons
{
    internal class Lenz : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Slows all nearby enemies on impact, \nexplodes after a short while");
        }
        public override void SetDefaults()
        {
            item.damage = 200;
            item.crit = 46;
            item.knockBack = 3;
            item.magic = true;
            item.noMelee = true;
            item.mana = 48;
            item.width = 30;
            item.height = 48;
            item.scale = 1.2f;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.UseSound = SoundID.Item5.WithPitchVariance(0.4f);
            item.useTime = 72;
            item.useAnimation = 72;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("LenzProj1");
            item.shootSpeed = 20f;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.DaedalusStormbow, 1);
            recipe.AddIngredient(ItemID.Grenade, 8);
            recipe.AddIngredient(ItemID.IceRod, 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 spawnOffset = new Vector2(speedX, speedY);
            spawnOffset.Normalize();
            spawnOffset *= item.width;
            position += spawnOffset;

            int proj = Projectile.NewProjectile(position, new Vector2(speedX, speedY), type, damage, knockBack, Main.LocalPlayer.cHead);

            return false;
        }
    }
}