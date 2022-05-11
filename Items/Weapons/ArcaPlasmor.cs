using Terraria;
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
            item.damage = 222;
            item.crit = 18;
            item.magic = true;
            item.mana = 15;
            item.width = 48;
            item.height = 16;
            item.useTime = 54;
            item.useAnimation = 54;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.noMelee = true;
            item.knockBack = 7;
            item.value = 150000;
            item.rare = 4;
            item.autoReuse = true;
            item.shoot = ModContent.ProjectileType<Projectiles.ArcaPlasmorProj>();
            item.shootSpeed = 30f;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.CobaltBar, 22);
            recipe.AddIngredient(ItemID.SoulofLight, 12);

            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.PalladiumBar, 22);
            recipe.AddIngredient(ItemID.SoulofLight, 12);

            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            float pitch = Main.rand.NextFloat(-0.05f, 0.15f);
            PlaySound(pitch);

            var projectile = ShootWith(position, speedX, speedY, type, damage, knockBack, offset: item.width);
            projectile.GetGlobalProjectile<Projectiles.wfGlobalProj>().critMult = 0.8f;
            projectile.GetGlobalProjectile<Projectiles.wfGlobalProj>().AddProcChance(new ProcChance(31, 28));
            float rotation = Convert.ToSingle(-Math.Atan2(speedX, speedY));
            projectile.rotation = rotation;

            return false;
        }
    }
}