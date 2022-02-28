using Microsoft.Xna.Framework;
using System;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace wfMod.Projectiles
{
    internal class AcceltraProj : ModProjectile
    {
        const int explosionRadius = 200;
        wfGlobalProj globalProj;
        public Vector2 initialPos;
        float distanceToInitialPos => (initialPos - projectile.position).Length();
        bool canExplode => distanceToInitialPos >= explosionRadius;
        public override void SetDefaults()
        {
            globalProj = projectile.GetGlobalProjectile<wfGlobalProj>();
            globalProj.exploding = false;
            projectile.width = 16;
            projectile.height = 16;
            projectile.knockBack = 6;
            projectile.ranged = true;
            projectile.friendly = true;
            projectile.extraUpdates = 0;
            projectile.penetrate = -1;
            projectile.timeLeft = 400;
            projectile.hide = true;
            projectile.light = 0.2f;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = -1;
        }
        public override void AI()
        {
            if (globalProj.exploding)
                return;

            for (int num = 0; num < 2; num++)
            {
                int num353 = Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.WhiteTorch);
                Dust dust = Main.dust[num353];
                dust.noLight = true;
                dust.scale = (float)Main.rand.Next(80, 130) * 0.01f;
                dust.velocity *= 0.2f;
                dust.noGravity = true;
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (ExplodeIfConditionsAreMet())
                return false;
            return true;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            ExplodeIfConditionsAreMet();
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (!canExplode)
            {
                knockback *= 2;
                damage /= 2;
                projectile.penetrate = 0;
            }
        }
        public bool ExplodeIfConditionsAreMet()
        {
            if (globalProj.exploding) return true;
            if (!canExplode) return false;

            globalProj.Explode(explosionRadius);
            Main.PlaySound(SoundID.Item14.WithVolume(0.5f), projectile.Center);
            projectile.light = 0.8f;
            wfMod.NewDustsCircle(30, projectile.Center, projectile.width / 3, DustID.AncientLight,
            (d) =>
            {
                d.scale = 1.5f;
                d.noGravity = true;
                d.velocity += Vector2.Normalize(d.position - projectile.Center) * 4f;
            });
            return true;
        }
    }
}