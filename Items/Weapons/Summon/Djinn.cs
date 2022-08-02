using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ID;
namespace wfMod.Items.Weapons.Summon
{
    public class Djinn : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Djinn");
            Tooltip.SetDefault("Summons an infested Djinn Sentinel to shoot toxic darts 3 times per second for you\nCan be affected by fire rate bonuses\nUses 3 minion slots\nOnly one Djinn can be active at a time");
            ItemID.Sets.GamepadWholeScreenUseRange[Item.type] = true; // This lets the player target anywhere on the whole screen while using a controller.
            ItemID.Sets.LockOnIgnoresCollision[Item.type] = true;
        }

        public override void SetDefaults()
        {
            Item.damage = 49;
            Item.knockBack = 4f;
            Item.mana = 14;
            Item.width = 18;
            Item.height = 32;
            Item.noUseGraphic = true;
            Item.useTime = 36;
            Item.useAnimation = 36;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.value = Item.buyPrice(0, 3, 0, 0);
            Item.rare = 4;
            Item.UseSound = SoundID.Item44;

            // These below are needed for a minion weapon
            Item.noMelee = true;
            Item.DamageType = DamageClass.Summon;
            Item.buffType = ModContent.BuffType<Buffs.DjinnBuff>();
            // No buffTime because otherwise the item tooltip would say something like "1 minute duration"
            Item.shoot = ModContent.ProjectileType<Projectiles.Minions.Djinn>();
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
            recipe.AddIngredient(ItemID.SoulofNight, 16);
            recipe.AddIngredient(ItemID.Bone, 20);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}