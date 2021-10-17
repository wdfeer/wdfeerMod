using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;

namespace wfMod.Items.Weapons
{
    public class Ferrox : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Shoots a powerful, penetrating beam\n+40% Critical Damage");
        }
        public override void SetDefaults()
        {
            item.damage = 178; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
            item.crit = 28;
            item.magic = true; // sets the damage type to ranged
            item.mana = 13;
            item.width = 95; // hitbox width of the item
            item.height = 6; // hitbox height of the item
            item.scale = 1f;
            item.useTime = 40; // The item's use time in ticks (60 ticks == 1 second.)
            item.useAnimation = 40; // The length of the item's use animation in ticks (60 ticks == 1 second.)
            item.useStyle = ItemUseStyleID.HoldingOut; // how you use the item (swinging, holding out, etc)
            item.noMelee = true; //so the item's animation doesn't do damage
            item.knockBack = 4; // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
            item.value = 50000; // how much the item sells for (measured in copper)
            item.rare = 7; // the color that the item's name will be in-game
            item.UseSound = SoundID.Item125.WithPitchVariance(-0.3f).WithVolume(0.9f); // The sound that this item plays when used.
            item.autoReuse = true; // if you can hold click to automatically use it again
            item.shoot = ProjectileID.MagnetSphereBolt;
            item.shootSpeed = 16f; // the speed of the projectile (measured in pixels per frame)
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-20, 3);
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Nanites, 16);
            recipe.AddIngredient(ItemID.Gungnir, 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 spawnOffset = new Vector2(speedX, speedY);
            spawnOffset.Normalize();
            spawnOffset *= item.width - 20;

            position += spawnOffset;
            var proj = Main.projectile[Projectile.NewProjectile(position, new Vector2(speedX, speedY), type, damage, knockBack, Main.LocalPlayer.cHead)];
            var globalProj = proj.GetGlobalProjectile<Projectiles.wdfeerGlobalProj>();
            globalProj.critMult = 1.4f;
            proj.penetrate = 3;
            proj.usesLocalNPCImmunity = true;
            proj.localNPCHitCooldown = -1;

            return false;
        }
    }
}