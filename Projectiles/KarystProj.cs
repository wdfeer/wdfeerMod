using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;

namespace wdfeerMod.Projectiles
{
    internal class KarystProj : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.melee = true;
            projectile.height = 32;
            projectile.width = 32;
            projectile.penetrate = 2;
            projectile.friendly = true;

            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 12;
        }
        public override void AI()
        {
            projectile.velocity.Y += 0.4f;
            projectile.rotation += 0.4f;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Main.PlaySound(0);

            return base.OnTileCollide(oldVelocity);
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Poisoned,300);
            projectile.velocity *= 0.8f;
        }
    }
}