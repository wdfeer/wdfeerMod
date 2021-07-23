using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;

namespace wdfeerMod.Projectiles
{
    internal class LenzProj2 : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.friendly = true;
            projectile.width = 320;
            projectile.height = 320;
            projectile.scale = 1f;
            projectile.alpha = 196;
            projectile.tileCollide = false;
            projectile.knockBack = 12;
            projectile.timeLeft = 78;
            projectile.penetrate = -1;
        }
        public override void AI()
        {            
            if (projectile.timeLeft <= 16) projectile.alpha = 256 - projectile.timeLeft * 4;
            else
            {
                int dustIndex = Dust.NewDust(projectile.position, projectile.width, projectile.height, 68, 0f, 0f, 80, default(Color), 1.2f);
                var dust = Main.dust[dustIndex];
                dust.noGravity = true;
                dust.velocity *= 0.75f;
            }
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (projectile.timeLeft > 4) return false; else return null;
        }
        public override void Kill(int timeLeft)
        {
            if (timeLeft <= 0)
            {
                // Play explosion sound
                Main.PlaySound(new Terraria.Audio.LegacySoundStyle(2, 14), projectile.position);
                #region vfx
                // Smoke Dust spawn
                for (int i = 0; i < 50; i++)
                {
                    int dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 31, 0f, 0f, 100, default(Color), 2f);
                    Main.dust[dustIndex].velocity *= 1.4f;
                }
                // Fire Dust spawn
                for (int i = 0; i < 80; i++)
                {
                    int dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 6, 0f, 0f, 100, default(Color), 3f);
                    Main.dust[dustIndex].noGravity = true;
                    Main.dust[dustIndex].velocity *= 5f;
                    dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 6, 0f, 0f, 100, default(Color), 2f);
                    Main.dust[dustIndex].velocity *= 3f;
                }
                // Large Smoke Gore spawn
                for (int g = 0; g < 2; g++)
                {
                    int goreIndex = Gore.NewGore(new Vector2(projectile.position.X + (float)(projectile.width / 2) - 24f, projectile.position.Y + (float)(projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
                    Main.gore[goreIndex].scale = 1.5f;
                    Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
                    Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
                    goreIndex = Gore.NewGore(new Vector2(projectile.position.X + (float)(projectile.width / 2) - 24f, projectile.position.Y + (float)(projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
                    Main.gore[goreIndex].scale = 1.25f;
                    Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
                    Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
                    goreIndex = Gore.NewGore(new Vector2(projectile.position.X + (float)(projectile.width / 2) - 24f, projectile.position.Y + (float)(projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
                    Main.gore[goreIndex].scale = 1.75f;
                    Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
                    Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
                    goreIndex = Gore.NewGore(new Vector2(projectile.position.X + (float)(projectile.width / 2) - 24f, projectile.position.Y + (float)(projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
                    Main.gore[goreIndex].scale = 1.5f;
                    Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
                    Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
                }
                #endregion
            }
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            int critChance = Main.LocalPlayer.magicCrit;
            if (Main.rand.Next(0, 100) <= critChance) crit = true; else crit = false;
        }
    }
}