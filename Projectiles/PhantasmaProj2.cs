using Microsoft.Xna.Framework;
using System;
using System.Linq;
using Terraria;
using Terraria.Audio;
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
            globalProj = Projectile.GetGlobalProjectile<wfGlobalProj>();
            globalProj.exploding = false;
            Projectile.width = 32;
            Projectile.height = 32;
            Projectile.knockBack = 8;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.friendly = true;
            Projectile.extraUpdates = 0;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 400;
            Projectile.hide = true;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
        }
        public override void AI()
        {
            if (globalProj.exploding)
                return;

            Projectile.velocity += new Vector2(0, 0.2f);
            for (int num = 0; num < 5; num++)
            {
                int num353 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 187);
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
            SoundEngine.PlaySound(new Terraria.Audio.LegacySoundStyle(2, 45).WithVolume(0.5f), Projectile.Center);
            SoundEngine.PlaySound(new Terraria.Audio.LegacySoundStyle(2, 45).WithVolume(0.5f), Projectile.Center);

            wfMod.NewDustsCircle(90, Projectile.Center, Projectile.width / 3, 92,
            (d) =>
            {
                d.scale = 1.5f;
                d.noGravity = true;
                d.velocity += Vector2.Normalize(d.position - Projectile.Center) * 4f;
            });
        }
    }
}