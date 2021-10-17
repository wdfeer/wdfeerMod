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
            projectile.width = 84;
            projectile.height = 84;
            projectile.alpha = 32;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.scale = 0.9f;
            projectile.timeLeft = 45;
            projectile.light = 0.5f;
            projectile.tileCollide = false;
        }
        public override void AI()
        {
            if (projectile.timeLeft == 45)
            {
                if (projectile.velocity.Y > Math.Abs(projectile.velocity.X))
                    drawOriginOffsetY = -12;
                else if (projectile.velocity.Y < -Math.Abs(projectile.velocity.X))
                    drawOriginOffsetY = 12;
            } 

            var dust = Main.dust[Dust.NewDust(projectile.position,projectile.width,projectile.height,91)];
            dust.noGravity = true;
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            // Smoke Dust spawn
            for (int i = 0; i < 12; i++)
            {
                Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 206, 0f, 0f, 75, default(Color), 0.6f);
            }
        }
    }
}