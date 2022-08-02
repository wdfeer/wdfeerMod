using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;

namespace wfMod.Items.Weapons
{
    public class Ferrox : wfWeapon
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Shoots a powerful, penetrating beam\n+40% Critical Damage");
        }
        public override void SetDefaults()
        {
            pathToSound = "Sounds/FerroxSound";
            Item.damage = 177;
            Item.crit = 28;
            Item.DamageType = DamageClass.Magic;
            Item.mana = 13;
            Item.width = 95;
            Item.height = 6;
            Item.scale = 1f;
            Item.useTime = 40;
            Item.useAnimation = 40;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 4;
            Item.value = 50000;
            Item.rare = 7;
            Item.autoReuse = true;
            Item.shoot = ProjectileID.MagnetSphereBolt;
            Item.shootSpeed = 16f;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-20, 3);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Nanites, 16);
            recipe.AddIngredient(ItemID.Gungnir, 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            var pitch = Main.rand.NextFloat(-0.1f, 0.1f);
            PlaySound(pitch);

            var proj = ShootWith(position, speedX, speedY, type, damage, knockBack, offset: Item.width - 20);
            var globalProj = proj.GetGlobalProjectile<Projectiles.wfGlobalProj>();
            globalProj.critMult = 1.4f;
            proj.penetrate = 3;
            proj.usesLocalNPCImmunity = true;
            proj.localNPCHitCooldown = -1;

            return false;
        }
    }
}