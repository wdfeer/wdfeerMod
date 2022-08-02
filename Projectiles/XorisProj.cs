using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace wfMod.Projectiles
{
    internal class XorisProj : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.ThornChakram);
            Projectile.width = 32;
            Projectile.height = 32;
            Projectile.usesIDStaticNPCImmunity = true;
            Projectile.idStaticNPCHitCooldown = 12;
        }

        public override void AI()
        {
            Vector2 extraVelocity = (Main.LocalPlayer.position - Projectile.position) / 60;
            if (Projectile.Distance(Main.LocalPlayer.position) > 800) Projectile.velocity += extraVelocity;
        }
        bool bigBoom = false;
        public void Explode(bool bigKaboom)
        {
            bigBoom = bigKaboom;
            var globalProj = Projectile.GetGlobalProjectile<wfGlobalProj>();
            globalProj.AddProcChance(new ProcChance(BuffID.Electrified, 18));
            globalProj.Explode(bigBoom ? 480 : 320);
            Projectile.idStaticNPCHitCooldown = 4;
            if (bigBoom) Projectile.damage = Convert.ToInt32(Projectile.damage * 1.5f);

            #region Effects
            // Play explosion sound
            SoundEngine.PlaySound(new Terraria.Audio.LegacySoundStyle(2, 14).WithVolume(bigBoom ? 1.4f : 1f), Projectile.position);
            // Smoke Dust spawn
            for (int i = 0; i < 50 * (bigBoom ? 3 : 1); i++)
            {
                int dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 31, 0f, 0f, 100, Scale: 2f);
                Main.dust[dustIndex].velocity *= 1.4f;
            }
            // Electricity Dust spawn
            wfMod.NewDustsCircleFromCenter(Projectile.width / 6, Projectile.Center, Projectile.width / 2, 226, 2.5f);
            #endregion
        }
    }
}