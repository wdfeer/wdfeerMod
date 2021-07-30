using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
namespace wdfeerMod.Items.Weapons.Summon
{
    public class Djinn : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Djinn");
            Tooltip.SetDefault("Summons an infested Djinn Sentinel to shoot toxic darts for you\nUses 3 minion slots\nOnly one Djinn can be active at a time");
            ItemID.Sets.GamepadWholeScreenUseRange[item.type] = true; // This lets the player target anywhere on the whole screen while using a controller.
            ItemID.Sets.LockOnIgnoresCollision[item.type] = true;
        }

        public override void SetDefaults()
        {
            item.damage = 49;
            item.knockBack = 4f;
            item.mana = 10;
            item.width = 18;
            item.height = 32;
            item.noUseGraphic = true;
            item.useTime = 36;
            item.useAnimation = 36;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.value = Item.buyPrice(0, 3, 0, 0);
            item.rare = 4;
            item.UseSound = SoundID.Item44;

            // These below are needed for a minion weapon
            item.noMelee = true;
            item.summon = true;
            item.buffType = ModContent.BuffType<Buffs.DjinnBuff>();
            // No buffTime because otherwise the item tooltip would say something like "1 minute duration"
            item.shoot = ModContent.ProjectileType<Projectiles.Minions.Djinn>();
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (player.HasBuff(item.buffType)) return false;
            player.AddBuff(item.buffType, 2);

            position = Main.MouseWorld;
            return true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.SoulofNight, 16);
            recipe.AddIngredient(ItemID.Bone, 20);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}