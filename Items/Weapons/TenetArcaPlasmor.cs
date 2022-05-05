using Terraria;
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
            item.damage = 697;
            item.crit = 18;
            item.magic = true;
            item.mana = 12;
            item.width = 48;
            item.height = 16;
            item.useTime = 60;
            item.useAnimation = 60;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.noMelee = true;
            item.knockBack = 7;
            item.value = 150000;
            item.rare = 10;
            item.autoReuse = true;
            item.shoot = ModContent.ProjectileType<Projectiles.ArcaPlasmorProj>();
            item.shootSpeed = 30f;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(mod.ItemType("ArcaPlasmor"), 1);
            recipe.AddIngredient(mod.ItemType("Fieldron"));
            recipe.AddIngredient(ItemID.FragmentNebula, 8);

            recipe.AddTile(412);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            PlaySound(Main.rand.NextFloat(-0.15f, 0.1f));

            var projectile = ShootWith(position, speedX, speedY, type, damage, knockBack, offset: item.width);
            projectile.GetGlobalProjectile<Projectiles.wfGlobalProj>().AddProcChance(new ProcChance(31, 34));
            projectile.extraUpdates = 1;
            projectile.timeLeft = 44;
            var modProj = projectile.modProjectile as Projectiles.ArcaPlasmorProj;
            modProj.tenet = true;
            float rotation = Convert.ToSingle(-Math.Atan2(speedX, speedY));
            projectile.rotation = rotation;

            return false;
        }
    }
}