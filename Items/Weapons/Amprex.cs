using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;

namespace wfMod.Items.Weapons
{
    public class Amprex : wfWeapon
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Inflicts Electricity debuffs\n+10% Critical Damage\nLimited range, chains between nearby enemies");
        }
        public override void SetDefaults()
        {
            Item.damage = 26;
            Item.crit = 28;
            Item.DamageType = DamageClass.Magic;
            Item.width = 45;
            Item.height = 10;
            Item.useTime = 5;
            Item.useAnimation = 5;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 0;
            Item.value = Item.buyPrice(gold: 5);
            Item.rare = 7;
            Item.UseSound = SoundID.Item93.WithPitchVariance(0f).WithVolume(0.1f);
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.AmprexProj>();
            Item.shootSpeed = 16f;
            Item.mana = 4;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.TitaniumBar,14);
            recipe.AddIngredient(ItemID.Nanites, 16);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.AdamantiteBar,14);
            recipe.AddIngredient(ItemID.Nanites, 16);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            var projectile = ShootWith(position,speedX,speedY,type,damage,knockBack, offset: 64, sound: SoundID.Item91.WithVolume(0.3f));
            var globalProj = projectile.GetGlobalProjectile<Projectiles.wfGlobalProj>();
            globalProj.critMult = 1.1f;
            globalProj.AddProcChance(new ProcChance(BuffID.Electrified, 22));

            return false;
        }
    }
}