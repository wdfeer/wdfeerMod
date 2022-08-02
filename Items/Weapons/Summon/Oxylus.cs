using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
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
            ItemID.Sets.GamepadWholeScreenUseRange[Item.type] = true; // This lets the player target anywhere on the whole screen while using a controller.
            ItemID.Sets.LockOnIgnoresCollision[Item.type] = true;
        }

        public override void SetDefaults()
        {
            Item.damage = 76;
            Item.knockBack = 5f;
            Item.mana = 9;
            Item.width = 23;
            Item.height = 37;
            Item.noUseGraphic = true;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.value = Item.buyPrice(0, 9, 0, 0);
            Item.rare = 8;
            Item.UseSound = SoundID.Item44;

            // These below are needed for a minion weapon
            Item.noMelee = true;
            Item.DamageType = DamageClass.Summon;
            Item.buffType = ModContent.BuffType<Buffs.OxylusBuff>();
            // No buffTime because otherwise the item tooltip would say something like "1 minute duration"
            Item.shoot = ModContent.ProjectileType<Projectiles.Minions.Oxylus>();
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.HasBuff(Item.buffType)) return false;
            player.AddBuff(Item.buffType, 2);

            position = Main.MouseWorld;
            return true;
        }
    }
}