using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace wdfeerMod.Projectiles
{

    internal class FulminProj : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 16;
            projectile.friendly = true;
            projectile.penetrate = 6;
            projectile.scale = 1.6f;
            projectile.timeLeft = 18;
            // These 2 help the projectile hitbox be centered on the projectile sprite.
            drawOffsetX = 0;
            drawOriginOffsetY = 0;
        }
        public override void ModifyHitNPC (NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            Random rand = new Random();

            if (rand.Next(0,100) <= Main.LocalPlayer.rangedCrit) crit = true; else crit = false;

            damage = crit ? Convert.ToInt32(damage * 1.2f) : damage;
            if (rand.Next(1, 101) <= 16) target.AddBuff(BuffID.Electrified,200);           
        }  
    }
}