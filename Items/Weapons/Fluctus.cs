using Terraria;
using Terraria.DataStructures;
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
            Item.damage = 90;
            Item.crit = 18;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 91;
            Item.height = 96;
            Item.scale = 0.8f;
            Item.useTime = 12;
            Item.useAnimation = 12;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 7;
            Item.value = Item.buyPrice(gold: 13);
            Item.rare = 9;
            Item.autoReuse = true;
            Item.UseSound = SoundID.Item38.WithVolume(0.4f);
            Item.shoot = ModContent.ProjectileType<Projectiles.FluctusProj>();
            Item.shootSpeed = 30f;
        }
        public override Vector2? HoldoutOffset()
        {
            return null;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.InfluxWaver);
            recipe.AddIngredient(ItemID.ShroomiteBar, 16);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 spawnOffset = new Vector2(speedX, speedY);
            spawnOffset.Normalize();
            spawnOffset *= Item.width;
            position += spawnOffset;

            int proj = Projectile.NewProjectile(position, new Vector2(speedX, speedY), type, damage, knockBack, Main.LocalPlayer.cHead);
            var projectile = Main.projectile[proj];
            projectile.GetGlobalProjectile<Projectiles.wfGlobalProj>().AddProcChance(new ProcChance(Mod.Find<ModBuff>("SlashProc").Type, 16));
            float rotation = Convert.ToSingle(-Math.Atan2(speedX, speedY));
            projectile.rotation = rotation;

            return false;
        }
    }
}