using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;

namespace wfMod.Items.Weapons
{
    public class ArcaPlasmor : wfWeapon
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Shoots a piercing projectile\nMight confuse enemies\n-20% Critical Damage");
        }
        public override void SetDefaults()
        {
            pathToSound = "Sounds/TenetArcaPlasmorSound";
            Item.damage = 222;
            Item.crit = 18;
            Item.DamageType = DamageClass.Magic;
            Item.mana = 15;
            Item.width = 48;
            Item.height = 16;
            Item.useTime = 54;
            Item.useAnimation = 54;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 7;
            Item.value = 150000;
            Item.rare = 4;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.ArcaPlasmorProj>();
            Item.shootSpeed = 30f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.CobaltBar, 22);
            recipe.AddIngredient(ItemID.SoulofLight, 12);

            recipe.AddTile(TileID.Anvils);
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.PalladiumBar, 22);
            recipe.AddIngredient(ItemID.SoulofLight, 12);

            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float pitch = Main.rand.NextFloat(-0.05f, 0.15f);
            PlaySound(pitch);

            var projectile = ShootWith(position, speedX, speedY, type, damage, knockBack, offset: Item.width);
            projectile.GetGlobalProjectile<Projectiles.wfGlobalProj>().critMult = 0.8f;
            projectile.GetGlobalProjectile<Projectiles.wfGlobalProj>().AddProcChance(new ProcChance(31, 28));
            float rotation = Convert.ToSingle(-Math.Atan2(speedX, speedY));
            projectile.rotation = rotation;

            return false;
        }
    }
}