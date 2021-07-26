using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;

namespace wdfeerMod.Items.Weapons
{
    public class Quatz : ModItem
    {
        Random rand = new Random();
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Right Click to use the Burst fire mode\nIn auto has a 27% Electricity proc chance\nGains a bonus to critical stats in Burst mode");
        }
        public override void SetDefaults()
        {
            item.damage = 2; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
            item.crit = 9;
            item.ranged = true; // sets the damage type to ranged
            item.width = 32; // hitbox width of the item
            item.height = 19; // hitbox height of the item
            item.scale = 0.8f;
            item.useTime = 4; // The item's use time in ticks (60 ticks == 1 second.)
            item.useAnimation = 4; // The length of the item's use animation in ticks (60 ticks == 1 second.)
            item.useStyle = ItemUseStyleID.HoldingOut; // how you use the item (swinging, holding out, etc)
            item.noMelee = true; //so the item's animation doesn't do damage
            item.knockBack = 0; // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
            item.value = 1500; // how much the item sells for (measured in copper)
            item.rare = 3; // the color that the item's name will be in-game
            item.UseSound = SoundID.Item11.WithVolume(0.6f); // The sound that this item plays when used.
            item.autoReuse = true; // if you can hold click to automatically use it again
            item.shoot = 10; //idk why but all the guns in the vanilla source have this
            item.shootSpeed = 16f; // the speed of the projectile (measured in pixels per frame)
            item.useAmmo = AmmoID.Bullet; // The "ammo Id" of the ammo item that this weapon uses. Note that this is not an item Id, but just a magic value.
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            // ItemType<ExampleItem>() is how to get the ExampleItem item, 10 is the amount of that item you need to craft the recipe
            recipe.AddIngredient(ItemID.ShadowScale, 12);
            recipe.AddIngredient(ItemID.TungstenBar, 8);
            // You can use recipe.AddIngredient(ItemID.TheItemYouWantToUse, the amount of items needed); for a vanilla item.
            recipe.AddTile(TileID.Hellforge); // Set the crafting tile to ExampleWorkbench
            recipe.SetResult(this); // Set the result to this item (ExampleSword)
            recipe.AddRecipe(); // When your done, add the recipe

            recipe = new ModRecipe(mod);
            // ItemType<ExampleItem>() is how to get the ExampleItem item, 10 is the amount of that item you need to craft the recipe
            recipe.AddIngredient(ItemID.ShadowScale, 12);
            recipe.AddIngredient(ItemID.SilverBar, 8);
            // You can use recipe.AddIngredient(ItemID.TheItemYouWantToUse, the amount of items needed); for a vanilla item.
            recipe.AddTile(TileID.Hellforge); // Set the crafting tile to ExampleWorkbench
            recipe.SetResult(this); // Set the result to this item (ExampleSword)
            recipe.AddRecipe(); // When your done, add the recipe
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                item.useTime = 23;
                item.useAnimation = 23;
                item.crit = 23;
                item.shootSpeed = 20f;
                item.autoReuse = false;
                item.UseSound = SoundID.Item11;
            }
            else
            {
                item.useTime = 4;
                item.useAnimation = 4;
                item.crit = 9;
                item.shootSpeed = 16f;
                item.autoReuse = true;
                item.UseSound = SoundID.Item11.WithVolume(0.6f);
            }
            return base.CanUseItem(player);
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 spread = new Vector2(speedY, -speedX);

            for (int i = 0; i < (player.altFunctionUse == 2 ? 4 : 1); i++)
            {
                var proj = Main.projectile[Projectile.NewProjectile(position, new Vector2(speedX, speedY) + spread * Main.rand.NextFloat(-0.012f, 0.012f), type, damage, knockBack, Main.LocalPlayer.cHead)];
                var globalProj = proj.GetGlobalProjectile<Projectiles.wdfeerGlobalProj>();
                if (player.altFunctionUse == 2)
                    globalProj.critMult = 1.25f;
                else globalProj.electroChance = 27;
            }

            return false;
        }
    }
}