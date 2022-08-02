using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace wfMod.Projectiles
{
    internal class OrviusProj : ModProjectile
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
            var dust = Main.dust[Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 206)];
        }
        public void Explode()
        {
            var gProj = Projectile.GetGlobalProjectile<wfGlobalProj>();
            if (gProj.procChances.ContainsKey(Mod.Find<ModBuff>("SlashProc").Type))
                gProj.procChances[Mod.Find<ModBuff>("SlashProc").Type].chance = 0;
            gProj.Explode(240);
            Projectile.idStaticNPCHitCooldown = 4;

            // Play explosion sound
            SoundEngine.PlaySound(new Terraria.Audio.LegacySoundStyle(2, 14), Projectile.position);

            wfMod.NewDustsCircleFromCenter(69, Projectile.Center, Projectile.width / 2, 206, 2.5f);
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (Projectile.GetGlobalProjectile<wfGlobalProj>().exploding) target.AddBuff(BuffID.Slow, 240);
        }
    }
}