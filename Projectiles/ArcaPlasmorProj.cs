using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace wfMod.Projectiles
{
    internal class ArcaPlasmorProj : ModProjectile
    {
        bool tenet = false;
        public override void SetDefaults()
        {
            projectile.width = 80;
            projectile.height = 80;
            projectile.scale = 0.6f;
            projectile.alpha = 128;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 32;
            projectile.light = 0.5f;
            projectile.tileCollide = true;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = -1;
        }
        public override void AI()
        {
            if (projectile.timeLeft >= 40) tenet = true;
            for (int i = 0; i < (tenet ? 2 : 1); i++)
            {
                var dust = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, 226)];
                dust.scale = 0.75f;
                projectile.alpha = 256 - projectile.timeLeft * (tenet ? 3 : 4);
                projectile.light = projectile.timeLeft * 0.01f;
            }
        }
        bool hitATile = false;
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (projectile.timeLeft > 16)
            {
                projectile.timeLeft = 16;
                projectile.velocity *= 0.4f;
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
            if (Main.rand.Next(0, 100) <= Main.LocalPlayer.rangedCrit) crit = true; else crit = false;
            if (projectile.timeLeft > 8)
            {
                if (tenet) projectile.timeLeft -= 8;
                else
                {
                    projectile.timeLeft = 8;
                    projectile.velocity *= 0.6f;
                }
            }
            for (int i = 0; i < 24; i++)
            {
                Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 180, 0f, 0f, 75, default(Color), 0.6f);
            }
        }
    }
}