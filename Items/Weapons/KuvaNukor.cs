using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;

namespace wdfeerMod.Items.Weapons
{
    public class KuvaNukor : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Will confuse enemies\nCan hit up to 3 enemies\n+120% Critical Damage\nLimited range");
        }
        public override void SetDefaults()
        {
            item.damage = 17; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
            item.crit = 3;
            item.magic = true;
            item.width = 32; // hitbox width of the item
            item.height = 24; // hitbox height of the item
            item.useTime = 6; // The item's use time in ticks (60 ticks == 1 second.)
            item.useAnimation = 6; // The length of the item's use animation in ticks (60 ticks == 1 second.)
            item.useStyle = ItemUseStyleID.HoldingOut; // how you use the item (swinging, holding out, etc)
            item.noMelee = true; //so the item's animation doesn't do damage
            item.knockBack = 0; // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
            item.value = 20000; // how much the item sells for (measured in copper)
            item.rare = 4; // the color that the item's name will be in-game
            item.UseSound = SoundID.Item91.WithPitchVariance(Main.rand.NextFloat(-0.2f, 0.2f)).WithVolume(0.6f); // The sound that this item plays when used.
            item.autoReuse = true; // if you can hold click to automatically use it again
            item.shoot = ModContent.ProjectileType<Projectiles.NukorProj>();
            item.shootSpeed = 16f; // the speed of the projectile (measured in pixels per frame)
            item.mana = 3;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("Nukor"));
            recipe.AddIngredient(mod.ItemType("Kuva"), 7);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 spawnOffset = new Vector2(speedX, speedY);
            spawnOffset.Normalize();
            spawnOffset *= (item.width + 8);
            position += spawnOffset;

            int proj = Projectile.NewProjectile(position, new Vector2(speedX, speedY), type, damage, knockBack, Main.LocalPlayer.cHead);
            var a = 0;
            var b = false;
            Main.projectile[proj].modProjectile.ModifyHitPvp(Main.LocalPlayer, ref a, ref b);
            var globalProj = Main.projectile[proj].GetGlobalProjectile<Projectiles.wdfeerGlobalProj>();
            globalProj.critMult = 2.5f;
            globalProj.kuvaNukor = true;

            return false;
        }
    }
}