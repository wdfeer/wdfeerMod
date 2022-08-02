using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace wfMod.Projectiles
{

    internal class FluctusProj : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 84;
            Projectile.height = 84;
            Projectile.alpha = 32;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.scale = 0.9f;
            Projectile.timeLeft = 45;
            Projectile.light = 0.5f;
            Projectile.tileCollide = false;
        }
        public override void AI()
        {
            if (Projectile.timeLeft == 45)
            {
                if (Projectile.velocity.Y > Math.Abs(Projectile.velocity.X))
                    DrawOriginOffsetY = -12;
                else if (Projectile.velocity.Y < -Math.Abs(Projectile.velocity.X))
                    DrawOriginOffsetY = 12;
            } 

            var dust = Main.dust[Dust.NewDust(Projectile.position,Projectile.width,Projectile.height,91)];
            dust.noGravity = true;
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            // Smoke Dust spawn
            for (int i = 0; i < 12; i++)
            {
                Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 206, 0f, 0f, 75, default(Color), 0.6f);
            }
        }
    }
}