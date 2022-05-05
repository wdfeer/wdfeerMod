using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
namespace wfMod.Items.Weapons.Summon
{
    public class Oxylus : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Oxylus");
            Tooltip.SetDefault("Summons the Oxylus Sentinel to fight for you while you fish\n25% Electricity chance\nCan be affected by fire rate bonuses\nUses 2 minion slots\nOnly one Oxylus can be active at a time");
            ItemID.Sets.GamepadWholeScreenUseRange[item.type] = true; // This lets the player target anywhere on the whole screen while using a controller.
            ItemID.Sets.LockOnIgnoresCollision[item.type] = true;
        }

        public override void SetDefaults()
        {
            item.damage = 76;
            item.knockBack = 5f;
            item.mana = 9;
            item.width = 23;
            item.height = 37;
            item.noUseGraphic = true;
            item.useTime = 30;
            item.useAnimation = 30;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.value = Item.buyPrice(0, 9, 0, 0);
            item.rare = 8;
            item.UseSound = SoundID.Item44;

            // These below are needed for a minion weapon
            item.noMelee = true;
            item.summon = true;
            item.buffType = ModContent.BuffType<Buffs.OxylusBuff>();
            // No buffTime because otherwise the item tooltip would say something like "1 minute duration"
            item.shoot = ModContent.ProjectileType<Projectiles.Minions.Oxylus>();
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (player.HasBuff(item.buffType)) return false;
            player.AddBuff(item.buffType, 2);

            position = Main.MouseWorld;
            return true;
        }
    }
}