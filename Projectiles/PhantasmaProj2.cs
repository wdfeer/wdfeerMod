using Microsoft.Xna.Framework;
using System;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace wfMod.Projectiles
{
    internal class PhantasmaProj2 : ModProjectile
    {
        public override string Texture => "wfMod/EmptyTexture";
        wfGlobalProj globalProj;
        public override void SetDefaults()
        {
            globalProj = projectile.GetGlobalProjectile<wfGlobalProj>();
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
            if (globalProj.exploding)
                return;

            projectile.velocity += new Vector2(0, 0.2f);
            for (int num = 0; num < 5; num++)
            {
                int num353 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 187);
                Dust dust = Main.dust[num353];
                dust.scale = Main.rand.Next(80, 130) * 0.01f;
                dust.velocity *= 0.2f;
                dust.noGravity = true;
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Explode();
            return false;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Explode();
        }
        public void Explode()
        {
            if (globalProj.exploding) return;

            globalProj.Explode(280);
            Main.PlaySound(new Terraria.Audio.LegacySoundStyle(2, 45).WithVolume(0.5f), projectile.Center);
            Main.PlaySound(new Terraria.Audio.LegacySoundStyle(2, 45).WithVolume(0.5f), projectile.Center);

            wfMod.NewDustsCircle(90, projectile.Center, projectile.width / 3, 92,
            (d) =>
            {
                d.scale = 1.5f;
                d.noGravity = true;
                d.velocity += Vector2.Normalize(d.position - projectile.Center) * 4f;
            });
        }
    }
}