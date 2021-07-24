using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace wdfeerMod.Projectiles
{

    internal class ArcaPlasmorProj : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 32;
            projectile.height = 80;
            projectile.scale = 0.75f;
            projectile.alpha = 128;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.scale = 0.9f;
            projectile.timeLeft = 32;
            projectile.light = 0.5f;
            projectile.tileCollide = false;
            // These 2 help the projectile hitbox be centered on the projectile sprite.
        }
        public override void AI()
        {
            var dust = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, 226)];
            dust.scale = 0.6f;
            projectile.alpha = 256 - projectile.timeLeft * 4;
            projectile.light = projectile.timeLeft / 64;
        }
        bool hitATile = false;
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (projectile.timeLeft > 8)
            {
                projectile.timeLeft -= 4;
            }
            if (!hitATile)
                for (int i = 0; i < 16; i++)
                {
                    Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 180, 0f, 0f, 75, default(Color), 0.6f);
                }
            hitATile = true;
            return false;
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (projectile.timeLeft > 8)
            {
                projectile.timeLeft = 8;
                projectile.velocity *= 0.6f;
            }
            for (int i = 0; i < 24; i++)
            {
                Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 180, 0f, 0f, 75, default(Color), 0.6f);
            }

            if (Main.rand.Next(0,100) <= 28) target.AddBuff(BuffID.Confused,300);
        }
    }
}