using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ID;
namespace wfMod.Items.Weapons.Summon
{
    public class Wyrm : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Wyrm");
            Tooltip.SetDefault("Summons a Wyrm Sentinel to shoot lasers for you\nPeriodically releases a high-knockback shockwave\nCan be affected by fire rate bonuses\nOnly one Wyrm can be active at a time");
            ItemID.Sets.GamepadWholeScreenUseRange[Item.type] = true; // This lets the player target anywhere on the whole screen while using a controller.
            ItemID.Sets.LockOnIgnoresCollision[Item.type] = true;
        }

        public override void SetDefaults()
        {
            Item.damage = 25;
            Item.knockBack = 6f;
            Item.mana = 10;
            Item.width = 18;
            Item.height = 32;
            Item.noUseGraphic = true;
            Item.useTime = 36;
            Item.useAnimation = 36;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.value = Item.buyPrice(0, 3, 0, 0);
            Item.rare = 3;
            Item.UseSound = SoundID.Item44;

            // These below are needed for a minion weapon
            Item.noMelee = true;
            Item.DamageType = DamageClass.Summon;
            Item.buffType = ModContent.BuffType<Buffs.WyrmBuff>();
            // No buffTime because otherwise the item tooltip would say something like "1 minute duration"
            Item.shoot = ModContent.ProjectileType<Projectiles.Minions.Wyrm>();
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
            recipe.AddIngredient(ItemID.SpaceGun, 1);
            recipe.AddIngredient(ItemID.Bone, 8);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}