using Terraria;
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
            item.damage = 7;
            item.crit = -2;
            item.magic = true;
            item.width = 32;
            item.height = 24;
            item.useTime = 6;
            item.useAnimation = 6;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.noMelee = true;
            item.knockBack = 0;
            item.value = Item.buyPrice(silver: 45);
            item.rare = 3;
            item.autoReuse = true;
            item.shoot = ModContent.ProjectileType<Projectiles.NukorProj>();
            item.shootSpeed = 16f;
            item.mana = 2;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.DemoniteBar, 8);
            recipe.AddRecipeGroup("IronBar", 12);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.CrimtaneBar, 8);
            recipe.AddRecipeGroup("IronBar", 12);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
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
        public override void UseStyle(Player player)
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
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            spawnPosOffset = position + wfWeapon.findOffset(speedX, speedY, item.width + 1) - player.position;
            spawnVelocity = new Vector2(speedX, speedY);
            var proj = ShootWith(position, speedX, speedY, type, damage, knockBack, offset: item.width + 1);
            var globalProj = proj.GetGlobalProjectile<Projectiles.wfGlobalProj>();
            globalProj.kill = (Projectile projectile, int timeLeft) =>
            {
                dustSpawnDistance = 1 - timeLeft / 44f;
            };
            globalProj.critMult = 2;

            if (timeSinceLastShot > 12)
            {
                sound = mod.GetSound("Sounds/KuvaNukorStartSound").CreateInstance();
                sound.Volume = 0.4f;
                sound.Play();
            }
            else if (shots % 2 == 0)
            {
                int rand = Main.rand.Next(3);

                switch (rand)
                {
                    case 0:
                        sound = mod.GetSound("Sounds/KuvaNukorLoop1").CreateInstance();
                        break;
                    case 1:
                        sound = mod.GetSound("Sounds/KuvaNukorLoop2").CreateInstance();
                        break;
                    default:
                        sound = mod.GetSound("Sounds/KuvaNukorLoop3").CreateInstance();
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