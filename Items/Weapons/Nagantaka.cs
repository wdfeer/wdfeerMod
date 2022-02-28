using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;

namespace wfMod.Items.Weapons
{
    public class Nagantaka : wfWeapon
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("A semi-automatic dart shooter\nRight Click to shoot a burst of five darts that each deal 80% damage\n32% Slash chance\n+15% Critical Damage");
        }
        public override void SetDefaults()
        {
            item.damage = 54;
            item.crit = 11;
            item.ranged = true;
            item.width = 42;
            item.height = 19;
            item.useTime = 25;
            item.useAnimation = 25;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.noMelee = true;
            item.knockBack = 3.9f;
            item.value = Item.buyPrice(gold: 2);
            item.rare = 5;
            item.autoReuse = false;
            item.shoot = 10;
            item.shootSpeed = 16f;
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
            var globalProj = projectile.GetGlobalProjectile<Projectiles.wfGlobalProj>();
            globalProj.critMult = 1.15f;
            globalProj.AddProcChance(new ProcChance(mod.BuffType("SlashProc"), 32));

            return false;
        }
    }
}