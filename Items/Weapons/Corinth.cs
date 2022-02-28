using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;

namespace wfMod.Items.Weapons
{
    public class Corinth : wfWeapon
    {
        Random rand = new Random();
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Right Click to fire a projectile that explodes in the air\n+40% Critical Damage");
        }
        public override void SetDefaults()
        {
            item.damage = 10;
            item.crit = 26;
            item.ranged = true;
            item.width = 64;
            item.height = 19;
            item.scale = 1f;
            item.useTime = 51;
            item.useAnimation = 51;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.noMelee = true;
            item.knockBack = 2;
            item.value = 15000;
            item.rare = 3;
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
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                item.crit = 0;
                item.shootSpeed = 16f;
            }
            else
            {
                item.crit = 26;
                item.shootSpeed = 20f;
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
                pathToSound = "Sounds/CorinthSound";
                PlaySound(Main.rand.NextFloat(0f, 0.1f), 0.65f);
                for (int i = 0; i < 5; i++)
                {
                    var proj = ShootWith(position, speedX, speedY, type, damage, knockBack, 0.08f, item.width);
                    proj.GetGlobalProjectile<Projectiles.wfGlobalProj>().critMult = 1.4f;
                }
            }
            else
            {
                var projectile = ShootWith(position, speedX, speedY, mod.ProjectileType("CorinthAltProj"), damage * 7 / 2, knockBack * 2, sound: SoundID.Item61);
                projectile.GetGlobalProjectile<Projectiles.wfGlobalProj>().critMult = 0.8f;
            }
            return false;
        }
    }
}