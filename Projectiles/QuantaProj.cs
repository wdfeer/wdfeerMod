using Microsoft.Xna.Framework;
using System;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace wfMod.Projectiles
{
    internal class QuantaProj : ModProjectile
    {
        wfGlobalProj globalProj;
        Vector2 lastPos;
        public override void SetDefaults()
        {
            globalProj = projectile.GetGlobalProjectile<wfGlobalProj>();
            projectile.width = 8;
            projectile.height = 8;
            projectile.magic = true;
            projectile.friendly = true;
            projectile.extraUpdates = 40;
            projectile.timeLeft = 64;
            projectile.hide = true;

            projectile.localNPCHitCooldown = -1;
            projectile.usesLocalNPCImmunity = true;
        }
        public override void AI()
        {
            for (int num = 0; num < 3; num++)
            {
                Vector2 position2 = projectile.position;
                position2 -= projectile.velocity * ((float)num * 0.25f);
                int num353 = Dust.NewDust(position2, 1, 1, 206);
                Dust dust = Main.dust[num353];
                dust.position = position2;
                dust.position.X += projectile.width / 2;
                dust.position.Y += projectile.height / 2;
                dust.scale = (float)Main.rand.Next(90, 130) * 0.01f;
                dust.velocity *= 0.2f;
                dust.noGravity = true;
            }

            for (int i1 = 0; i1 < Main.projectile.Length; i1++)
            {
                Projectile p = Main.projectile[i1];
                if (!p.active || p.type != mod.ProjectileType("QuantaAltProj") || p.GetGlobalProjectile<Projectiles.wfGlobalProj>().exploding) continue;

                if (lastPos == null) lastPos = projectile.position;
                else if (Collision.CheckAABBvLineCollision(p.position, new Vector2(p.width, p.height), lastPos, projectile.position))
                {
                    p.GetGlobalProjectile<Projectiles.wfGlobalProj>().Explode(300);
                    p.damage = (int)(p.damage * 1.2f);
                }
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Main.PlaySound(SoundID.DD2_LightningBugZap.WithVolume(0.15f));
        }
    }
}