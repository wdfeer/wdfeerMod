using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace wdfeerMod.Projectiles
{

    internal class FluctusProj : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 32;
            projectile.height = 80;
            projectile.alpha = 32;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.scale = 0.9f;
            projectile.timeLeft = 45;
            projectile.light = 0.5f;
            projectile.tileCollide = false;
            // These 2 help the projectile hitbox be centered on the projectile sprite.
        }
        public override void ModifyHitNPC (NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            // Smoke Dust spawn
            for (int i = 0; i < 8; i++) 
            {
                Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 31, 0f, 0f, 75, default(Color), 0.6f);
            } 
        }  
    }
}