using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;

namespace wfMod.Items.Weapons
{
    public class Baza : wfWeapon
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("75% Chance not to consume ammo\n+50% Critical Damage");
        }
        public override void SetDefaults()
        {
            item.damage = 1;
            item.crit = 22;
            item.ranged = true;
            item.width = 45;
            item.height = 18;
            item.useTime = 4;
            item.useAnimation = 4;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.noMelee = true;
            item.knockBack = 0;
            item.value = 1500;
            item.rare = 3;
            item.UseSound = SoundID.Item11.WithVolume(0.1f);
            item.autoReuse = true;
            item.shoot = 10;
            item.shootSpeed = 16f;
            item.useAmmo = AmmoID.Bullet; // The "ammo Id" of the ammo item that this weapon uses. Note that this is not an item Id, but just a magic value.
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-4,0);
        }
        public override bool ConsumeAmmo(Player player)
        {
            if (Main.rand.Next(0,100) <= 75) return false;
            return base.ConsumeAmmo(player);
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("Furis"));
            recipe.AddIngredient(ItemID.ShadowScale,8);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("Furis"));
            recipe.AddIngredient(ItemID.TissueSample,8);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            var proj = ShootWith(position, speedX, speedY, type, damage, knockBack, 0.001f, item.width);
            proj.GetGlobalProjectile<Projectiles.wfGlobalProj>().critMult = 1.5f;
            return false;
        }
    }
}