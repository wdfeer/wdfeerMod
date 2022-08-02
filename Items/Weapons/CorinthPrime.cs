using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
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
            Item.damage = 21;
            Item.crit = 26;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 64;
            Item.height = 19;
            Item.useTime = 42;
            Item.useAnimation = 42;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 2;
            Item.value = 150000;
            Item.rare = 5;
            Item.autoReuse = false;
            Item.shoot = 10;
            Item.shootSpeed = 20f;
            Item.useAmmo = AmmoID.Bullet; // The "ammo Id" of the ammo item that this weapon uses. Note that this is not an item Id, but just a magic value.
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
            Item.noUseGraphic = false;
            if (player.altFunctionUse == 2)
            {
                Item.crit = 0;
                Item.useTime = 20;
                Item.useAnimation = 20;
                Item.shootSpeed = 12f;
                if (altFireProj == null || !altFireProj.active)
                {
                    if (player.GetModPlayer<wfPlayer>().longTimer - lastAltShoot < 2 * Item.useTime * Item.GetGlobalItem<wfGlobalItem>().UseTimeMultiplier(Item, player))
                        return false;
                }
                else Item.noUseGraphic = true;
            }
            else
            {
                Item.crit = 26;
                Item.useTime = 42;
                Item.useAnimation = 42;
                Item.shootSpeed = 20f;
            }
            return base.CanUseItem(player);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(Mod.Find<ModItem>("Corinth").Type, 1);
            recipe.AddIngredient(ItemID.HallowedBar, 8);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
        Projectile altFireProj;
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.altFunctionUse != 2)
            {
                sound = Mod.GetSound("Sounds/CorinthPrimeSound").CreateInstance();
                sound.Volume = 0.4f;
                sound.Pitch += Main.rand.NextFloat(0, 0.1f);
                sound.Play();

                Vector2 spread = new Vector2(speedY, -speedX);
                for (int i = 0; i < 5; i++)
                {
                    var projectile = ShootWith(position, speedX, speedY, type, damage, knockBack, 0.07f, 60);
                    projectile.GetGlobalProjectile<Projectiles.wfGlobalProj>().critMult = 1.4f;
                    projectile.GetGlobalProjectile<Projectiles.wfGlobalProj>().AddProcChance(new ProcChance(Mod.Find<ModBuff>("SlashProc").Type, 4));
                }
            }
            else
            {
                if (altFireProj == null || altFireProj.ModProjectile == null || !altFireProj.active)
                {
                    SoundEngine.PlaySound(SoundID.Item61);
                    lastAltShoot = player.GetModPlayer<wfPlayer>().longTimer;
                    int proj = Projectile.NewProjectile(position, new Vector2(speedX, speedY), Mod.Find<ModProjectile>("CorinthAltProj").Type, damage * 6, knockBack, Main.LocalPlayer.cHead);
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