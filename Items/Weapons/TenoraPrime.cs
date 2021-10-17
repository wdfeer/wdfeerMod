using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;

namespace wfMod.Items.Weapons
{
    public class TenoraPrime : wfWeapon
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Shoots rapidly with +10% critical damage and near-perfect accuraccy after spooling up\nRight Click to shoot a single shot with 7x damage and +50% critical damage\n70% Chance not to consume ammo");
        }
        int baseFireRate = 18;
        int spooledFireRate = 5;
        public override void SetDefaults()
        {
            item.damage = 43;
            item.crit = 26;
            item.ranged = true;
            item.width = 60;
            item.height = 16;
            item.useTime = baseFireRate;
            item.useAnimation = baseFireRate;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.noMelee = true;
            item.knockBack = 4f;
            item.value = Item.buyPrice(gold: 5);
            item.rare = 5;
            item.autoReuse = true;
            item.shoot = 10;
            item.shootSpeed = 16f;
            item.useAmmo = AmmoID.Bullet;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-0.75f, 0.3f);
        }
        public override bool ConsumeAmmo(Player player)
        {
            if (Main.rand.Next(0, 100) <= 70) return false;
            return base.ConsumeAmmo(player);
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("Tenora"));
            recipe.AddIngredient(ItemID.FragmentVortex, 8);
            recipe.AddIngredient(ItemID.HallowedBar, 15);
            recipe.AddTile(412);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        int lastShotTime = 0;
        int timeSinceLastShot => Main.player[item.owner].GetModPlayer<wfPlayer>().longTimer - lastShotTime;
        int spooledShots = 1;
        float spreadMult => 1f / (spooledShots <= 16 ? (float)Math.Sqrt(spooledShots) : spooledShots / 4);
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse != 2)
            {
                item.autoReuse = true;
                item.crit = 26;
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
                lastShotTime = player.GetModPlayer<wfPlayer>().longTimer;
            }
            else
            {
                item.crit = 36;
                item.useTime = 40;
                item.useAnimation = 40;
                item.autoReuse = false;

                lastShotTime = -1;
            }

            return base.CanUseItem(player);
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            var proj = ShootWith(position, speedX, speedY, type, damage, knockBack, (player.altFunctionUse == 2 ? 0 : 0.09f * spreadMult), 57, sound: (player.altFunctionUse == 2 ? SoundID.Item40 : SoundID.Item11));
            var gProj = proj.GetGlobalProjectile<Projectiles.wfGlobalProj>();
            if (player.altFunctionUse == 2)
            {
                if (proj.penetrate != -1) proj.penetrate += 1;
                proj.damage *= 7;
                proj.knockBack *= 4;
                gProj.critMult = 1.5f;
            }
            else
            {
                gProj.critMult = 1.1f;
                gProj.AddProcChance(new ProcChance(mod.BuffType("SlashProc"), 7));
            }
            return false;
        }
    }
}