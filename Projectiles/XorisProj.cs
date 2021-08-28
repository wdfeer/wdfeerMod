using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace wdfeerMod.Projectiles
{
    internal class XorisProj : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.ThornChakram);
            projectile.width = 32;
            projectile.height = 32;
            projectile.usesIDStaticNPCImmunity = true;
            projectile.idStaticNPCHitCooldown = 12;
        }

        public override void AI()
        {
            Vector2 extraVelocity = (Main.LocalPlayer.position - projectile.position) / 60;
            if (projectile.Distance(Main.LocalPlayer.position) > 800) projectile.velocity += extraVelocity;
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
        public void Explode(bool bigKaboom)
        {
            bigBoom = bigKaboom;
            var globalProj = projectile.GetGlobalProjectile<wdfeerGlobalProj>();
            globalProj.procChances.Add(new ProcChance(BuffID.Electrified, 18));
            globalProj.Explode(bigBoom ? 480 : 320);
            projectile.idStaticNPCHitCooldown = 4;
            if (bigBoom) projectile.damage = Convert.ToInt32(projectile.damage * 1.5f);
        }
    }
}