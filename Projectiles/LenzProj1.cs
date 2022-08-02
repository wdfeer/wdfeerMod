using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace wfMod.Projectiles
{
    internal class LenzProj1 : ModProjectile
    {
        wfGlobalProj globalProj;
        public override void SetDefaults()
        {
            globalProj = Projectile.GetGlobalProjectile<wfGlobalProj>();
            Projectile.CloneDefaults(ProjectileID.WoodenArrowFriendly);
            Projectile.height = 30;
            Projectile.width = 30;
            Projectile.timeLeft = 120;
            Projectile.penetrate = -1;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            canImpale = false;
            globalProj.proj = Projectile;
            globalProj.Explode(320);
            return false;
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            damage /= 16;
            if (canImpale)
            {
                hitNpc = target;
                damage *= 3;
                canImpale = false;
            }

            globalProj.proj = Projectile;
            globalProj.Explode(320);
        }
        bool canImpale = true;
        NPC hitNpc;
        public override void Kill(int timeLeft)
        {
            if (timeLeft <= 0 && globalProj.exploding)
            {
                var proj = Main.projectile[Projectile.NewProjectile(Projectile.Center, Vector2.Zero, Mod.Find<ModProjectile>("LenzProj2").Type, Projectile.damage, Projectile.knockBack, Projectile.owner)];
                if (hitNpc != null && hitNpc.active)
                {
                    Vector2 offset = Projectile.Center - hitNpc.Center;

                    proj.GetGlobalProjectile<wfGlobalProj>().Impale(hitNpc, offset.X, offset.Y);
                }
                for (int i = 0; i < 45; i++)
                {
                    int dustIndex = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 67, 0f, 0f, 100, default(Color), 1.2f);
                    var dust = Main.dust[dustIndex];
                    dust.noGravity = true;
                    dust.velocity *= 0.75f;
                }
            }
        }
    }
}