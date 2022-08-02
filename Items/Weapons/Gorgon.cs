using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;

namespace wfMod.Items.Weapons
{
    public class Gorgon : wfWeapon
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Takes a while to spool up, shoots rapidly but inaccurately\n-25% Critical Damage\n40% Chance not to consume ammo");
        }
        public override void SetDefaults()
        {
            Item.damage = 6;
            Item.crit = 13;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 50;
            Item.height = 19;
            Item.useTime = 22;
            Item.useAnimation = 22;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 1;
            Item.value = Item.buyPrice(gold: 3);
            Item.rare = 3;
            Item.UseSound = SoundID.Item11.WithVolume(0.75f);
            Item.autoReuse = true;
            Item.shoot = 10;
            Item.shootSpeed = 14f;
            Item.useAmmo = AmmoID.Bullet;
        }
        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            if (Main.rand.Next(0, 100) <= 40) return false;
            return base.CanConsumeAmmo(player);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Minishark);
            recipe.AddIngredient(ItemID.JungleSpores, 9);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
        int lastShotTime = 0;
        int timeSinceLastShot = 60;
        public override bool CanUseItem(Player player)
        {
            timeSinceLastShot = player.GetModPlayer<wfPlayer>().longTimer - lastShotTime;
            if (Item.useTime > 6)
            {
                Item.useTime -= 2;
                Item.useAnimation -= 2;
                if (Item.useTime < 6)
                {
                    Item.useTime = 6;
                    Item.useAnimation = 6;
                }
            }
            else if (timeSinceLastShot > 14)
            {
                Item.useTime += timeSinceLastShot / 3;
                Item.useAnimation += timeSinceLastShot / 3;
                if (Item.useTime > 22)
                {
                    Item.useTime = 22;
                    Item.useAnimation = 22;
                }
            }
            lastShotTime = player.GetModPlayer<wfPlayer>().longTimer;

            return base.CanUseItem(player);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            var proj = ShootWith(position, speedX, speedY, type, damage, knockBack, (timeSinceLastShot > 40 ? 0.03f : 0.08f), 52);
            var gProj = proj.GetGlobalProjectile<Projectiles.wfGlobalProj>();
            gProj.critMult = 0.75f;
            return false;
        }
    }
}