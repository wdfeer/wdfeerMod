using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using Microsoft.Xna.Framework.Audio;

namespace wfMod.Items.Weapons
{
    public class TenetArcaPlasmor : wfWeapon
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Shoots a piercing projectile\nMight confuse enemies");
        }
        public override void SetDefaults()
        {
            pathToSound = "Sounds/TenetArcaPlasmorSound";
            Item.damage = 697;
            Item.crit = 18;
            Item.DamageType = DamageClass.Magic;
            Item.mana = 12;
            Item.width = 48;
            Item.height = 16;
            Item.useTime = 60;
            Item.useAnimation = 60;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 7;
            Item.value = 150000;
            Item.rare = 10;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.ArcaPlasmorProj>();
            Item.shootSpeed = 30f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(Mod.Find<ModItem>("ArcaPlasmor").Type, 1);
            recipe.AddIngredient(Mod.Find<ModItem>("Fieldron").Type);
            recipe.AddIngredient(ItemID.FragmentNebula, 8);

            recipe.AddTile(412);
            recipe.Register();
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            PlaySound(Main.rand.NextFloat(-0.15f, 0.1f));

            var projectile = ShootWith(position, speedX, speedY, type, damage, knockBack, offset: Item.width);
            projectile.GetGlobalProjectile<Projectiles.wfGlobalProj>().AddProcChance(new ProcChance(31, 34));
            projectile.extraUpdates = 1;
            projectile.timeLeft = 44;
            var modProj = projectile.ModProjectile as Projectiles.ArcaPlasmorProj;
            modProj.tenet = true;
            float rotation = Convert.ToSingle(-Math.Atan2(speedX, speedY));
            projectile.rotation = rotation;

            return false;
        }
    }
}