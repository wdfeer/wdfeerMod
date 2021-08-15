using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace wdfeerMod.Projectiles
{
    internal class TonkorProj : ModProjectile
    {
        wdfeerGlobalProj globalProj;
        public override void SetDefaults()
        {
            globalProj = projectile.GetGlobalProjectile<wdfeerGlobalProj>();
            projectile.friendly = true;
            projectile.height = 20;
            projectile.width = 20;
            projectile.timeLeft = 240;
            projectile.penetrate = -1;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = -1;
        }
        float rotationSpeed = Main.rand.NextFloat(-1, 1);
        public override void AI()
        {
            projectile.rotation += rotationSpeed;
            projectile.velocity.Y += 0.25f;

            var dust = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, 31)];
            dust.scale = 0.6f;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Explode();
            return false;
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            Explode();

            if (target.type == NPCID.EaterofWorldsHead && !Main.hardMode)
                damage /= 2;
            if (Main.rand.Next(100) < Main.player[projectile.owner].rangedCrit)
                crit = true;
        }
        public void Explode()
        {
            globalProj.proj = projectile;
            if (globalProj.exploding) return;
            globalProj.Explode(170);
        }
        public override void Kill(int timeLeft)
        {
            if (!globalProj.exploding) return;

            Main.PlaySound(new Terraria.Audio.LegacySoundStyle(2, 14).WithVolume(0.6f), projectile.position);
            // Smoke Dust spawn
            for (int i = 0; i < 30; i++)
            {
                int dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 31, 0f, 0f, 100, default(Color), 2f);
                Main.dust[dustIndex].velocity *= 1.4f;
            }
            // Fire Dust spawn
            for (int i = 0; i < 50; i++)
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