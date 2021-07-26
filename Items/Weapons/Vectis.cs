using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;

namespace wdfeerMod.Items.Weapons
{
    public class Vectis : ModItem
    {
        Random rand = new Random();
        public override void SetDefaults()
        {
            item.damage = 88; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
            item.crit = 21;
            item.ranged = true; // sets the damage type to ranged
            item.width = 63; // hitbox width of the item
            item.height = 17; // hitbox height of the item
            item.useTime = 60; // The item's use time in ticks (60 ticks == 1 second.)
            item.useAnimation = 60; // The length of the item's use animation in ticks (60 ticks == 1 second.)
            item.useStyle = ItemUseStyleID.HoldingOut; // how you use the item (swinging, holding out, etc)
            item.noMelee = true; //so the item's animation doesn't do damage
            item.knockBack = 6; // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
            item.value = 15000; // how much the item sells for (measured in copper)
            item.rare = 4; // the color that the item's name will be in-game
            item.UseSound = SoundID.Item40; // The sound that this item plays when used.
            item.autoReuse = false; // if you can hold click to automatically use it again
            item.shootSpeed = 48f; // the speed of the projectile (measured in pixels per frame)
            item.shoot = ProjectileID.SniperBullet;
            item.useAmmo = AmmoID.Bullet; // The "ammo Id" of the ammo item that this weapon uses. Note that this is not an item Id, but just a magic value.
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            // ItemType<ExampleItem>() is how to get the ExampleItem item, 10 is the amount of that item you need to craft the recipe
            recipe.AddIngredient(ItemID.MeteoriteBar, 12);
            recipe.AddIngredient(ItemID.Wire, 8);
            recipe.AddIngredient(ItemID.Musket, 1);
            // You can use recipe.AddIngredient(ItemID.TheItemYouWantToUse, the amount of items needed); for a vanilla item.
            recipe.AddTile(TileID.Anvils); // Set the crafting tile to ExampleWorkbench
            recipe.SetResult(this); // Set the result to this item (ExampleSword)
            recipe.AddRecipe(); // When your done, add the recipe

        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 spawnOffset = new Vector2(speedX, speedY);
            spawnOffset.Normalize();
            spawnOffset *= item.width;

            position += spawnOffset;
            var proj = Main.projectile[Projectile.NewProjectile(position, new Vector2(speedX, speedY), ProjectileID.BulletHighVelocity, damage, knockBack, Main.LocalPlayer.cHead)];
            proj.usesLocalNPCImmunity = true;
            proj.localNPCHitCooldown = -1;
            return false;
        }
    }
}