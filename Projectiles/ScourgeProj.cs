using Microsoft.Xna.Framework;
using System;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace wfMod.Projectiles
{
    internal class ScourgeProj : ModProjectile
    {
        wdfeerGlobalProj globalProj;
        public override void SetDefaults()
        {
            globalProj = projectile.GetGlobalProjectile<wdfeerGlobalProj>();
            projectile.width = 16;
            projectile.height = 16;
            projectile.magic = true;
            projectile.friendly = true;
            projectile.extraUpdates = 1;
            projectile.timeLeft = 60;
            projectile.hide = true;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = -1;
        }
        public override void AI()
        {
            int dustIndex = Dust.NewDust(projectile.position, projectile.width, projectile.height, 131, 0f, 0f, 80, default(Color), 0.8f);
            var dust = Main.dust[dustIndex];
            dust.velocity *= 0.2f;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            globalProj.Explode(144);
            for (int i = 0; i < projectile.width / 5; i++)
            {
                int dustIndex = Dust.NewDust(projectile.position, projectile.width, projectile.height, 44, 0f, 0f, 80, default(Color), 1.2f);
                var dust = Main.dust[dustIndex];
                dust.noGravity = true;
                dust.velocity *= 0.75f;
            }
            return false;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            OnTileCollide(Vector2.Zero);
        }
    }
}