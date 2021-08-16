using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;

namespace wdfeerMod.Projectiles
{
    internal class KarystPrimeProj : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.melee = true;
            projectile.height = 32;
            projectile.width = 32;
            projectile.penetrate = 3;
            projectile.friendly = true;

            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 12;
        }
        public override void AI()
        {
            if (projectile.velocity.Y < 20)
                projectile.velocity.Y += 0.3f;
            projectile.rotation += 0.36f;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Main.PlaySound(0);

            return base.OnTileCollide(oldVelocity);
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (Main.rand.NextBool())
                target.AddBuff(BuffID.Venom, 200);
            else target.AddBuff(BuffID.Poisoned, 200);
            projectile.velocity *= 0.8f;
        }
    }
}