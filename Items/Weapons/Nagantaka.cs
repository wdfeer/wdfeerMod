using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;

namespace wdfeerMod.Items.Weapons
{
    public class Nagantaka : wdfeerWeapon
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("A semi-automatic dart shooter\nRight Click to shoot a burst of five darts that each deal 80% damage\n32% Slash chance\n+15% Critical Damage");
        }
        public override void SetDefaults()
        {
            item.damage = 54; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
            item.crit = 11;
            item.ranged = true; // sets the damage type to ranged
            item.width = 42; // hitbox width of the item
            item.height = 19; // hitbox height of the item
            item.useTime = 25; // The item's use time in ticks (60 ticks == 1 second.)
            item.useAnimation = 25; // The length of the item's use animation in ticks (60 ticks == 1 second.)
            item.useStyle = ItemUseStyleID.HoldingOut; // how you use the item (swinging, holding out, etc)
            item.noMelee = true; //so the item's animation doesn't do damage
            item.knockBack = 3.9f; // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
            item.value = Item.buyPrice(gold: 2); // how much the item sells for (measured in copper)
            item.rare = 5; // the color that the item's name will be in-game
            item.autoReuse = false; // if you can hold click to automatically use it again
            item.shoot = 10; //idk why but all the guns in the vanilla source have this
            item.shootSpeed = 16f; // the speed of the projectile (measured in pixels per frame)
            item.useAmmo = AmmoID.Dart; // The "ammo Id" of the ammo item that this weapon uses. Note that this is not an item Id, but just a magic value.
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.TitaniumBar, 9);
            recipe.AddIngredient(ItemID.SoulofNight, 12);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.AdamantiteBar, 9);
            recipe.AddIngredient(ItemID.SoulofNight, 12);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                item.useTime = 63;
                item.useAnimation = 63;
            }
            else
            {
                item.useTime = 25;
                item.useAnimation = 25;
            }
            return base.CanUseItem(player);
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            float spreadMult = player.altFunctionUse == 2 ? 0.005f : 0.015f;
            var projectile = ShootWith(position, speedX, speedY, type, player.altFunctionUse != 2 ? damage : (int)(damage * 0.8f), knockBack, spreadMult, 44, SoundID.Item98.WithVolume(0.8f), bursts: (player.altFunctionUse == 2 ? 5 : -1), burstInterval: (player.altFunctionUse == 2 ? item.useTime / 8 : -1));
            var globalProj = projectile.GetGlobalProjectile<Projectiles.wdfeerGlobalProj>();
            globalProj.critMult = 1.15f;
            globalProj.procChances.Add(new ProcChance(mod.BuffType("SlashProc"), 32));

            return false;
        }
    }
}