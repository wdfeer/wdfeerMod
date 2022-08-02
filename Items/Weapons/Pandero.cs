using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;

namespace wfMod.Items.Weapons
{
    public class Pandero : wfWeapon
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Right Click to shoot 8 bullets in a rapid burst with very low accuraccy and lowered damage\n+40% Critical Damage");
        }
        public override void SetDefaults()
        {
            Item.damage = 17;
            Item.crit = 26;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 30;
            Item.height = 17;
            Item.useTime = 20;
            Item.useAnimation = Item.useTime;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 5.75f;
            Item.value = Item.buyPrice(gold: 2);
            Item.rare = 3;
            Item.autoReuse = false;
            Item.shoot = 10;
            Item.shootSpeed = 17f;
            Item.useAmmo = AmmoID.Bullet;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.FlintlockPistol);
            recipe.AddIngredient(ItemID.DemoniteBar, 7);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.FlintlockPistol);
            recipe.AddIngredient(ItemID.CrimtaneBar, 7);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                Item.useTime = 40;
                Item.useAnimation = 40;
            }
            else
            {
                Item.useTime = 20;
                Item.useAnimation = 20;
            }

            return base.CanUseItem(player);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int bursts = -1;
            int burstInt = -1;
            float spread = 0;
            if (player.altFunctionUse == 2)
            {
                bursts = 8;
                burstInt = 2;
                spread = 0.22f;

                damage = (int)(damage * 0.6f);
            }
            var proj = ShootWith(position, speedX, speedY, type, damage, knockBack, spread, 22, player.altFunctionUse == 2 ? SoundID.Item11 : SoundID.Item41, bursts, burstInt);
            if (player.altFunctionUse != 2)
            {
                proj.velocity *= 0.8f;
                proj.extraUpdates += 1;
            }
            var gProj = proj.GetGlobalProjectile<Projectiles.wfGlobalProj>();
            gProj.critMult = 1.4f;
            return false;
        }
    }
}