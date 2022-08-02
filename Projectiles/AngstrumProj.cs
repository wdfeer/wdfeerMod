using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace wfMod.Projectiles
{
    internal class AngstrumProj : ModProjectile
    {
        public override string Texture => "wfMod/EmptyTexture";
        wfGlobalProj globalProj;
        public override void SetDefaults()
        {
            globalProj = Projectile.GetGlobalProjectile<wfGlobalProj>();
            Projectile.friendly = true;
            Projectile.height = 20;
            Projectile.width = 20;
            Projectile.timeLeft = 240;
            Projectile.light = 0.4f;
            Projectile.penetrate = -1;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
        }
        public override void AI()
        {
            for (int i = 0; i < 3; i++)
            {
                var dust = Main.dust[Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 92)];
                dust.scale = 0.75f;
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Explode();
            return false;
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            Explode();

            if (target.type == NPCID.EaterofWorldsHead && !Main.hardMode)
                damage /= 2;
            if (Main.rand.Next(100) < Main.player[Projectile.owner].GetCritChance(DamageClass.Ranged))
                crit = true;
        }
        public void Explode()
        {
            globalProj.proj = Projectile;
            if (globalProj.exploding) return;
            globalProj.Explode(100);
        }
        public override void Kill(int timeLeft)
        {
            if (!globalProj.exploding) return;

            SoundEngine.PlaySound(new Terraria.Audio.LegacySoundStyle(2, 14).WithVolume(0.6f), Projectile.position);
            // Smoke Dust spawn
            for (int i = 0; i < 30; i++)
            {
                int dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 31, 0f, 0f, 100, default(Color), 2f);
                Main.dust[dustIndex].velocity *= 1.4f;
            }
            // Fire Dust spawn
            for (int i = 0; i < 50; i++)
            {
                int dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 6, 0f, 0f, 100, default(Color), 3f);
                Main.dust[dustIndex].noGravity = true;
                Main.dust[dustIndex].velocity *= 5f;
                dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 6, 0f, 0f, 100, default(Color), 2f);
                Main.dust[dustIndex].velocity *= 3f;
            }
        }
    }
}