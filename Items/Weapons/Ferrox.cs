using Terraria;
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
            item.damage = 177;
            item.crit = 28;
            item.magic = true;
            item.mana = 13;
            item.width = 95;
            item.height = 6;
            item.scale = 1f;
            item.useTime = 40;
            item.useAnimation = 40;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.noMelee = true;
            item.knockBack = 4;
            item.value = 50000;
            item.rare = 7;
            item.autoReuse = true;
            item.shoot = ProjectileID.MagnetSphereBolt;
            item.shootSpeed = 16f;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-20, 3);
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Nanites, 16);
            recipe.AddIngredient(ItemID.Gungnir, 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            var pitch = Main.rand.NextFloat(-0.1f, 0.1f);
            PlaySound(pitch);

            var proj = ShootWith(position, speedX, speedY, type, damage, knockBack, offset: item.width - 20);
            var globalProj = proj.GetGlobalProjectile<Projectiles.wfGlobalProj>();
            globalProj.critMult = 1.4f;
            proj.penetrate = 3;
            proj.usesLocalNPCImmunity = true;
            proj.localNPCHitCooldown = -1;

            return false;
        }
    }
}