using Terraria;
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
            item.damage = 45;
            item.crit = 12;
            item.ranged = true;
            item.width = 27;
            item.height = 14;
            item.scale = 1.3f;
            item.useTime = 30;
            item.useAnimation = 30;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.noMelee = true;
            item.knockBack = 5;
            item.value = Item.sellPrice(gold: 1);
            item.rare = 3;
            item.autoReuse = false;
            item.shoot = 10;
            item.shootSpeed = 20f;
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
                item.useTime = 72;
                item.useAnimation = 72;
            }
            else
            {
                item.useTime = 30;
                item.useAnimation = 30;
            }
            return base.CanUseItem(player);
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.PlatinumBar, 3);
            recipe.AddIngredient(ItemID.HellstoneBar, 8);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (player.altFunctionUse == 2)
            {
                pathToSound = "Sounds/PrismaAngstrumAltSound";
                PlaySound(Main.rand.NextFloat(-0.08f, 0.08f));

                for (int i = 0; i < 3; i++)
                {
                    var proj = ShootWith(position, speedX, speedY, ModContent.ProjectileType<Projectiles.AngstrumProj>(), damage, knockBack, 0.18f, item.width);
                }
            }
            else
            {
                pathToSound = "Sounds/PrismaAngstrumSound";
                PlaySound(Main.rand.NextFloat(0, 0.1f));

                var proj = ShootWith(position, speedX, speedY, ModContent.ProjectileType<Projectiles.AngstrumProj>(), damage, knockBack, 0.005f, item.width, SoundID.Item72.WithVolume(0.6f));
            }
            return false;
        }
    }
}