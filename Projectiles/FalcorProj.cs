using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace wdfeerMod.Projectiles
{
    internal class FalcorProj : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.ThornChakram);
            projectile.width=32;
            projectile.height=32;
        }
    }
}