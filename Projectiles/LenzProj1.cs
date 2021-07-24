using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace wdfeerMod.Projectiles
{
    internal class LenzProj1 : ModProjectile
    {
        wdfeerGlobalProj globalProj;
        public override void SetDefaults()
        {
            globalProj = projectile.GetGlobalProjectile<wdfeerGlobalProj>();
            projectile.CloneDefaults(ProjectileID.WoodenArrowFriendly);
            projectile.height = 32;
            projectile.width = 6;
            projectile.timeLeft = 120;
            projectile.penetrate = -1;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = -1;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            canImpale = false;
            globalProj.proj = projectile;
            globalProj.Explode(320);
            return false;
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            damage /= 16;
            target.AddBuff(BuffID.Slow, 144);
            if (canImpale)
                hitNpc = target;
            canImpale = false;
            globalProj.proj = projectile;
            globalProj.Explode(320);
        }
        bool canImpale = true;
        NPC hitNpc;
        public override void Kill(int timeLeft)
        {
            if (timeLeft <= 0 && globalProj.exploding)
            {
                int proj = Projectile.NewProjectile(projectile.Center, Vector2.Zero, mod.ProjectileType("LenzProj2"), projectile.damage, projectile.knockBack, projectile.owner);
                if (hitNpc != null && hitNpc.active)
                    Main.projectile[proj].GetGlobalProjectile<wdfeerGlobalProj>().Impale(hitNpc);
                for (int i = 0; i < 45; i++)
                {
                    int dustIndex = Dust.NewDust(projectile.position, projectile.width, projectile.height, 67, 0f, 0f, 100, default(Color), 1.2f);
                    var dust = Main.dust[dustIndex];
                    dust.noGravity = true;
                    dust.velocity *= 0.75f;
                }
            }
        }
    }
}