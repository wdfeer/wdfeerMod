using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;

namespace wdfeerMod.Items.Weapons
{
    public class Tenora : wdfeerWeapon
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Shoots rapidly and precisely after spooling up\nRight Click to shoot a single shot with 6x damage and +50% critical damage\n70% Chance not to consume ammo");
        }
        int baseFireRate = 18;
        int spooledFireRate => Main.rand.Next(5,6);
        public override void SetDefaults()
        {
            item.damage = 16;
            item.crit = 24;
            item.ranged = true;
            item.width = 56;
            item.height = 10;
            item.useTime = baseFireRate;
            item.useAnimation = baseFireRate;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.noMelee = true;
            item.knockBack = 1.9f;
            item.value = Item.buyPrice(gold: 5);
            item.rare = 5;
            item.autoReuse = true;
            item.shoot = 10;
            item.shootSpeed = 16f;
            item.useAmmo = AmmoID.Bullet;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-0.5f, 0.2f);
        }
        public override bool ConsumeAmmo(Player player)
        {
            if (Main.rand.Next(0, 100) <= 70) return false;
            return base.ConsumeAmmo(player);
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Megashark);
            recipe.AddIngredient(mod.ItemType("Pandero"), 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        int lastShotTime = 0;
        int timeSinceLastShot => Main.player[item.owner].GetModPlayer<wdfeerPlayer>().longTimer - lastShotTime;
        int spooledShots = 1;
        float spreadMult => 1f / (spooledShots <= 16 ? (float)Math.Sqrt(Math.Sqrt(spooledShots)) : spooledShots / 8);
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse != 2)
            {
                item.autoReuse = true;
                item.crit = 24;
                if (lastShotTime == -1)
                {
                    item.useTime = baseFireRate;
                    item.useAnimation = baseFireRate;
                }

                if (item.useTime > spooledFireRate)
                {
                    item.useTime -= 3;
                    item.useAnimation -= 3;
                    if (item.useTime < spooledFireRate)
                    {
                        item.useTime = spooledFireRate;
                        item.useAnimation = spooledFireRate;
                    }
                }
                else if (timeSinceLastShot > 16)
                {
                    item.useTime += timeSinceLastShot / 3;
                    item.useAnimation += timeSinceLastShot / 3;
                    if (item.useTime > baseFireRate)
                    {
                        item.useTime = baseFireRate;
                        item.useAnimation = baseFireRate;
                    }
                }

                if (timeSinceLastShot < spooledFireRate + 3)
                    spooledShots++;
                else spooledShots = 1;
                lastShotTime = player.GetModPlayer<wdfeerPlayer>().longTimer;
            }
            else
            {
                item.crit = 30;
                item.useTime = 44;
                item.useAnimation = 44;
                item.autoReuse = false;

                lastShotTime = -1;
            }

            return base.CanUseItem(player);
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            var proj = ShootWith(position, speedX, speedY, type, damage, knockBack, (player.altFunctionUse == 2 ? 0 : 0.1f * spreadMult), 57, sound: (player.altFunctionUse == 2 ? SoundID.Item40 : SoundID.Item11));
            var gProj = proj.GetGlobalProjectile<Projectiles.wdfeerGlobalProj>();
            if (player.altFunctionUse == 2)
            {
                if (proj.penetrate != -1) proj.penetrate += 1;
                proj.damage *= 6;
                proj.knockBack *= 3;
                gProj.critMult = 1.5f;
            }
            return false;
        }
    }
}