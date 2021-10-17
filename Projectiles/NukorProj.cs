using Microsoft.Xna.Framework;
using System;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace wfMod.Projectiles
{
    internal class NukorProj : ModProjectile
    {
        public bool chain = false;
        public int confusedChance = 28;
        wfGlobalProj globalProj;
        public override void SetDefaults()
        {
            globalProj = projectile.GetGlobalProjectile<wfGlobalProj>();
            projectile.width = 8;
            projectile.height = 8;
            projectile.magic = true;
            projectile.friendly = true;
            projectile.extraUpdates = 100;
            projectile.penetrate = 1;
            projectile.maxPenetrate = 1;
            projectile.timeLeft = 44;
            projectile.hide = true;

            projectile.localNPCHitCooldown = -1;
            projectile.usesLocalNPCImmunity = true;
        }
        public override void AI()
        {
            for (int num = 0; num < (chain ? 4 : 3); num++)
            {
                Vector2 position2 = projectile.position;
                position2 -= projectile.velocity * ((float)num * 0.25f);
                int num353 = Dust.NewDust(position2, 1, 1, 162);
                Main.dust[num353].position = position2;
                Main.dust[num353].position.X += projectile.width / 2;
                Main.dust[num353].position.Y += projectile.height / 2;
                Main.dust[num353].scale = (float)Main.rand.Next(70, 110) * (chain ? 0.015f : 0.01f);
                Dust dust = Main.dust[num353];
                dust.velocity *= 0.2f;
            }
        }
        int hits = 1;
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            damage /= hits;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (Main.rand.Next(0, 100) <= confusedChance) target.AddBuff(BuffID.Confused, 100);
            if (chain)
            {
                hits++;
                globalProj.Explode(320);
                for (int i = 0; i < target.width / 4; i++)
                {
                    int dustIndex = Dust.NewDust(target.position, Convert.ToInt32(target.width * target.scale), Convert.ToInt32(target.height * target.scale), 162, 0f, 0f, 100, default(Color), 1.6f);
                    var dust = Main.dust[dustIndex];
                    dust.noGravity = true;
                }
            }
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (hits > 3) return false;
            if (globalProj.hitNPCs.Contains<NPC>(target)) return false;
            return null;
        }
    }
}