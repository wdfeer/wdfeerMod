using Terraria;
using Terraria.DataStructures;
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
            Item.damage = 10;
            Item.crit = 26;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 64;
            Item.height = 19;
            Item.scale = 1f;
            Item.useTime = 51;
            Item.useAnimation = 51;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 2;
            Item.value = 15000;
            Item.rare = 3;
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
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                Item.crit = 0;
                Item.shootSpeed = 16f;
            }
            else
            {
                Item.crit = 26;
                Item.shootSpeed = 20f;
            }
            return base.CanUseItem(player);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.SilverBar, 16);
            recipe.AddIngredient(ItemID.Bone, 8);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.TungstenBar, 16);
            recipe.AddIngredient(ItemID.Bone, 8);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.altFunctionUse != 2)
            {
                pathToSound = "Sounds/CorinthSound";
                PlaySound(Main.rand.NextFloat(0f, 0.1f), 0.65f);
                for (int i = 0; i < 5; i++)
                {
                    var proj = ShootWith(position, speedX, speedY, type, damage, knockBack, 0.08f, Item.width);
                    proj.GetGlobalProjectile<Projectiles.wfGlobalProj>().critMult = 1.4f;
                }
            }
            else
            {
                var projectile = ShootWith(position, speedX, speedY, Mod.Find<ModProjectile>("CorinthAltProj").Type, damage * 7 / 2, knockBack * 2, sound: SoundID.Item61);
                projectile.GetGlobalProjectile<Projectiles.wfGlobalProj>().critMult = 0.8f;
            }
            return false;
        }
    }
}