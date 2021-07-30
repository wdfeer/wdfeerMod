using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;

namespace wdfeerMod.Items.Weapons
{
    public class Corinth : wdfeerWeapon
    {
        Random rand = new Random();
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Right Click to fire a projectile that explodes in the air\n+40% Critical Damage");
        }
        public override void SetDefaults()
        {
            item.damage = 9; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
            item.crit = 26;
            item.ranged = true; // sets the damage type to ranged
            item.width = 64; // hitbox width of the item
            item.height = 19; // hitbox height of the item
            item.scale = 1f;
            item.useTime = 51; // The item's use time in ticks (60 ticks == 1 second.)
            item.useAnimation = 51; // The length of the item's use animation in ticks (60 ticks == 1 second.)
            item.useStyle = ItemUseStyleID.HoldingOut; // how you use the item (swinging, holding out, etc)
            item.noMelee = true; //so the item's animation doesn't do damage
            item.knockBack = 1; // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
            item.value = 15000; // how much the item sells for (measured in copper)
            item.rare = 3; // the color that the item's name will be in-game
            item.UseSound = SoundID.Item36; // The sound that this item plays when used.
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
                item.shootSpeed = 16f;
                item.UseSound = SoundID.Item61;
            }
            else
            {
                item.crit = 26;
                item.shootSpeed = 20f;
                item.UseSound = SoundID.Item36;
            }
            return base.CanUseItem(player);
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.SilverBar, 16);
            recipe.AddIngredient(ItemID.Bone, 8);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.TungstenBar, 16);
            recipe.AddIngredient(ItemID.Bone, 8);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (player.altFunctionUse != 2)
            {
                for (int i = 0; i < 5; i++)
                {
                    var proj = ShootWith(position, speedX, speedY, type, damage, knockBack, 0.08f, item.width);
                    proj.GetGlobalProjectile<Projectiles.wdfeerGlobalProj>().critMult = 1.4f;
                }
            }
            else
            {
                int proj = Projectile.NewProjectile(position, new Vector2(speedX, speedY), mod.ProjectileType("CorinthAltProj"), damage * 7 / 2, knockBack, Main.LocalPlayer.cHead);
                var projectile = Main.projectile[proj];
                projectile.GetGlobalProjectile<Projectiles.wdfeerGlobalProj>().critMult = 0.8f;
            }
            return false;
        }
    }
}