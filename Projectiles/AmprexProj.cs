using Microsoft.Xna.Framework;
using System;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace wfMod.Projectiles
{
    internal class AmprexProj : ModProjectile
    {
        public override string Texture => "wfMod/EmptyTexture";
        public int chainLeft = 3;
        wfGlobalProj globalProj;
        public override void SetDefaults()
        {
            globalProj = projectile.GetGlobalProjectile<wfGlobalProj>();
            globalProj.exploding = false;
            projectile.width = 8;
            projectile.height = 8;
            projectile.magic = true;
            projectile.friendly = true;
            projectile.extraUpdates = 100;
            projectile.penetrate = -1;
            projectile.timeLeft = 48;
            projectile.hide = true;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = -1;
        }
        public override void AI()
        {
            for (int num = 0; num < 3; num++)
            {
                Vector2 position2 = projectile.position;
                position2 -= projectile.velocity * (num * 0.25f);
                int num353 = Dust.NewDust(position2, 1, 1, 226);
                Main.dust[num353].position = position2;
                Main.dust[num353].position.X += projectile.width / 2;
                Main.dust[num353].position.Y += projectile.height / 2;
                Main.dust[num353].scale = Main.rand.Next(33, 75) * 0.01f;
                Dust dust = Main.dust[num353];
                dust.velocity *= 0.75f;
                dust.noGravity = true;
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (chainLeft > 0)
            {
                chainLeft -= 1;
                if (!globalProj.exploding)
                {
                    globalProj.Explode(160);
                    projectile.damage /= 2;
                }
                else
                {
                    Vector2 offset = target.Center - projectile.Center;
                    for (int i = 0; i < 4; i++)
                    {
                        var dust = Main.dust[Dust.NewDust(projectile.Center + Main.rand.NextFloat(0, 1) * offset, 1, 1, 226)];
                        dust.noGravity = true;
                        dust.scale = 0.8f;
                    }
                }
            }
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (chainLeft == 0) return false;
            return null;
        }
    }
}