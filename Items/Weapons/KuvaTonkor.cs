using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace wdfeerMod.Items.Weapons
{
    internal class KuvaTonkor : wdfeerWeapon
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Launches grenades that explode on impact without self-damage\nDeals halved damage to the Eater of Worlds\n+25% Critical Damage");
        }
        public override void SetDefaults()
        {
            item.damage = 63;
            item.crit = 26;
            item.knockBack = 9;
            item.ranged = true;
            item.noMelee = true;
            item.width = 37;
            item.height = 15;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.UseSound = SoundID.Item61;
            item.useTime = 56;
            item.useAnimation = 56;
            item.rare = 5;
            item.value = Item.buyPrice(gold: 2, silver: 40);
            item.shoot = mod.ProjectileType("TonkorProj");
            item.shootSpeed = 17f;
            item.useAmmo = ItemID.Grenade;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("Tonkor"));
            recipe.AddIngredient(mod.ItemType("Kuva"), 5);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            var proj = ShootWith(position, speedX, speedY, mod.ProjectileType("TonkorProj"), damage, knockBack, offset: item.width + 2);
            var gProj = proj.GetGlobalProjectile<Projectiles.wdfeerGlobalProj>();
            gProj.critMult = 1.25f;
            gProj.ai = () =>
            {
                if (proj.velocity.Y < 10)
                    proj.velocity.Y += 0.2f;
            };
            gProj.onTileCollide = () =>
            {
                if (proj.timeLeft > 5)
                    gProj.Explode(80);
            };
            return false;
        }
    }
}