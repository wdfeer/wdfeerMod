using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;

namespace wfMod.Items.Weapons
{
    public class Quatz : wfWeapon
    {
        Random rand = new Random();
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Right Click to use the Burst fire mode\nIn auto has a 27% Electricity proc chance and 70% chance not to consume ammo\nGains a bonus to critical stats, damage and accuraccy in Burst mode");
        }
        public override void SetDefaults()
        {
            item.damage = 3;
            item.crit = 9;
            item.ranged = true;
            item.width = 32;
            item.height = 19;
            item.scale = 0.8f;
            item.useTime = 4;
            item.useAnimation = 4;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.noMelee = true;
            item.knockBack = 0;
            item.value = 1500;
            item.rare = 3;
            item.UseSound = SoundID.Item11.WithVolume(0.6f);
            item.autoReuse = true;
            item.shoot = 10;
            item.shootSpeed = 16f;
            item.useAmmo = AmmoID.Bullet; // The "ammo Id" of the ammo item that this weapon uses. Note that this is not an item Id, but just a magic value.
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            
            recipe.AddIngredient(ItemID.ShadowScale, 12);
            recipe.AddIngredient(ItemID.TungstenBar, 8);
            
            recipe.AddTile(TileID.Hellforge);
            recipe.SetResult(this);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            
            recipe.AddIngredient(ItemID.ShadowScale, 12);
            recipe.AddIngredient(ItemID.SilverBar, 8);
            
            recipe.AddTile(TileID.Hellforge);
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
        public override bool ConsumeAmmo(Player player)
        {
            if (player.altFunctionUse != 2 && Main.rand.Next(100) < 70)
                return false;
            return base.ConsumeAmmo(player);
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            for (int i = 0; i < (player.altFunctionUse == 2 ? 4 : 1); i++)
            {
                float spreadMult = player.altFunctionUse == 2 ? 0.012f : 0.024f;
                var projectile = ShootWith(position, speedX, speedY, type, damage, knockBack, spreadMult, item.width);
                var globalProj = projectile.GetGlobalProjectile<Projectiles.wfGlobalProj>();
                if (player.altFunctionUse == 2)
                {
                    globalProj.critMult = 1.25f;
                    projectile.damage = (int)(projectile.damage * 1.5f);
                    projectile.knockBack += 2f;
                }                   
                else globalProj.AddProcChance(new ProcChance(BuffID.Electrified, 27));
            }

            return false;
        }
    }
}