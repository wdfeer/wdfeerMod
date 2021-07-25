using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;

namespace wdfeerMod.Items.Weapons
{
    public class GlaxionVandal : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Will slow and freeze enemies \nDeals damage in an AoE on impact");
        }
        public override void SetDefaults()
        {
            item.damage = 64; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
            item.crit = 14;
            item.ranged = true; // sets the damage type to ranged
            item.width = 64; // hitbox width of the item
            item.height = 14; // hitbox height of the item
            item.useTime = 5; // The item's use time in ticks (60 ticks == 1 second.)
            item.useAnimation = 5; // The length of the item's use animation in ticks (60 ticks == 1 second.)
            item.useStyle = ItemUseStyleID.HoldingOut; // how you use the item (swinging, holding out, etc)
            item.noMelee = true; //so the item's animation doesn't do damage
            item.knockBack = 0; // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
            item.value = 500000; // how much the item sells for (measured in copper)
            item.rare = 10; // the color that the item's name will be in-game
            item.UseSound = SoundID.Item91.WithPitchVariance(Main.rand.NextFloat(-0.1f, 0.1f)).WithVolume(0.67f); // The sound that this item plays when used.
            item.autoReuse = true; // if you can hold click to automatically use it again
            item.shoot = ProjectileID.MagnetSphereBolt;
            item.shootSpeed = 16f; // the speed of the projectile (measured in pixels per frame)
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("Glaxion"), 1);
            recipe.AddIngredient(ItemID.FragmentVortex, 8);
            recipe.AddTile(412);
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
            var projectile = Main.projectile[proj];
            projectile.magic = false;
            projectile.ranged = true;
            var globalProj = Main.projectile[proj].GetGlobalProjectile<Projectiles.wdfeerGlobalProj>();
            globalProj.glaxionProcs = 38;
            globalProj.glaxionVandal = true;


            return false;
        }
    }
}