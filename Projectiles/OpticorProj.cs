using Microsoft.Xna.Framework;
using System;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace wdfeerMod.Projectiles
{
    internal class OpticorProj : ModProjectile
    {
        wdfeerGlobalProj globalProj;
        public override void SetDefaults()
        {
            globalProj = projectile.GetGlobalProjectile<wdfeerGlobalProj>();
            projectile.width = 32;
            projectile.height = 32;
            projectile.magic = true;
            projectile.friendly = true;
            projectile.extraUpdates = 0;
            projectile.penetrate = -1;
            projectile.timeLeft = 200;
            projectile.hide = true;
            projectile.usesIDStaticNPCImmunity = true;
        }
        bool playedSound = false;
        public override void AI()
        {
            if (projectile.timeLeft >= 95)
            {
                if (projectile.velocity != Vector2.Zero) projectile.velocity = Vector2.Zero;
                projectile.position = Main.LocalPlayer.position + globalProj.v2;
                var dust = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, 187, globalProj.baseVelocity.X + Main.LocalPlayer.velocity.X, globalProj.baseVelocity.Y + Main.LocalPlayer.velocity.Y)];
                dust.noGravity = true;

                if (projectile.timeLeft == 146 && !playedSound)
                {
                    Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/OpticorSound"));
                    playedSound = true;
                }
                if (Main.LocalPlayer.dead) projectile.Kill();
            }
            else
            {
                if (projectile.velocity == Vector2.Zero) projectile.velocity = globalProj.baseVelocity;
                projectile.extraUpdates = 100;
                for (int num = 0; num < 8; num++)
                {
                    Vector2 position2 = projectile.position;
                    position2 -= projectile.velocity * ((float)num * 0.25f);
                    int num353 = Dust.NewDust(position2, 1, 1, 180);
                    Dust dust = Main.dust[num353];
                    dust.position = position2;
                    dust.position.X += projectile.width / 2;
                    dust.position.Y += projectile.height / 2;
                    dust.scale = (float)Main.rand.Next(70, 110) * 0.013f;
                    dust.velocity *= 2f;
                    dust.noGravity = true;
                }
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            globalProj.Explode(144);
            for (int i = 0; i < projectile.width / 4; i++)
            {
                int dustIndex = Dust.NewDust(projectile.position, projectile.width, projectile.height, 226, 0f, 0f, 80, default(Color), 1.2f);
                var dust = Main.dust[dustIndex];
                dust.noGravity = true;
                dust.velocity *= 1.5f;
            }
            return false;
        }
        public override bool CanDamage()
        {
            if (projectile.extraUpdates == 0) return false;
            else return true;
        }
    }
}