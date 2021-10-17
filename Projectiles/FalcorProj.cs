using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace wfMod.Projectiles
{
    internal class FalcorProj : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.ThornChakram);
            projectile.width = 32;
            projectile.height = 32;
            projectile.usesIDStaticNPCImmunity = true;
            projectile.idStaticNPCHitCooldown = 12;
        }

        public override void Kill(int timeLeft)
        {
            if (timeLeft <= 0)
            {
                // Play explosion sound
                Main.PlaySound(new Terraria.Audio.LegacySoundStyle(2, 14), projectile.position);
                // Electricity Dust spawn
                wfMod.NewDustsCircleFromCenter(projectile.width / 3, projectile.Center, projectile.width / 2, 226, 1f);
            }
        }
    }
}