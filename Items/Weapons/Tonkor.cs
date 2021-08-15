using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace wdfeerMod.Items.Weapons
{
    internal class Tonkor : wdfeerWeapon
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Launches grenades that explode on impact with self-damage\n+25% Critical Damage");
        }
        public override void SetDefaults()
        {
            item.damage = 9;
            item.crit = 21;
            item.knockBack = 3;
            item.ranged = true;
            item.noMelee = true;
            item.width = 37;
            item.height = 14;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.UseSound = SoundID.Item38;
            item.useTime = 66;
            item.useAnimation = 66;
            item.rare = 2;
            item.value = Item.buyPrice(gold: 2);
            item.shoot = ProjectileID.GrenadeI;
            item.shootSpeed = 20f;
            item.useAmmo = ItemID.Grenade;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            var proj = ShootWith(position, speedX, speedY, ProjectileID.GrenadeI, damage, knockBack, offset: item.width + 2);
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