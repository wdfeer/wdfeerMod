using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace wdfeerMod.Items.Weapons
{
    internal class Lenz : wdfeerWeapon
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Slows all nearby enemies on impact, \nExplodes after a short while");
        }
        public override void SetDefaults()
        {
            item.damage = 586;
            item.crit = 46;
            item.knockBack = 3;
            item.ranged = true;
            item.noMelee = true;
            item.width = 30;
            item.height = 48;
            item.scale = 1.2f;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.UseSound = SoundID.Item5.WithPitchVariance(-0.2f);
            item.useTime = 66;
            item.useAnimation = 66;
            item.rare = 9;
            item.value = 120000;
            item.shoot = mod.ProjectileType("LenzProj1");
            item.shootSpeed = 24f;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Tsunami, 1);
            recipe.AddIngredient(ItemID.GrenadeLauncher, 1);
            recipe.AddIngredient(ItemID.IceRod, 1);
            recipe.AddIngredient(mod.ItemType("Fieldron"));
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            var proj = ShootWith(position, speedX, speedY, type, damage, knockBack);
            proj.GetGlobalProjectile<Projectiles.wdfeerGlobalProj>().procChances.Add(new ProcChance(BuffID.Slow, 100, 144));
            return false;
        }
    }
}