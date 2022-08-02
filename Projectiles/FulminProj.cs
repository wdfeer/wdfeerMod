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
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.alpha = 128;
            Projectile.friendly = true;
            Projectile.penetrate = 6;
            Projectile.scale = 1.6f;
            Projectile.timeLeft = 18;
            Projectile.rotation = 255f;
            Projectile.light = 0.2f;
        }
        public override void AI()
        {
            if (Projectile.timeLeft <= 16)
            {
                Projectile.alpha = 255 - Projectile.timeLeft * 8;
            }
            Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 226, 0f, 0f, 75, default(Color), 0.6f);
        }
        public override void Kill(int timeLeft)
        {
            // Smoke Dust spawn
            for (int i = 0; i < 16; i++)
            {
                Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 226, 0f, 0f, 75, default(Color), 1.2f);
            }
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            Random rand = new Random();

            if (rand.Next(0, 100) <= Main.LocalPlayer.GetCritChance(DamageClass.Ranged)) crit = true; else crit = false;
        }
    }
}