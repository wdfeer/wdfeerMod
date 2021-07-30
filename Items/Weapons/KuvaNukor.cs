using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;

namespace wdfeerMod.Items.Weapons
{
    public class KuvaNukor : wdfeerWeapon
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Will confuse enemies\nCan hit up to 3 enemies\n+120% Critical Damage\nLimited range");
        }
        public override void SetDefaults()
        {
            item.damage = 17;
            item.crit = 3;
            item.magic = true;
            item.width = 32;
            item.height = 24;
            item.useTime = 6;
            item.useAnimation = 6;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.noMelee = true;
            item.knockBack = 0;
            item.value = 20000;
            item.rare = 4;
            item.UseSound = SoundID.Item91.WithPitchVariance(Main.rand.NextFloat(-0.2f, 0.2f)).WithVolume(0.6f);
            item.autoReuse = true;
            item.shoot = ModContent.ProjectileType<Projectiles.NukorProj>();
            item.shootSpeed = 16f;
            item.mana = 4;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("Nukor"));
            recipe.AddIngredient(mod.ItemType("Kuva"), 7);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            var proj = ShootWith(position, speedX, speedY, type, damage, knockBack, offset: item.width + 1);
            var a = 0;
            var b = false;
            proj.modProjectile.ModifyHitPvp(Main.LocalPlayer, ref a, ref b);
            var globalProj = proj.GetGlobalProjectile<Projectiles.wdfeerGlobalProj>();
            globalProj.critMult = 2.5f;
            globalProj.kuvaNukor = true;

            return false;
        }
    }
}