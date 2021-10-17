using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;

namespace wfMod.Items.Weapons
{
    public class Synapse : wfWeapon
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("13% ichor chance\n +35% Critical Damage");
        }
        public override void SetDefaults()
        {
            item.damage = 14; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
            item.crit = 35;
            item.magic = true;
            item.mana = 3;
            item.width = 48; // hitbox width of the item
            item.height = 11; // hitbox height of the item
            item.useTime = 5; // The item's use time in ticks (60 ticks == 1 second.)
            item.useAnimation = 5; // The length of the item's use animation in ticks (60 ticks == 1 second.)
            item.useStyle = ItemUseStyleID.HoldingOut; // how you use the item (swinging, holding out, etc)
            item.noMelee = true; //so the item's animation doesn't do damage
            item.knockBack = 0; // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
            item.value = Item.buyPrice(gold: 3); // how much the item sells for (measured in copper)
            item.rare = 4; // the color that the item's name will be in-game
            item.UseSound = SoundID.Item91.WithPitchVariance(-0.2f).WithVolume(0.6f); // The sound that this item plays when used.
            item.autoReuse = true; // if you can hold click to automatically use it again
            item.shoot = mod.ProjectileType("NukorProj");
            item.shootSpeed = 16f; // the speed of the projectile (measured in pixels per frame)
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.SoulofNight, 4);
            recipe.AddIngredient(ItemID.Ichor, 8);
            recipe.AddIngredient(ItemID.MythrilBar, 8);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.SoulofNight, 4);
            recipe.AddIngredient(ItemID.Ichor, 8);
            recipe.AddIngredient(ItemID.OrichalcumBar, 8);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            var proj = ShootWith(position, speedX, speedY, type, damage, knockBack, offset: item.width);
            proj.timeLeft = 60;
            var globalProj = proj.GetGlobalProjectile<Projectiles.wfGlobalProj>();
            globalProj.AddProcChance(new ProcChance(BuffID.Ichor, 13));

            return false;
        }
    }
}