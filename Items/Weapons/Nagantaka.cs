using Terraria;
using Terraria.DataStructures;
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
            Item.damage = 54;
            Item.crit = 11;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 42;
            Item.height = 19;
            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 3.9f;
            Item.value = Item.buyPrice(gold: 2);
            Item.rare = 5;
            Item.autoReuse = false;
            Item.shoot = 10;
            Item.shootSpeed = 16f;
            Item.useAmmo = AmmoID.Dart; // The "ammo Id" of the ammo item that this weapon uses. Note that this is not an item Id, but just a magic value.
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.TitaniumBar, 9);
            recipe.AddIngredient(ItemID.SoulofNight, 12);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.AdamantiteBar, 9);
            recipe.AddIngredient(ItemID.SoulofNight, 12);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                Item.useTime = 63;
                Item.useAnimation = 63;
            }
            else
            {
                Item.useTime = 25;
                Item.useAnimation = 25;
            }
            return base.CanUseItem(player);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float spreadMult = player.altFunctionUse == 2 ? 0.005f : 0.015f;
            var projectile = ShootWith(position, speedX, speedY, type, player.altFunctionUse != 2 ? damage : (int)(damage * 0.8f), knockBack, spreadMult, 44, SoundID.Item98.WithVolume(0.8f), bursts: (player.altFunctionUse == 2 ? 5 : -1), burstInterval: (player.altFunctionUse == 2 ? Item.useTime / 8 : -1));
            var globalProj = projectile.GetGlobalProjectile<Projectiles.wfGlobalProj>();
            globalProj.critMult = 1.15f;
            globalProj.AddProcChance(new ProcChance(Mod.Find<ModBuff>("SlashProc").Type, 32));

            return false;
        }
    }
}