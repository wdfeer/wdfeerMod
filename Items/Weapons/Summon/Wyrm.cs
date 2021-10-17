using System;
using Microsoft.Xna.Framework;
using Terraria;
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
            ItemID.Sets.GamepadWholeScreenUseRange[item.type] = true; // This lets the player target anywhere on the whole screen while using a controller.
            ItemID.Sets.LockOnIgnoresCollision[item.type] = true;
        }

        public override void SetDefaults()
        {
            item.damage = 25;
            item.knockBack = 6f;
            item.mana = 10;
            item.width = 18;
            item.height = 32;
            item.noUseGraphic = true;
            item.useTime = 36;
            item.useAnimation = 36;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.value = Item.buyPrice(0, 3, 0, 0);
            item.rare = 3;
            item.UseSound = SoundID.Item44;

            // These below are needed for a minion weapon
            item.noMelee = true;
            item.summon = true;
            item.buffType = ModContent.BuffType<Buffs.WyrmBuff>();
            // No buffTime because otherwise the item tooltip would say something like "1 minute duration"
            item.shoot = ModContent.ProjectileType<Projectiles.Minions.Wyrm>();
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
            recipe.AddIngredient(ItemID.SpaceGun, 1);
            recipe.AddIngredient(ItemID.Bone, 8);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}