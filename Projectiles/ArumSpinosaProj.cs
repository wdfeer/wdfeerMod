using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace wfMod.Projectiles
{
    internal class ArumSpinosaProj : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.friendly = true;
            projectile.melee = true;
            projectile.height = 8;
            projectile.width = 8;
            projectile.penetrate = 2;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = -1;
        }
        public override void AI()
        {
            for (int i = 0; i < 3; i++)
            {
                var dust = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, 178)];
                dust.noGravity = true;
                dust.scale = 0.75f;
            }
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (Main.rand.Next(0,100) < Main.LocalPlayer.meleeCrit) crit = true; else crit = false;
            target.AddBuff(BuffID.Venom, 300);
        }
    }
}