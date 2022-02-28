using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;

namespace wfMod.Items.Weapons
{
    public class Fluctus : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("16% Slash Proc chance \nInfinite Punch Through");
        }
        public override void SetDefaults()
        {
            item.damage = 90;
            item.crit = 18;
            item.ranged = true;
            item.width = 91;
            item.height = 96;
            item.scale = 0.8f;
            item.useTime = 12;
            item.useAnimation = 12;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.noMelee = true;
            item.knockBack = 7;
            item.value = Item.buyPrice(gold: 13);
            item.rare = 9;
            item.autoReuse = true;
            item.UseSound = SoundID.Item38.WithVolume(0.4f);
            item.shoot = ModContent.ProjectileType<Projectiles.FluctusProj>();
            item.shootSpeed = 30f;
        }
        public override Vector2? HoldoutOffset()
        {
            return null;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.InfluxWaver);
            recipe.AddIngredient(ItemID.ShroomiteBar, 16);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 spawnOffset = new Vector2(speedX, speedY);
            spawnOffset.Normalize();
            spawnOffset *= item.width;
            position += spawnOffset;

            int proj = Projectile.NewProjectile(position, new Vector2(speedX, speedY), type, damage, knockBack, Main.LocalPlayer.cHead);
            var projectile = Main.projectile[proj];
            projectile.GetGlobalProjectile<Projectiles.wfGlobalProj>().AddProcChance(new ProcChance(mod.BuffType("SlashProc"), 16));
            float rotation = Convert.ToSingle(-Math.Atan2(speedX, speedY));
            projectile.rotation = rotation;

            return false;
        }
    }
}