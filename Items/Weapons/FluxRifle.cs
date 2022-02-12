using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;

namespace wfMod.Items.Weapons
{
    public class FluxRifle : wfWeapon
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Doesn't consume ammo\n50% Slash chance");
        }
        public override void SetDefaults()
        {
            item.damage = 8; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
            item.crit = 6;
            item.ranged = true; // sets the damage type to ranged
            item.width = 45; // hitbox width of the item
            item.height = 16; // hitbox height of the item
            item.useTime = 5; // The item's use time in ticks (60 ticks == 1 second.)
            item.useAnimation = 5; // The length of the item's use animation in ticks (60 ticks == 1 second.)
            item.useStyle = ItemUseStyleID.HoldingOut; // how you use the item (swinging, holding out, etc)
            item.noMelee = true; //so the item's animation doesn't do damage
            item.knockBack = 0; // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
            item.value = Item.sellPrice(gold: 2); // how much the item sells for (measured in copper)
            item.rare = 3; // the color that the item's name will be in-game
            item.UseSound = SoundID.Item11.WithVolume(0.32f).WithPitchVariance(0.6f); // The sound that this item plays when used.
            item.autoReuse = true; // if you can hold click to automatically use it again
            item.shoot = ModContent.ProjectileType<Projectiles.FluxRifleProj>(); ; //idk why but all the guns in the vanilla source have this
            item.shootSpeed = 16f; // the speed of the projectile (measured in pixels per frame)
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-4, 0);
        }
        public override void AddRecipes()
        {
            var recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Minishark);
            recipe.AddIngredient(ItemID.TissueSample, 12);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            var gProj = ShootWith(position, speedX, speedY, type, damage, knockBack, 0.0025f, item.width).GetGlobalProjectile<Projectiles.wfGlobalProj>();
            gProj.AddProcChance(new ProcChance(mod.BuffType("SlashProc"), 50));
            return false;
        }
    }
}