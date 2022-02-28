using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;

namespace wfMod.Items.Weapons
{
    public class CorinthPrime : wfWeapon
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Right Click to fire a manually exploding projectile\n+40% Critical Damage \n4% Slash Proc chance per pellet");
        }
        public override void SetDefaults()
        {
            item.damage = 21;
            item.crit = 26;
            item.ranged = true;
            item.width = 64;
            item.height = 19;
            item.useTime = 42;
            item.useAnimation = 42;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.noMelee = true;
            item.knockBack = 2;
            item.value = 150000;
            item.rare = 5;
            item.autoReuse = false;
            item.shoot = 10;
            item.shootSpeed = 20f;
            item.useAmmo = AmmoID.Bullet; // The "ammo Id" of the ammo item that this weapon uses. Note that this is not an item Id, but just a magic value.
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-4, 0);
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public int lastAltShoot;
        public override bool CanUseItem(Player player)
        {
            item.noUseGraphic = false;
            if (player.altFunctionUse == 2)
            {
                item.crit = 0;
                item.useTime = 20;
                item.useAnimation = 20;
                item.shootSpeed = 12f;
                if (altFireProj == null || !altFireProj.active)
                {
                    if (player.GetModPlayer<wfPlayer>().longTimer - lastAltShoot < 2 * item.useTime * item.GetGlobalItem<wfGlobalItem>().UseTimeMultiplier(item, player))
                        return false;
                }
                else item.noUseGraphic = true;
            }
            else
            {
                item.crit = 26;
                item.useTime = 42;
                item.useAnimation = 42;
                item.shootSpeed = 20f;
            }
            return base.CanUseItem(player);
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("Corinth"), 1);
            recipe.AddIngredient(ItemID.HallowedBar, 8);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        Projectile altFireProj;
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (player.altFunctionUse != 2)
            {
                sound = mod.GetSound("Sounds/CorinthPrimeSound").CreateInstance();
                sound.Volume = 0.4f;
                sound.Pitch += Main.rand.NextFloat(0, 0.1f);
                sound.Play();

                Vector2 spread = new Vector2(speedY, -speedX);
                for (int i = 0; i < 5; i++)
                {
                    var projectile = ShootWith(position, speedX, speedY, type, damage, knockBack, 0.07f, 60);
                    projectile.GetGlobalProjectile<Projectiles.wfGlobalProj>().critMult = 1.4f;
                    projectile.GetGlobalProjectile<Projectiles.wfGlobalProj>().AddProcChance(new ProcChance(mod.BuffType("SlashProc"), 4));
                }
            }
            else
            {
                if (altFireProj == null || altFireProj.modProjectile == null || !altFireProj.active)
                {
                    Main.PlaySound(SoundID.Item61);
                    lastAltShoot = player.GetModPlayer<wfPlayer>().longTimer;
                    int proj = Projectile.NewProjectile(position, new Vector2(speedX, speedY), mod.ProjectileType("CorinthAltProj"), damage * 6, knockBack, Main.LocalPlayer.cHead);
                    altFireProj = Main.projectile[proj];
                    altFireProj.timeLeft = 80;
                    altFireProj.GetGlobalProjectile<Projectiles.wfGlobalProj>().critMult = 0.8f;
                }
                else
                {
                    altFireProj.timeLeft = 3;
                }
            }
            return false;
        }
    }
}