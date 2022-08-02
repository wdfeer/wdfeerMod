using Terraria;
using Terraria.DataStructures;
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
            Item.damage = 3;
            Item.crit = 9;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 32;
            Item.height = 19;
            Item.scale = 0.8f;
            Item.useTime = 4;
            Item.useAnimation = 4;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 0;
            Item.value = 1500;
            Item.rare = 3;
            Item.UseSound = SoundID.Item11.WithVolume(0.6f);
            Item.autoReuse = true;
            Item.shoot = 10;
            Item.shootSpeed = 16f;
            Item.useAmmo = AmmoID.Bullet; // The "ammo Id" of the ammo item that this weapon uses. Note that this is not an item Id, but just a magic value.
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            
            recipe.AddIngredient(ItemID.ShadowScale, 12);
            recipe.AddIngredient(ItemID.TungstenBar, 8);
            
            recipe.AddTile(TileID.Hellforge);
            recipe.Register();

            recipe = CreateRecipe();
            
            recipe.AddIngredient(ItemID.ShadowScale, 12);
            recipe.AddIngredient(ItemID.SilverBar, 8);
            
            recipe.AddTile(TileID.Hellforge);
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
                Item.useTime = 23;
                Item.useAnimation = 23;
                Item.crit = 23;
                Item.shootSpeed = 20f;
                Item.autoReuse = false;
                Item.UseSound = SoundID.Item11;
            }
            else
            {
                Item.useTime = 4;
                Item.useAnimation = 4;
                Item.crit = 9;
                Item.shootSpeed = 16f;
                Item.autoReuse = true;
                Item.UseSound = SoundID.Item11.WithVolume(0.6f);
            }
            return base.CanUseItem(player);
        }
        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            if (player.altFunctionUse != 2 && Main.rand.Next(100) < 70)
                return false;
            return base.CanConsumeAmmo(player);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            for (int i = 0; i < (player.altFunctionUse == 2 ? 4 : 1); i++)
            {
                float spreadMult = player.altFunctionUse == 2 ? 0.012f : 0.024f;
                var projectile = ShootWith(position, speedX, speedY, type, damage, knockBack, spreadMult, Item.width);
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