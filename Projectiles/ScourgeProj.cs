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
        public override string Texture => "wfMod/EmptyTexture";
        wfGlobalProj globalProj;
        public override void SetDefaults()
        {
            globalProj = Projectile.GetGlobalProjectile<wfGlobalProj>();
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.friendly = true;
            Projectile.extraUpdates = 1;
            Projectile.timeLeft = 60;
            Projectile.hide = true;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
        }
        public override void AI()
        {
            int dustIndex = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 131, 0f, 0f, 80, default(Color), 0.8f);
            var dust = Main.dust[dustIndex];
            dust.velocity *= 0.2f;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            globalProj.Explode(144);
            for (int i = 0; i < Projectile.width / 5; i++)
            {
                int dustIndex = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 44, 0f, 0f, 80, default(Color), 1.2f);
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