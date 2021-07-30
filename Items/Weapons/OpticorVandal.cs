using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;

namespace wdfeerMod.Items.Weapons
{
    public class OpticorVandal : wdfeerWeapon
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Quickly charges to shoot a powerful beam\n+30% Critical Damage");
        }
        public override void SetDefaults()
        {
            item.damage = 600; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
            item.crit = 20;
            item.magic = true; // sets the damage type to ranged
            item.mana = 42;
            item.width = 48; // hitbox width of the item
            item.height = 16; // hitbox height of the item
            item.useTime = 48; // The item's use time in ticks (60 ticks == 1 second.)
            item.useAnimation = 48; // The length of the item's use animation in ticks (60 ticks == 1 second.)
            item.useStyle = ItemUseStyleID.HoldingOut; // how you use the item (swinging, holding out, etc)
            item.noMelee = true; //so the item's animation doesn't do damage
            item.knockBack = 20; // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
            item.value = Item.buyPrice(gold: 15); // how much the item sells for (measured in copper)
            item.rare = 10; // the color that the item's name will be in-game
            item.autoReuse = false; // if you can hold click to automatically use it again
            item.shoot = mod.ProjectileType("OpticorProj");
            item.shootSpeed = 16f; // the speed of the projectile (measured in pixels per frame)
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("Opticor"), 1);
            recipe.AddIngredient(ItemID.FragmentNebula, 12);
            recipe.AddTile(412);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 velocity = new Vector2(speedX, speedY);
            var proj = ShootWith(position, speedX, speedY, type, damage, knockBack, offset: item.width);
            proj.timeLeft = 128;
            var globalProj = proj.GetGlobalProjectile<Projectiles.wdfeerGlobalProj>();
            globalProj.critMult = 1.3f;
            globalProj.baseVelocity = velocity;
            globalProj.v2 = proj.position - Main.LocalPlayer.position;

            Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/OpticorVandalSound"));
            return false;
        }
    }
}