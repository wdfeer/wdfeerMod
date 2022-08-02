using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace wfMod.Projectiles
{
    internal class FalcorProj : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.ThornChakram);
            Projectile.width = 32;
            Projectile.height = 32;
            Projectile.usesIDStaticNPCImmunity = true;
            Projectile.idStaticNPCHitCooldown = 12;
        }

        public override void Kill(int timeLeft)
        {
            if (timeLeft <= 0)
            {
                // Play explosion sound
                SoundEngine.PlaySound(new Terraria.Audio.LegacySoundStyle(2, 14), Projectile.position);
                // Electricity Dust spawn
                wfMod.NewDustsCircleFromCenter(Projectile.width / 3, Projectile.Center, Projectile.width / 2, 226, 1f);
            }
        }
    }
}