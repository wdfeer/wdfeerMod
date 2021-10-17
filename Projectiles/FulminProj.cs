using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace wfMod.Projectiles
{
    internal class FulminProj : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 16;
            projectile.alpha = 128;
            projectile.friendly = true;
            projectile.penetrate = 6;
            projectile.scale = 1.6f;
            projectile.timeLeft = 18;
            projectile.rotation = 255f;
            projectile.light = 0.2f;
        }
        public override void AI()
        {
            if (projectile.timeLeft <= 16)
            {
                projectile.alpha = 255 - projectile.timeLeft * 8;
            }
            Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 226, 0f, 0f, 75, default(Color), 0.6f);
        }
        public override void Kill(int timeLeft)
        {
            // Smoke Dust spawn
            for (int i = 0; i < 16; i++)
            {
                Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 226, 0f, 0f, 75, default(Color), 1.2f);
            }
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            Random rand = new Random();

            if (rand.Next(0, 100) <= Main.LocalPlayer.rangedCrit) crit = true; else crit = false;
        }
    }
}