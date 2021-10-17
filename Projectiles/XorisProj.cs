using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace wfMod.Projectiles
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
        bool bigBoom = false;
        public void Explode(bool bigKaboom)
        {
            bigBoom = bigKaboom;
            var globalProj = projectile.GetGlobalProjectile<wfGlobalProj>();
            globalProj.AddProcChance(new ProcChance(BuffID.Electrified, 18));
            globalProj.Explode(bigBoom ? 480 : 320);
            projectile.idStaticNPCHitCooldown = 4;
            if (bigBoom) projectile.damage = Convert.ToInt32(projectile.damage * 1.5f);

            #region Effects
            // Play explosion sound
            Main.PlaySound(new Terraria.Audio.LegacySoundStyle(2, 14).WithVolume(bigBoom ? 1.4f : 1f), projectile.position);
            // Smoke Dust spawn
            for (int i = 0; i < 50 * (bigBoom ? 3 : 1); i++)
            {
                int dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 31, 0f, 0f, 100, Scale: 2f);
                Main.dust[dustIndex].velocity *= 1.4f;
            }
            // Electricity Dust spawn
            wfMod.NewDustsCircleFromCenter(projectile.width / 6, projectile.Center, projectile.width / 2, 226, 2.5f);
            #endregion
        }
    }
}