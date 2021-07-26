using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace wdfeerMod.Projectiles
{
    internal class OrviusProj : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.ThornChakram);
            projectile.width = 32;
            projectile.height = 32;
        }

        public override void AI()
        {
            var dust = Main.dust[Dust.NewDust(projectile.position,projectile.width,projectile.height,226)];
        }

        public override void Kill(int timeLeft)
        {
            if (timeLeft <= 0)
            {
                // Play explosion sound
                Main.PlaySound(new Terraria.Audio.LegacySoundStyle(2, 14).WithVolume(bigBoom ? 1.4f : 1f), projectile.position);
                Color color = new Color(0.75f, 0.8f, 1);
                // Smoke Dust spawn
                for (int i = 0; i < 50 * (bigBoom ? 3 : 1); i++)
                {
                    int dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 31, 0f, 0f, 100, color, 2f);
                    Main.dust[dustIndex].velocity *= 1.4f;
                }
                // Fire Dust spawn
                for (int i = 0; i < 80 * (bigBoom ? 3 : 1); i++)
                {
                    int dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 6, 0f, 0f, 100, color, 3f);
                    Main.dust[dustIndex].noGravity = true;
                    Main.dust[dustIndex].velocity *= 5f;
                    dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 6, 0f, 0f, 100, color, 2f);
                    Main.dust[dustIndex].velocity *= 3f;
                }
                // Large Smoke Gore spawn
                for (int g = 0; g < 2 * (bigBoom ? 2 : 1); g++)
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
                    Main.gore[goreIndex].scale = 1.6f;
                    Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
                    Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
                    goreIndex = Gore.NewGore(new Vector2(projectile.position.X + (float)(projectile.width / 2) - 24f, projectile.position.Y + (float)(projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
                    Main.gore[goreIndex].scale = 1.4f;
                    Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
                    Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
                }
            }
        }
        bool bigBoom = false;
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            if (target == Main.LocalPlayer && damage == 0)
            {
                projectile.GetGlobalProjectile<wdfeerGlobalProj>().slashChance = 0;
                projectile.timeLeft = 3;
                projectile.velocity = new Vector2(0, 0);
                projectile.tileCollide = false;
                // Set to transparent. This projectile technically lives as  transparent for about 3 frames
                projectile.alpha = 255;
                // change the hitbox size, centered about the original projectile center. This makes the projectile damage enemies during the explosion.
                projectile.position = projectile.Center;
                //projectile.position.X = projectile.position.X + (float)(projectile.width / 2);
                //projectile.position.Y = projectile.position.Y + (float)(projectile.height / 2);
                projectile.width = Convert.ToInt32(240);
                projectile.height = Convert.ToInt32(240);
                projectile.scale = 1f;
                projectile.Center = projectile.position;//projectile.position.X = projectile.position.X - (float)(projectile.width / 2);
                //projectile.position.Y = projectile.position.Y - (float)(projectile.height / 2);
                projectile.damage = Convert.ToInt32(projectile.damage * 0.9f);
                projectile.knockBack = 12;
            }
            else base.OnHitPvp(target, damage, crit);
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (projectile.GetGlobalProjectile<wdfeerGlobalProj>().slashChance == 0) target.AddBuff(BuffID.Slow,240);
        }
    }
}