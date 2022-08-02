using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;

namespace wfMod.Projectiles
{
    internal class KarystProj : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.DamageType = DamageClass.Melee;
            Projectile.height = 32;
            Projectile.width = 32;
            Projectile.penetrate = 2;
            Projectile.friendly = true;

            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 12;
        }
        public override void AI()
        {
            if (Projectile.velocity.Y < 20)
                Projectile.velocity.Y += 0.4f;
            Projectile.rotation += 0.4f;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            SoundEngine.PlaySound(SoundID.Dig);

            return base.OnTileCollide(oldVelocity);
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Poisoned, 300);
            Projectile.velocity *= 0.8f;
        }
    }
}