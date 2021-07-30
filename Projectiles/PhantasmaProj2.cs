using Microsoft.Xna.Framework;
using System;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace wdfeerMod.Projectiles
{
    internal class PhantasmaProj2 : ModProjectile
    {
        wdfeerGlobalProj globalProj;
        public override void SetDefaults()
        {
            globalProj = projectile.GetGlobalProjectile<wdfeerGlobalProj>();
            globalProj.exploding = false;
            projectile.width = 32;
            projectile.height = 32;
            projectile.knockBack = 8;
            projectile.magic = true;
            projectile.friendly = true;
            projectile.extraUpdates = 0;
            projectile.penetrate = -1;
            projectile.timeLeft = 400;
            projectile.hide = true;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = -1;
        }
        public override void AI()
        {
            if (!globalProj.exploding)
            {
                projectile.velocity += new Vector2(0, 0.2f);
                for (int num = 0; num < 5; num++)
                {
                    int num353 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 187);
                    Dust dust = Main.dust[num353];
                    dust.scale = (float)Main.rand.Next(80, 130) * 0.01f;
                    dust.velocity *= 0.2f;
                    dust.noGravity = true;
                }
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (!globalProj.exploding)
            {
                globalProj.Explode(280);
                projectile.damage = projectile.damage * 3 / 4;
            }
            return false;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (!globalProj.exploding)
            {
                globalProj.Explode(280);
                projectile.damage = projectile.damage * 3 / 4;
            }
        }
        public override void Kill(int timeLeft)
        {
            if (globalProj.exploding)
            {
                Main.PlaySound(new Terraria.Audio.LegacySoundStyle(2, 45).WithVolume(0.5f), projectile.Center);
                Main.PlaySound(new Terraria.Audio.LegacySoundStyle(2, 45).WithVolume(0.5f), projectile.Center);
                for (int i = 0; i < 200; i++)
                {
                    int dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 92, 0f, 0f, 100, default(Color), 1.7f);
                    Main.dust[dustIndex].velocity *= 1.4f;
                    Main.dust[dustIndex].noGravity = true;
                }
            }
        }
    }
}