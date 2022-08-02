using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace wfMod.Items.Weapons
{
    internal class Lenz : wfWeapon
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Slows all nearby enemies on impact, \nExplodes after a short while");
        }
        public override void SetDefaults()
        {
            Item.damage = 586;
            Item.crit = 46;
            Item.knockBack = 3;
            Item.DamageType = DamageClass.Ranged;
            Item.noMelee = true;
            Item.width = 30;
            Item.height = 48;
            Item.scale = 1.2f;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item5.WithPitchVariance(-0.2f);
            Item.useTime = 66;
            Item.useAnimation = 66;
            Item.rare = 9;
            Item.value = 120000;
            Item.shoot = Mod.Find<ModProjectile>("LenzProj1").Type;
            Item.shootSpeed = 24f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Tsunami, 1);
            recipe.AddIngredient(ItemID.GrenadeLauncher, 1);
            recipe.AddIngredient(ItemID.IceRod, 1);
            recipe.AddIngredient(Mod.Find<ModItem>("Fieldron").Type);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            var proj = ShootWith(position, speedX, speedY, type, damage, knockBack, offset: 23);
            proj.GetGlobalProjectile<Projectiles.wfGlobalProj>().AddProcChance(new ProcChance(BuffID.Slow, 100, 144));
            return false;
        }
    }
}