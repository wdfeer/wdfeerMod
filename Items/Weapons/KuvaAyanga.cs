using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace wfMod.Items.Weapons
{
    internal class KuvaAyanga : wfWeapon
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Rapidly launches grenades that explode on impact without self-damage");
        }
        public override void SetDefaults()
        {
            item.damage = 47;
            item.crit = 31;
            item.knockBack = 8;
            item.ranged = true;
            item.noMelee = true;
            item.width = 58;
            item.height = 17;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.UseSound = SoundID.Item61.WithVolume(0.8f);
            item.useTime = 14;
            item.useAnimation = 14;
            item.autoReuse = true;
            item.rare = 8;
            item.value = Item.buyPrice(gold: 3);
            item.shoot = mod.ProjectileType("TonkorProj");
            item.shootSpeed = 19f;
            item.useAmmo = ItemID.Grenade;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.GrenadeLauncher);
            recipe.AddIngredient(mod.ItemType("Kuva"), 9);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            var proj = ShootWith(position, speedX, speedY, mod.ProjectileType("TonkorProj"), damage, knockBack, spreadMult: 0.03f, offset: item.width + 3);
            var gProj = proj.GetGlobalProjectile<Projectiles.wfGlobalProj>();
            gProj.ai = () =>
            {
                if (proj.velocity.Y < 10)
                    proj.velocity.Y += 0.2f;
            };
            return false;
        }
    }
}