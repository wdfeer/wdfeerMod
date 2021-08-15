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
            Tooltip.SetDefault("Launches grenades that explode on impact without self-damage\nDamage is not affected by the used grenade's damage\nDeals halved damage to the Eater of Worlds\n+25% Critical Damage");
        }
        public override void SetDefaults()
        {
            item.damage = 41;
            item.crit = 21;
            item.knockBack = 6;
            item.ranged = true;
            item.noMelee = true;
            item.width = 37;
            item.height = 14;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.UseSound = SoundID.Item61;
            item.useTime = 66;
            item.useAnimation = 66;
            item.rare = 2;
            item.value = Item.buyPrice(gold: 2);
            item.shoot = mod.ProjectileType("TonkorProj");
            item.shootSpeed = 16f;
            item.useAmmo = ItemID.Grenade;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            var proj = ShootWith(position, speedX, speedY, mod.ProjectileType("TonkorProj"), damage, knockBack, offset: item.width + 2);
            proj.damage = (int)(item.damage * player.rangedDamageMult);
            var gProj = proj.GetGlobalProjectile<Projectiles.wdfeerGlobalProj>();
            gProj.critMult = 1.25f;
            gProj.ai = () =>
            {
                if (proj.velocity.Y < 10) 
                    proj.velocity.Y += 0.1f;
            };
            return false;
        }
    }
}