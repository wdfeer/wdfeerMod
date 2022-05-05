using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;

namespace wfMod.Projectiles
{
    internal class FluxRifleProj : ModProjectile
    {
        public override string Texture => "wfMod/EmptyTexture";
        public override void SetDefaults()
        {
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.height = 10;
            projectile.width = 10;
            projectile.penetrate = 2;
            projectile.extraUpdates = 100;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = -1;
        }
        public override void AI()
        {
            for (int i = 0; i < 2; i++)
            {
                Vector2 position = projectile.position;
                position -= projectile.velocity * Main.rand.NextFloat(-0.25f, 0.25f);
                var dust = Main.dust[Dust.NewDust(position, projectile.width, projectile.height, 91, Scale: 0.8f)];
                dust.velocity *= 0;
                dust.noGravity = true;
            }
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (Main.rand.Next(0, 100) < Main.LocalPlayer.rangedCrit) crit = true; else crit = false;
            base.ModifyHitNPC(target, ref damage, ref knockback, ref crit, ref hitDirection);
        }
    }
}