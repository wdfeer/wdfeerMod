using Microsoft.Xna.Framework;
using System;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace wfMod.Projectiles
{
    internal class PhantasmaProj : ModProjectile
    {
        public override string Texture => "wfMod/EmptyTexture";
        public int chainLeft = 6;
        wfGlobalProj globalProj;
        public override void SetDefaults()
        {
            globalProj = Projectile.GetGlobalProjectile<wfGlobalProj>();
            globalProj.exploding = false;
            Projectile.width = 24;
            Projectile.height = 24;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.friendly = true;
            Projectile.extraUpdates = 100;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 40;
            Projectile.hide = true;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
        }
        public override void AI()
        {
            if (!globalProj.exploding)
                for (int num = 0; num < 3; num++)
                {
                    int num353 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 187);
                    Main.dust[num353].scale = Main.rand.Next(80, 120) * 0.01f;
                    Dust dust = Main.dust[num353];
                    dust.velocity *= 0.2f;
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
                    globalProj.Explode(150);
                    Projectile.damage = Projectile.damage * 3 / 4;
                }
                else
                {
                    Vector2 offset = target.Center - Projectile.Center;
                    for (int i = 0; i < 10; i++)
                    {
                        var dust = Main.dust[Dust.NewDust(Projectile.Center - new Vector2(12, 12) + Main.rand.NextFloat(0, 1) * offset, 24, 24, 187)];
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