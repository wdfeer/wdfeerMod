using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;

namespace wfMod.Items.Weapons
{
    public class Phantasma : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Shoots a chaining beam\nRight Click to cast a slow explosive projectile that deals 6x the damage\nHas a 37% chance to confuse enemies\n-25% Critical Damage");
        }
        public override void SetDefaults()
        {
            Item.damage = 44;
            Item.crit = 0;
            Item.DamageType = DamageClass.Magic;
            Item.width = 40;
            Item.height = 23;
            Item.useTime = 5;
            Item.useAnimation = 5;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 0;
            Item.value = Item.buyPrice(gold: 4);
            Item.rare = 8;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.PhantasmaProj>();
            Item.shootSpeed = 16f;
            Item.mana = 5;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                Item.mana = 22;
                Item.useTime = 30;
                Item.useAnimation = 30;
            }
            else
            {
                Item.mana = 5;
                Item.useTime = 5;
                Item.useAnimation = 5;
            }
            return base.CanUseItem(player);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.altFunctionUse == 2)
            {
                damage *= 6;
                speedX *= 0.9f;
                speedY *= 0.9f;
                type = Mod.Find<ModProjectile>("PhantasmaProj2").Type;

                SoundEngine.PlaySound(SoundID.Item117.WithVolume(0.6f), position);
            }
            else
            {
                SoundEngine.PlaySound(SoundID.Item72.WithPitchVariance(-0.2f).WithVolume(0.4f), position);
            }

            Vector2 spawnOffset = new Vector2(speedX, speedY);
            spawnOffset.Normalize();
            spawnOffset *= 50;
            position += spawnOffset;

            int proj = Projectile.NewProjectile(position, new Vector2(speedX, speedY), type, damage, knockBack, Main.LocalPlayer.cHead);
            var globalProj = Main.projectile[proj].GetGlobalProjectile<Projectiles.wfGlobalProj>();
            globalProj.critMult = 0.75f;
            globalProj.AddProcChance(new ProcChance(BuffID.Confused, 37));
            return false;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.SpectreBar, 9);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}