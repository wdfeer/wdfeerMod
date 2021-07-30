using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;

namespace wdfeerMod.Items.Weapons
{
    public class CorinthPrime : ModItem
    {
        Random rand = new Random();
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Right Click to fire a manually exploding projectile\n+40% Critical Damage \n4% Slash Proc chance per pellet");
        }
        public override void SetDefaults()
        {
            item.damage = 21; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
            item.crit = 26;
            item.ranged = true; // sets the damage type to ranged
            item.width = 64; // hitbox width of the item
            item.height = 19; // hitbox height of the item
            item.scale = 1f;
            item.useTime = 42; // The item's use time in ticks (60 ticks == 1 second.)
            item.useAnimation = 42; // The length of the item's use animation in ticks (60 ticks == 1 second.)
            item.useStyle = ItemUseStyleID.HoldingOut; // how you use the item (swinging, holding out, etc)
            item.noMelee = true; //so the item's animation doesn't do damage
            item.knockBack = 1; // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
            item.value = 150000; // how much the item sells for (measured in copper)
            item.rare = 5; // the color that the item's name will be in-game
            item.UseSound = SoundID.Item36.WithVolume(0.9f); // The sound that this item plays when used.
            item.autoReuse = true; // if you can hold click to automatically use it again
            item.shoot = 10; //idk why but all the guns in the vanilla source have this
            item.shootSpeed = 20f; // the speed of the projectile (measured in pixels per frame)
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
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                item.crit = 0;
                item.useTime = 40;
                item.useAnimation = 40;
                item.shootSpeed = 12f;
                item.UseSound = null;
            }
            else
            {
                item.crit = 26;
                item.useTime = 42;
                item.useAnimation = 42;
                item.shootSpeed = 20f;
                item.UseSound = SoundID.Item36;
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
                Vector2 spread = new Vector2(speedY, -speedX);
                for (int i = 0; i < 5; i++)
                {
                    int proj = Projectile.NewProjectile(position, new Vector2(speedX, speedY) + spread * Main.rand.NextFloat(-0.07f, 0.07f), type, damage, knockBack, Main.LocalPlayer.cHead);
                    var projectile = Main.projectile[proj];
                    projectile.GetGlobalProjectile<Projectiles.wdfeerGlobalProj>().critMult = 1.4f;
                    projectile.GetGlobalProjectile<Projectiles.wdfeerGlobalProj>().procChances.Add(new ProcChance(mod.BuffType("SlashProc"), 4));
                }
            }
            else
            {
                if (altFireProj == null || altFireProj.modProjectile == null || !altFireProj.active)
                {
                    Main.PlaySound(SoundID.Item61);

                    int proj = Projectile.NewProjectile(position, new Vector2(speedX, speedY), mod.ProjectileType("CorinthAltProj"), damage * 6, knockBack, Main.LocalPlayer.cHead);
                    altFireProj = Main.projectile[proj];
                    altFireProj.timeLeft = 80;
                    altFireProj.GetGlobalProjectile<Projectiles.wdfeerGlobalProj>().critMult = 0.8f;
                }
                else
                {
                    altFireProj.modProjectile.OnHitPvp(Main.LocalPlayer, 0, false);
                }
            }
            return false;
        }
    }
}