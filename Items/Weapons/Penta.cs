using System;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace wdfeerMod.Items.Weapons
{
    internal class Penta : wdfeerWeapon
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Launches grenades that have to be detonated manually\nRight Click to trigger the explosions\nDirect hits deal 40% of the damage\nOnly five grenades can be active at a time\nDamage is not affected by the used grenade's damage");
        }
        public override void SetDefaults()
        {
            item.damage = 57;
            item.crit = 6;
            item.knockBack = 7.5f;
            item.ranged = true;
            item.noMelee = true;
            item.width = 46;
            item.height = 17;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.useTime = 57;
            item.useAnimation = 57;
            item.rare = 2;
            item.value = Item.buyPrice(gold: 2);
            item.shoot = mod.ProjectileType("PentaProj");
            item.shootSpeed = 16f;
            item.useAmmo = ItemID.Grenade;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse != 2)
                return base.CanUseItem(player);
            else if (player.altFunctionUse == 2 && projs.Any<Projectile>(X => X.active && X.type == item.shoot))
            {
                for (int i = 0; i < projs.Length; i++)
                {
                    if (!projs[i].active || projs[i].type != item.shoot) continue;
                    projs[i].GetGlobalProjectile<Projectiles.wdfeerGlobalProj>().Explode(150);
                    projs[i].localNPCHitCooldown = -1;
                }

                return false;
            }
            else return false;
        }
        int nextProjIndex
        {
            get
            {
                if (Array.FindIndex<Projectile>(projs, x => !x.active || x.type != item.shoot) >= 0 && Array.FindIndex<Projectile>(projs, x => !x.active || x.type != item.shoot) < 5) return Array.FindIndex<Projectile>(projs, x => !x.active || x.type != item.shoot);
                else
                {
                    int firstShotIndex = Array.FindIndex<Projectile>(projs, x => x.timeLeft == projs.Min<Projectile>(X => X.timeLeft));
                    projs[firstShotIndex].GetGlobalProjectile<Projectiles.wdfeerGlobalProj>().Explode(150);
                    return firstShotIndex;
                }
            }
        }
        Projectile[] projs = new Projectile[5] { new Projectile(), new Projectile(), new Projectile(), new Projectile(), new Projectile() };
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            var proj = ShootWith(position, speedX, speedY, mod.ProjectileType("PentaProj"), (int)(item.damage * player.rangedDamageMult), knockBack, offset: 16, sound: SoundID.Item61.WithPitchVariance(-0.2f));
            projs[nextProjIndex] = proj;
            var gProj = proj.GetGlobalProjectile<Projectiles.wdfeerGlobalProj>();
            gProj.ai = () =>
            {
                if (proj.velocity.Y < 10)
                    proj.velocity.Y += 0.12f;
            };
            return false;
        }
    }
}