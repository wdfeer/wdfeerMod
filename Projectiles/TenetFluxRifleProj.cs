using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;

namespace wfMod.Projectiles
{
    internal class TenetFluxRifleProj : ModProjectile
    {
        public override string Texture => "wfMod/EmptyTexture";
        public override void SetDefaults()
        {
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.height = 10;
            Projectile.width = 10;
            Projectile.penetrate = 2;
            Projectile.extraUpdates = 1;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
        }
        public override void AI()
        {
            for (int i = 0; i < 2; i++)
            {
                Vector2 position = Projectile.position;
                position -= Projectile.velocity * Main.rand.NextFloat(-1f, 1f);
                var dust = Main.dust[Dust.NewDust(position, Projectile.width, Projectile.height, 91, Scale: 0.8f)];
                dust.velocity *= 0;
                dust.noGravity = true;
            }
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (Main.rand.Next(0, 100) < Main.LocalPlayer.GetCritChance(DamageClass.Ranged)) crit = true; else crit = false;
            base.ModifyHitNPC(target, ref damage, ref knockback, ref crit, ref hitDirection);
        }
    }
}