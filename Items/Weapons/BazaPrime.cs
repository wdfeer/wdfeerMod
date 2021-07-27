using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;

namespace wdfeerMod.Items.Weapons
{
    public class BazaPrime : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("80% Chance not to consume ammo\n+50% Critical Damage");
        }
        public override void SetDefaults()
        {
            item.damage = 14; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
            item.crit = 24;
            item.ranged = true; // sets the damage type to ranged
            item.width = 45; // hitbox width of the item
            item.height = 18; // hitbox height of the item
            item.useTime = 4; // The item's use time in ticks (60 ticks == 1 second.)
            item.useAnimation = 4; // The length of the item's use animation in ticks (60 ticks == 1 second.)
            item.useStyle = ItemUseStyleID.HoldingOut; // how you use the item (swinging, holding out, etc)
            item.noMelee = true; //so the item's animation doesn't do damage
            item.knockBack = 0; // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
            item.value = 1500; // how much the item sells for (measured in copper)
            item.rare = 7; // the color that the item's name will be in-game
            item.UseSound = SoundID.Item11.WithVolume(0.2f); // The sound that this item plays when used.
            item.autoReuse = true; // if you can hold click to automatically use it again
            item.shoot = 10; //idk why but all the guns in the vanilla source have this
            item.shootSpeed = 17f; // the speed of the projectile (measured in pixels per frame)
            item.useAmmo = AmmoID.Bullet; // The "ammo Id" of the ammo item that this weapon uses. Note that this is not an item Id, but just a magic value.
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-4,0);
        }
        public override bool ConsumeAmmo(Player player)
        {
            if (Main.rand.Next(0,100) <= 80) return false;
            return base.ConsumeAmmo(player);
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("Baza"));
            recipe.AddIngredient(ItemID.HallowedBar, 8);
            recipe.AddIngredient(ItemID.LihzahrdPowerCell,1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 spread = new Vector2(speedY, -speedX);
            var proj = Main.projectile[Projectile.NewProjectile(position, new Vector2(speedX, speedY) + spread * Main.rand.NextFloat(-0.001f, 0.001f), type, damage, knockBack, Main.LocalPlayer.cHead)];
            proj.GetGlobalProjectile<Projectiles.wdfeerGlobalProj>().critMult = 1.5f;
            return false;
        }
    }
}