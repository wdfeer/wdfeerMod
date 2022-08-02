using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace wfMod.Items.Weapons
{
    public class Fulmin : wfWeapon
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("+20% Critical Damage\n16% Electricity proc chance");
        }
        public override void SetDefaults()
        {
            pathToSound = "Sounds/FulminSound";
            Item.damage = 27;
            Item.crit = 26;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 48;
            Item.height = 15;
            Item.useTime = 28;
            Item.useAnimation = 28;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 8;
            Item.value = 15000;
            Item.rare = 3;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.FulminProj>();
            Item.shootSpeed = 36f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.IllegalGunParts);
            recipe.AddIngredient(ItemID.Feather, 8);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            PlaySound(Main.rand.NextFloat(0, 0.15f), 0.6f);

            var projectile = ShootWith(position, speedX, speedY, type, damage, knockBack, offset: Item.width - 2);
            var globalProj = projectile.GetGlobalProjectile<Projectiles.wfGlobalProj>();
            globalProj.critMult = 1.2f;
            globalProj.AddProcChance(new ProcChance(BuffID.Electrified, 16));

            return false;
        }
    }
}