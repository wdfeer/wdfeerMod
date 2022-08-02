using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace wfMod.Items.Weapons
{
    public class Nukor : wfWeapon
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Will confuse enemies \n+100% Critical Damage\nLimited range");
        }
        public override void SetDefaults()
        {
            Item.damage = 7;
            Item.crit = -2;
            Item.DamageType = DamageClass.Magic;
            Item.width = 32;
            Item.height = 24;
            Item.useTime = 6;
            Item.useAnimation = 6;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 0;
            Item.value = Item.buyPrice(silver: 45);
            Item.rare = 3;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.NukorProj>();
            Item.shootSpeed = 16f;
            Item.mana = 2;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.DemoniteBar, 8);
            recipe.AddRecipeGroup("IronBar", 12);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.CrimtaneBar, 8);
            recipe.AddRecipeGroup("IronBar", 12);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
        int lastShotTime = 0;
        int timeSinceLastShot = 60;
        int shots = 0;
        Vector2 spawnVelocity;
        Vector2 spawnPosOffset;
        float dustSpawnDistance = 1f;
        public override bool CanUseItem(Player player)
        {
            timeSinceLastShot = player.GetModPlayer<wfPlayer>().longTimer - lastShotTime;
            lastShotTime = player.GetModPlayer<wfPlayer>().longTimer;
            return base.CanUseItem(player);
        }
        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            if (timeSinceLastShot < 12)
            {
                for (int i = 0; i < 10 * dustSpawnDistance; i++)
                {
                    var dust = Dust.NewDustPerfect(player.position + spawnPosOffset + spawnVelocity * 44 * Main.rand.NextFloat(0, dustSpawnDistance), 162);
                    dust.scale = 1.2f;
                }
            }
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            spawnPosOffset = position + wfWeapon.findOffset(speedX, speedY, Item.width + 1) - player.position;
            spawnVelocity = new Vector2(speedX, speedY);
            var proj = ShootWith(position, speedX, speedY, type, damage, knockBack, offset: Item.width + 1);
            var globalProj = proj.GetGlobalProjectile<Projectiles.wfGlobalProj>();
            globalProj.kill = (Projectile projectile, int timeLeft) =>
            {
                dustSpawnDistance = 1 - timeLeft / 44f;
            };
            globalProj.critMult = 2;

            if (timeSinceLastShot > 12)
            {
                sound = Mod.GetSound("Sounds/KuvaNukorStartSound").CreateInstance();
                sound.Volume = 0.4f;
                sound.Play();
            }
            else if (shots % 2 == 0)
            {
                int rand = Main.rand.Next(3);

                switch (rand)
                {
                    case 0:
                        sound = Mod.GetSound("Sounds/KuvaNukorLoop1").CreateInstance();
                        break;
                    case 1:
                        sound = Mod.GetSound("Sounds/KuvaNukorLoop2").CreateInstance();
                        break;
                    default:
                        sound = Mod.GetSound("Sounds/KuvaNukorLoop3").CreateInstance();
                        break;
                }

                sound.Volume = 0.3f;
                sound.Play();
            }
            shots++;

            return false;
        }
    }
}