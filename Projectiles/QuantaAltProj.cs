using System;
using System.Linq;
using Terraria;
using Terraria.Audio;
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
            gProj = Projectile.GetGlobalProjectile<wfGlobalProj>();
            Projectile.friendly = true;
            Projectile.height = 20;
            Projectile.width = 20;
            Projectile.timeLeft = 488;
            Projectile.penetrate = -1;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
            Projectile.hide = true;
            Projectile.light = 0.4f;
        }
        public override void AI()
        {
            if (Projectile.timeLeft <= 8) gProj.Explode(300);
            if (Projectile.velocity.Length() > 0.1f) Projectile.velocity -= Vector2.Normalize(Projectile.velocity) * 0.5f;
            if (!gProj.exploding)
                for (int i = 0; i < Main.projectile.Length; i++)
                {
                    Projectile p = Main.projectile[i];
                    if (p.active && (p.type == Mod.Find<ModProjectile>("QuantaProj").Type || (p.type == Projectile.type && p != Projectile && p.GetGlobalProjectile<wfGlobalProj>().exploding)) && Rectangle.Intersect(Projectile.getRect(), p.getRect()) != Rectangle.Empty)
                    {
                        gProj.Explode(300);
                        Projectile.damage = (int)(Projectile.damage * 1.2f);
                    }
                }
            for (int i = 0; i < 3; i++)
            {
                var dust = Main.dust[Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 226)];
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
            SoundEngine.PlaySound(new Terraria.Audio.LegacySoundStyle(2, 14).WithVolume(0.5f), Projectile.position);
            for (int i = 0; i < 50; i++)
            {
                var dust = Dust.NewDustPerfect(Projectile.Center + new Vector2(Main.rand.NextFloat(-1, 1), Main.rand.NextFloat(-1, 1)) * Projectile.width / 3, 226, Scale: 1.15f);
                dust.velocity = Vector2.Normalize(dust.position - Projectile.Center) * 8;
            }
        }
    }
}