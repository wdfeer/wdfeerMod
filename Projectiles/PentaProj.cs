using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace wdfeerMod.Projectiles
{
    internal class PentaProj : ModProjectile
    {
        wdfeerGlobalProj globalProj;
        public override void SetDefaults()
        {
            globalProj = projectile.GetGlobalProjectile<wdfeerGlobalProj>();
            projectile.friendly = true;
            projectile.height = 17;
            projectile.width = 17;
            projectile.timeLeft = 1800;
            projectile.penetrate = -1;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 12;
        }
        float rotationSpeed = Main.rand.NextFloat(-1, 1);
        public override void AI()
        {
            projectile.rotation += rotationSpeed;
            if (projectile.velocity.Y < 32)
                projectile.velocity.Y += 0.25f;
            if (projectile.timeLeft == 4 || Main.player[projectile.owner].dead) globalProj.Explode(150);
            var dust = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, 31)];
            dust.scale = 0.6f;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (projectile.velocity.Length() > 0.4f)
                projectile.velocity.X -= Vector2.Normalize(projectile.velocity).X * 0.4f;
            if (Math.Abs(rotationSpeed) > 0.02f)
                rotationSpeed -= Math.Sign(rotationSpeed) * 0.01f;
            else rotationSpeed *= 0.5f;
            return false;
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (!globalProj.exploding)
            {
                damage = (int)(damage * 0.4f);
                knockback = projectile.velocity.Length() * 0.1f;
            }

            if (target.type == NPCID.EaterofWorldsHead && !Main.hardMode)
                damage /= 2;
            if (Main.rand.Next(100) < Main.player[projectile.owner].rangedCrit)
                crit = true;
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (globalProj.exploding || projectile.velocity.Length() > 5f)
                return true;
            else return false;
        }
        public override void Kill(int timeLeft)
        {
            if (!globalProj.exploding) return;

            Main.PlaySound(new Terraria.Audio.LegacySoundStyle(2, 14).WithVolume(0.7f), projectile.position);
            // Smoke Dust spawn
            for (int i = 0; i < 25; i++)
            {
                int dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 31, 0f, 0f, 100, default(Color), 2f);
                Main.dust[dustIndex].velocity *= 1.3f;
            }
            // Fire Dust spawn
            for (int i = 0; i < 40; i++)
            {
                int dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 6, 0f, 0f, 100, default(Color), 3f);
                Main.dust[dustIndex].noGravity = true;
                Main.dust[dustIndex].velocity *= 5f;
                dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 6, 0f, 0f, 100, default(Color), 2f);
                Main.dust[dustIndex].velocity *= 3f;
            }
        }
    }
}