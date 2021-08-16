using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;

namespace wdfeerMod.Items.Weapons
{
    public class Pandero : wdfeerWeapon
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Right Click to shoot 8 bullets in a rapid burst with very low accuraccy and lowered damage\n+40% Critical Damage");
        }
        public override void SetDefaults()
        {
            item.damage = 18;
            item.crit = 26;
            item.ranged = true;
            item.width = 30;
            item.height = 17;
            item.useTime = 20;
            item.useAnimation = item.useTime;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.noMelee = true;
            item.knockBack = 5.75f;
            item.value = Item.buyPrice(gold: 2);
            item.rare = 3;
            item.autoReuse = false;
            item.shoot = 10;
            item.shootSpeed = 17f;
            item.useAmmo = AmmoID.Bullet;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.FlintlockPistol);
            recipe.AddIngredient(ItemID.DemoniteBar, 7);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.FlintlockPistol);
            recipe.AddIngredient(ItemID.CrimtaneBar, 7);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                item.useTime = 40;
                item.useAnimation = 40;
            }
            else
            {
                item.useTime = 20;
                item.useAnimation = 20;
            }

            return base.CanUseItem(player);
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int bursts = -1;
            int burstInt = -1;
            float spread = 0;
            if (player.altFunctionUse == 2)
            {
                bursts = 8;
                burstInt = 2;
                spread = 0.22f;

                damage = (int)(damage * 0.75f);
            }
            var proj = ShootWith(position, speedX, speedY, type, damage, knockBack, spread, 22, player.altFunctionUse == 2 ? SoundID.Item11 : SoundID.Item41, bursts, burstInt);
            if (player.altFunctionUse != 2)
            {
                proj.velocity *= 0.8f;
                proj.extraUpdates += 1;
            }
            var gProj = proj.GetGlobalProjectile<Projectiles.wdfeerGlobalProj>();
            gProj.critMult = 1.4f;
            return false;
        }
    }
}