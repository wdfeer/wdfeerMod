using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;

namespace wfMod.Items.Weapons
{
    public class Angstrum : wfWeapon
    {
        Random rand = new Random();
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Shoot a single rocket with LMB\nRight click to fire 3 rockets at once");
        }
        public override void SetDefaults()
        {
            Item.damage = 45;
            Item.crit = 12;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 27;
            Item.height = 14;
            Item.scale = 1.3f;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 5;
            Item.value = Item.sellPrice(gold: 1);
            Item.rare = 3;
            Item.autoReuse = false;
            Item.shoot = 10;
            Item.shootSpeed = 20f;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-1.4f, 1.4f);
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                Item.useTime = 72;
                Item.useAnimation = 72;
            }
            else
            {
                Item.useTime = 30;
                Item.useAnimation = 30;
            }
            return base.CanUseItem(player);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.PlatinumBar, 3);
            recipe.AddIngredient(ItemID.HellstoneBar, 8);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.altFunctionUse == 2)
            {
                pathToSound = "Sounds/PrismaAngstrumAltSound";
                PlaySound(Main.rand.NextFloat(-0.08f, 0.08f));

                for (int i = 0; i < 3; i++)
                {
                    var proj = ShootWith(position, speedX, speedY, ModContent.ProjectileType<Projectiles.AngstrumProj>(), damage, knockBack, 0.18f, Item.width);
                }
            }
            else
            {
                pathToSound = "Sounds/PrismaAngstrumSound";
                PlaySound(Main.rand.NextFloat(0, 0.1f));

                var proj = ShootWith(position, speedX, speedY, ModContent.ProjectileType<Projectiles.AngstrumProj>(), damage, knockBack, 0.005f, Item.width, SoundID.Item72.WithVolume(0.6f));
            }
            return false;
        }
    }
}