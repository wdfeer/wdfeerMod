using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ID;
namespace wfMod.Items.Weapons.Summon
{
    public class Carrier : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Carrier");
            Tooltip.SetDefault("Summons a Carrier Sentinel to fight and save ammo for you\nShoots 4 pellets once every second\nCan be affected by fire rate bonuses\nOnly one Carrier can be active at a time");
            ItemID.Sets.GamepadWholeScreenUseRange[Item.type] = true; // This lets the player target anywhere on the whole screen while using a controller.
            ItemID.Sets.LockOnIgnoresCollision[Item.type] = true;
        }

        public override void SetDefaults()
        {
            Item.damage = 8;
            Item.knockBack = 4f;
            Item.mana = 10;
            Item.width = 18;
            Item.height = 32;
            Item.noUseGraphic = true;
            Item.useTime = 36;
            Item.useAnimation = 36;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.value = Item.buyPrice(0, 3, 0, 0);
            Item.rare = 2;
            Item.UseSound = SoundID.Item44;

            // These below are needed for a minion weapon
            Item.noMelee = true;
            Item.DamageType = DamageClass.Summon;
            Item.buffType = ModContent.BuffType<Buffs.CarrierBuff>();
            // No buffTime because otherwise the item tooltip would say something like "1 minute duration"
            Item.shoot = ModContent.ProjectileType<Projectiles.Minions.Carrier>();
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.HasBuff(Item.buffType)) return false;
            player.AddBuff(Item.buffType, 2);

            position = Main.MouseWorld;
            return true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Boomstick, 1);
            recipe.AddIngredient(ItemID.DemoniteBar, 16);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Boomstick, 1);
            recipe.AddIngredient(ItemID.CrimtaneBar, 16);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}