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
            item.damage = 90; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
            item.crit = 18;
            item.ranged = true; // sets the damage type to ranged
            item.width = 91; // hitbox width of the item
            item.height = 96; // hitbox height of the item
            item.scale = 0.8f;
            item.useTime = 12; // The item's use time in ticks (60 ticks == 1 second.)
            item.useAnimation = 12; // The length of the item's use animation in ticks (60 ticks == 1 second.)
            item.useStyle = ItemUseStyleID.HoldingOut; // how you use the item (swinging, holding out, etc)
            item.noMelee = true; //so the item's animation doesn't do damage
            item.knockBack = 7; // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
            item.value = Item.buyPrice(gold: 13); // how much the item sells for (measured in copper)
            item.rare = 9; // the color that the item's name will be in-game
            item.autoReuse = true; // if you can hold click to automatically use it again
            item.UseSound = SoundID.Item38.WithVolume(0.4f);
            item.shoot = ModContent.ProjectileType<Projectiles.FluctusProj>();
            item.shootSpeed = 30f; // the speed of the projectile (measured in pixels per frame)
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