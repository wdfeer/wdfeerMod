using Terraria;
using Terraria.DataStructures;
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
            Item.damage = 47;
            Item.crit = 31;
            Item.knockBack = 8;
            Item.DamageType = DamageClass.Ranged;
            Item.noMelee = true;
            Item.width = 58;
            Item.height = 17;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item61.WithVolume(0.8f);
            Item.useTime = 14;
            Item.useAnimation = 14;
            Item.autoReuse = true;
            Item.rare = 8;
            Item.value = Item.buyPrice(gold: 3);
            Item.shoot = Mod.Find<ModProjectile>("TonkorProj").Type;
            Item.shootSpeed = 19f;
            Item.useAmmo = ItemID.Grenade;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.GrenadeLauncher);
            recipe.AddIngredient(Mod.Find<ModItem>("Kuva").Type, 9);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            var proj = ShootWith(position, speedX, speedY, Mod.Find<ModProjectile>("TonkorProj").Type, damage, knockBack, spreadMult: 0.03f, offset: Item.width + 3);
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