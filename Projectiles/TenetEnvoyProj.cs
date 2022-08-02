using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace wfMod.Projectiles
{
    internal class TenetEnvoyProj : ModProjectile
    {
        wfGlobalProj globalProj;
        public override void SetDefaults()
        {
            globalProj = Projectile.GetGlobalProjectile<wfGlobalProj>();
            Projectile.friendly = true;
            Projectile.height = 22;
            Projectile.width = 22;
            Projectile.timeLeft = 600;
            Projectile.penetrate = -1;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
        }
        public override void AI()
        {
            if (Projectile.timeLeft <= 4) Explode();
            Projectile.velocity = Vector2.Normalize(Main.MouseWorld - Projectile.position) * 11;

            float rotation = (float)(Math.PI / 2 + -Math.Atan2(Projectile.velocity.X, Projectile.velocity.Y));
            Projectile.rotation = rotation;

            var dust = Main.dust[Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 206, Scale: 1.2f)];
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Explode();
            return false;
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            Explode();

            if (Main.rand.Next(100) < Main.player[Projectile.owner].GetCritChance(DamageClass.Ranged))
                crit = true;
            else crit = false;
        }
        public void Explode()
        {
            if (globalProj.exploding) return;
            globalProj.Explode(270);

            SoundEngine.PlaySound(new Terraria.Audio.LegacySoundStyle(2, 14).WithVolume(0.5f), Projectile.position);
            for (int i = 0; i < 50; i++)
            {
                var dust = Dust.NewDustPerfect(Projectile.Center + new Vector2(Main.rand.NextFloat(-1, 1), Main.rand.NextFloat(-1, 1)) * Projectile.width / 4, 206, Scale: 1.75f);
                dust.velocity = Vector2.Normalize(dust.position - Projectile.Center) * 8;
            }
        }
    }
}