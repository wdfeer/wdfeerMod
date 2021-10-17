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
            globalProj = projectile.GetGlobalProjectile<wfGlobalProj>();
            projectile.CloneDefaults(ProjectileID.WoodenArrowFriendly);
            projectile.height = 30;
            projectile.width = 30;
            projectile.timeLeft = 120;
            projectile.penetrate = -1;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = -1;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            canImpale = false;
            globalProj.proj = projectile;
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

            globalProj.proj = projectile;
            globalProj.Explode(320);
        }
        bool canImpale = true;
        NPC hitNpc;
        public override void Kill(int timeLeft)
        {
            if (timeLeft <= 0 && globalProj.exploding)
            {
                var proj = Main.projectile[Projectile.NewProjectile(projectile.Center, Vector2.Zero, mod.ProjectileType("LenzProj2"), projectile.damage, projectile.knockBack, projectile.owner)];
                if (hitNpc != null && hitNpc.active)
                {
                    Vector2 offset = projectile.Center - hitNpc.Center;

                    proj.GetGlobalProjectile<wfGlobalProj>().Impale(hitNpc, offset.X, offset.Y);
                }
                for (int i = 0; i < 45; i++)
                {
                    int dustIndex = Dust.NewDust(projectile.position, projectile.width, projectile.height, 67, 0f, 0f, 100, default(Color), 1.2f);
                    var dust = Main.dust[dustIndex];
                    dust.noGravity = true;
                    dust.velocity *= 0.75f;
                }
            }
        }
    }
}