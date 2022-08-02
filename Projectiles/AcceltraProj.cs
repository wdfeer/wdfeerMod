using Microsoft.Xna.Framework;
using System;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace wfMod.Projectiles
{
    internal class AcceltraProj : ModProjectile
    {
        public override string Texture => "wfMod/EmptyTexture";
        const int explosionRadius = 200;
        wfGlobalProj globalProj;
        public Vector2 initialPos;
        float distanceToInitialPos => (initialPos - Projectile.position).Length();
        bool canExplode => distanceToInitialPos >= explosionRadius;
        public override void SetDefaults()
        {
            globalProj = Projectile.GetGlobalProjectile<wfGlobalProj>();
            globalProj.exploding = false;
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.knockBack = 6;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.friendly = true;
            Projectile.extraUpdates = 0;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 400;
            Projectile.hide = true;
            Projectile.light = 0.2f;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
        }
        public override void AI()
        {
            if (globalProj.exploding)
                return;

            for (int num = 0; num < 2; num++)
            {
                int num353 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.WhiteTorch);
                Dust dust = Main.dust[num353];
                dust.noLight = true;
                dust.scale = Main.rand.Next(80, 130) * 0.01f;
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
                Projectile.penetrate = 0;
            }
        }
        public bool ExplodeIfConditionsAreMet()
        {
            if (globalProj.exploding) return true;
            if (!canExplode) return false;

            globalProj.Explode(explosionRadius);
            SoundEngine.PlaySound(SoundID.Item14.WithVolume(0.5f), Projectile.Center);
            Projectile.light = 0.8f;
            wfMod.NewDustsCircle(30, Projectile.Center, Projectile.width / 3, DustID.AncientLight,
            (d) =>
            {
                d.scale = 1.5f;
                d.noGravity = true;
                d.velocity += Vector2.Normalize(d.position - Projectile.Center) * 4f;
            });
            return true;
        }
    }
}