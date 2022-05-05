using System;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace wfMod.Projectiles
{
    internal class QuantaAltProj : ModProjectile
    {
        public override string Texture => "wfMod/EmptyTexture";
        wfGlobalProj gProj;
        public override void SetDefaults()
        {
            gProj = projectile.GetGlobalProjectile<wfGlobalProj>();
            projectile.friendly = true;
            projectile.height = 20;
            projectile.width = 20;
            projectile.timeLeft = 488;
            projectile.penetrate = -1;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = -1;
            projectile.hide = true;
            projectile.light = 0.4f;
        }
        public override void AI()
        {
            if (projectile.timeLeft <= 8) gProj.Explode(300);
            if (projectile.velocity.Length() > 0.1f) projectile.velocity -= Vector2.Normalize(projectile.velocity) * 0.5f;
            if (!gProj.exploding)
                for (int i = 0; i < Main.projectile.Length; i++)
                {
                    Projectile p = Main.projectile[i];
                    if (p.active && (p.type == mod.ProjectileType("QuantaProj") || (p.type == projectile.type && p != projectile && p.GetGlobalProjectile<wfGlobalProj>().exploding)) && Rectangle.Intersect(projectile.getRect(), p.getRect()) != Rectangle.Empty)
                    {
                        gProj.Explode(300);
                        projectile.damage = (int)(projectile.damage * 1.2f);
                    }
                }
            for (int i = 0; i < 3; i++)
            {
                var dust = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, 226)];
                dust.scale = 0.3f;
                dust.noGravity = true;
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            gProj.Explode(300);
            return false;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            gProj.Explode(300);
        }
        public override void Kill(int timeLeft)
        {
            if (!gProj.exploding) return;
            Main.PlaySound(new Terraria.Audio.LegacySoundStyle(2, 14).WithVolume(0.5f), projectile.position);
            for (int i = 0; i < 50; i++)
            {
                var dust = Dust.NewDustPerfect(projectile.Center + new Vector2(Main.rand.NextFloat(-1, 1), Main.rand.NextFloat(-1, 1)) * projectile.width / 3, 226, Scale: 1.15f);
                dust.velocity = Vector2.Normalize(dust.position - projectile.Center) * 8;
            }
        }
    }
}