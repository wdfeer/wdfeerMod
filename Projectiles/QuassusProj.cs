using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace wdfeerMod.Projectiles
{
    internal class QuassusProj : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.friendly = true;
            projectile.height = 8;
            projectile.width = 8;
            projectile.penetrate = 2;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = -1;
        }
        public override void AI()
        {
            for (int i = 0; i < 3; i++)
            {
                var dust = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, 53)];
                dust.noGravity = true;
                dust.scale = 0.75f;
            }
        }
    }
}