using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;

namespace wdfeerMod.Items.Weapons
{
    public class Scourge : wdfeerWeapon
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Fires explosive projectiles");
        }
        public override void SetDefaults()
        {
            item.damage = 24; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
            item.crit = 0;
            item.magic = true; // sets the damage type to ranged
            item.mana = 5;
            item.width = 95; // hitbox width of the item
            item.height = 15; // hitbox height of the item
            item.scale = 1f;
            item.useTime = 23; // The item's use time in ticks (60 ticks == 1 second.)
            item.useAnimation = 23; // The length of the item's use animation in ticks (60 ticks == 1 second.)
            item.useStyle = ItemUseStyleID.HoldingOut; // how you use the item (swinging, holding out, etc)
            item.noMelee = true; //so the item's animation doesn't do damage
            item.knockBack = 4; // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
            item.value = Item.buyPrice(gold: 1); // how much the item sells for (measured in copper)
            item.rare = 3; // the color that the item's name will be in-game
            item.UseSound = SoundID.Item43; // The sound that this item plays when used.
            item.autoReuse = true; // if you can hold click to automatically use it again
            item.shoot = mod.ProjectileType("ScourgeProj");
            item.shootSpeed = 16f; // the speed of the projectile (measured in pixels per frame)
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-20, -4);
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);          
            recipe.AddIngredient(ItemID.MeteoriteBar, 9);
            recipe.AddIngredient(ItemID.Emerald, 3);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);          
            recipe.AddIngredient(ItemID.MeteoriteBar, 9);
            recipe.AddIngredient(ItemID.Ruby, 3);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);          
            recipe.AddIngredient(ItemID.MeteoriteBar, 9);
            recipe.AddIngredient(ItemID.Diamond, 3);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            var proj = ShootWith(position, speedX, speedY, type, damage, knockBack, offset: item.width - 20);
            var globalProj = proj.GetGlobalProjectile<Projectiles.wdfeerGlobalProj>();
            globalProj.critMult = 1.4f;
            proj.penetrate = 3;
            proj.extraUpdates = 0;
            proj.usesLocalNPCImmunity = true;
            proj.localNPCHitCooldown = -1;

            return false;
        }
    }
}