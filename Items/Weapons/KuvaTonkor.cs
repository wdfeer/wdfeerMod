using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace wfMod.Items.Weapons
{
    internal class KuvaTonkor : wfWeapon
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Launches grenades that explode on impact without self-damage\n+25% Critical Damage");
        }
        public override void SetDefaults()
        {
            Item.damage = 63;
            Item.crit = 26;
            Item.knockBack = 9;
            Item.DamageType = DamageClass.Ranged;
            Item.noMelee = true;
            Item.width = 37;
            Item.height = 15;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item61;
            Item.useTime = 56;
            Item.useAnimation = 56;
            Item.rare = 5;
            Item.value = Item.buyPrice(gold: 2, silver: 40);
            Item.shoot = Mod.Find<ModProjectile>("TonkorProj").Type;
            Item.shootSpeed = 17f;
            Item.useAmmo = ItemID.Grenade;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(Mod.Find<ModItem>("Tonkor").Type);
            recipe.AddIngredient(Mod.Find<ModItem>("Kuva").Type, 5);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            var proj = ShootWith(position, speedX, speedY, Mod.Find<ModProjectile>("TonkorProj").Type, damage, knockBack, offset: Item.width + 2);
            var gProj = proj.GetGlobalProjectile<Projectiles.wfGlobalProj>();
            return false;
        }
    }
}