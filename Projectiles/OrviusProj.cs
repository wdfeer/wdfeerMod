using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace wfMod.Projectiles
{
    internal class OrviusProj : ModProjectile
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
            var dust = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, 206)];
        }
        public void Explode()
        {
            var gProj = projectile.GetGlobalProjectile<wdfeerGlobalProj>();
            if (gProj.procChances.ContainsKey(mod.BuffType("SlashProc")))
                gProj.procChances[mod.BuffType("SlashProc")].chance = 0;
            gProj.Explode(240);
            projectile.idStaticNPCHitCooldown = 4;

            // Play explosion sound
            Main.PlaySound(new Terraria.Audio.LegacySoundStyle(2, 14), projectile.position);

            wfMod.NewDustsCircleFromCenter(69, projectile.Center, projectile.width / 2, 206, 2.5f);
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (projectile.GetGlobalProjectile<wdfeerGlobalProj>().exploding) target.AddBuff(BuffID.Slow, 240);
        }
    }
}