using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace wfMod.Items.Weapons
{
    public class Nukor : wdfeerWeapon
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Will confuse enemies \n+100% Critical Damage\nLimited range");
        }
        public override void SetDefaults()
        {
            item.damage = 7; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
            item.crit = -2;
            item.magic = true;
            item.width = 32; // hitbox width of the item
            item.height = 24; // hitbox height of the item
            item.useTime = 6; // The item's use time in ticks (60 ticks == 1 second.)
            item.useAnimation = 6; // The length of the item's use animation in ticks (60 ticks == 1 second.)
            item.useStyle = ItemUseStyleID.HoldingOut; // how you use the item (swinging, holding out, etc)
            item.noMelee = true; //so the item's animation doesn't do damage
            item.knockBack = 0; // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
            item.value = Item.buyPrice(silver: 45); // how much the item sells for (measured in copper)
            item.rare = 3; // the color that the item's name will be in-game
            item.autoReuse = true; // if you can hold click to automatically use it again
            item.shoot = ModContent.ProjectileType<Projectiles.NukorProj>();
            item.shootSpeed = 16f; // the speed of the projectile (measured in pixels per frame)
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
        SoundEffectInstance sound;
        int shots = 0;
        Vector2 spawnVelocity;
        Vector2 spawnPosOffset;
        float dustSpawnDistance = 1f;
        public override bool CanUseItem(Player player)
        {
            timeSinceLastShot = player.GetModPlayer<wdfeerPlayer>().longTimer - lastShotTime;
            lastShotTime = player.GetModPlayer<wdfeerPlayer>().longTimer;
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
            spawnPosOffset = position + wdfeerWeapon.findOffset(speedX, speedY, item.width + 1) - player.position;
            spawnVelocity = new Vector2(speedX, speedY);
            var proj = ShootWith(position, speedX, speedY, type, damage, knockBack, offset: item.width + 1);
            var globalProj = proj.GetGlobalProjectile<Projectiles.wdfeerGlobalProj>();
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