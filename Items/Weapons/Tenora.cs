using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;

namespace wfMod.Items.Weapons
{
    public class Tenora : wfWeapon
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Shoots rapidly and precisely after spooling up\nRight Click to shoot a single shot with 6x damage and +50% critical damage\n70% Chance not to consume ammo");
        }
        int baseFireRate = 18;
        int spooledFireRate => Main.rand.Next(5, 7);
        public override void SetDefaults()
        {
            Item.damage = 15;
            Item.crit = 24;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 56;
            Item.height = 10;
            Item.useTime = baseFireRate;
            Item.useAnimation = baseFireRate;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 1.9f;
            Item.value = Item.buyPrice(gold: 5);
            Item.rare = 5;
            Item.autoReuse = true;
            Item.shoot = 10;
            Item.shootSpeed = 16f;
            Item.useAmmo = AmmoID.Bullet;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-0.5f, 0.2f);
        }
        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            if (Main.rand.Next(0, 100) <= 70) return false;
            return base.CanConsumeAmmo(player);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Megashark);
            recipe.AddIngredient(Mod.Find<ModItem>("Pandero").Type, 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        int lastShotTime = 0;
        int timeSinceLastShot => Main.player[Item.playerIndexTheItemIsReservedFor].GetModPlayer<wfPlayer>().longTimer - lastShotTime;
        int spooledShots = 1;
        float spreadMult => 1f / (spooledShots <= 16 ? (float)Math.Sqrt(Math.Sqrt(spooledShots)) : spooledShots / 8);
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse != 2)
            {
                Item.autoReuse = true;
                Item.crit = 24;
                if (lastShotTime == -1)
                {
                    Item.useTime = baseFireRate;
                    Item.useAnimation = baseFireRate;
                }

                if (Item.useTime > spooledFireRate)
                {
                    Item.useTime -= 3;
                    Item.useAnimation -= 3;
                    if (Item.useTime < spooledFireRate)
                    {
                        Item.useTime = spooledFireRate;
                        Item.useAnimation = spooledFireRate;
                    }
                }
                else if (timeSinceLastShot > 16)
                {
                    Item.useTime += timeSinceLastShot / 3;
                    Item.useAnimation += timeSinceLastShot / 3;
                    if (Item.useTime > baseFireRate)
                    {
                        Item.useTime = baseFireRate;
                        Item.useAnimation = baseFireRate;
                    }
                }

                if (timeSinceLastShot < spooledFireRate + 3)
                    spooledShots++;
                else spooledShots = 1;
                lastShotTime = player.GetModPlayer<wfPlayer>().longTimer;
            }
            else
            {
                Item.crit = 30;
                Item.useTime = 44;
                Item.useAnimation = 44;
                Item.autoReuse = false;

                lastShotTime = -1;
            }

            return base.CanUseItem(player);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            bool secondary = player.altFunctionUse == 2;

            pathToSound = secondary ? "Sounds/TenoraPrimeSecondarySound" : "Sounds/TenoraPrimePrimarySound";
            PlaySound(Main.rand.NextFloat(-0.1f, 0.1f), 0.6f);

            var proj = ShootWith(position, speedX, speedY, type, damage, knockBack, (secondary ? 0 : 0.1f * spreadMult), 57);
            var gProj = proj.GetGlobalProjectile<Projectiles.wfGlobalProj>();
            if (secondary)
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