using System;
using Terraria;
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
            globalProj = projectile.GetGlobalProjectile<wfGlobalProj>();
            projectile.friendly = true;
            projectile.height = 22;
            projectile.width = 22;
            projectile.timeLeft = 600;
            projectile.penetrate = -1;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = -1;
        }
        public override void AI()
        {
            if (projectile.timeLeft <= 4) Explode();
            projectile.velocity = Vector2.Normalize(Main.MouseWorld - projectile.position) * 11;

            float rotation = (float)(Math.PI / 2 + -Math.Atan2(projectile.velocity.X, projectile.velocity.Y));
            projectile.rotation = rotation;

            var dust = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, 206, Scale: 1.2f)];
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Explode();
            return false;
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            Explode();

            if (Main.rand.Next(100) < Main.player[projectile.owner].rangedCrit)
                crit = true;
            else crit = false;
        }
        public void Explode()
        {
            if (globalProj.exploding) return;
            globalProj.Explode(270);

            Main.PlaySound(new Terraria.Audio.LegacySoundStyle(2, 14).WithVolume(0.5f), projectile.position);
            for (int i = 0; i < 50; i++)
            {
                var dust = Dust.NewDustPerfect(projectile.Center + new Vector2(Main.rand.NextFloat(-1, 1), Main.rand.NextFloat(-1, 1)) * projectile.width / 4, 206, Scale: 1.75f);
                dust.velocity = Vector2.Normalize(dust.position - projectile.Center) * 8;
            }
        }
    }
}