using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace wfMod.Projectiles
{
    internal class PentaProj : ModProjectile
    {
        wfGlobalProj globalProj;
        bool napalm => Main.player[Projectile.owner].GetModPlayer<wfPlayer>().napalmGrenades;
        Vector2 stickPos = new Vector2(0, 0);
        public override void SetDefaults()
        {
            globalProj = Projectile.GetGlobalProjectile<wfGlobalProj>();
            Projectile.friendly = true;
            Projectile.height = 17;
            Projectile.width = 17;
            Projectile.timeLeft = 1800;
            Projectile.penetrate = -1;
            Projectile.light = 0.5f;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 12;
        }
        float rotationSpeed = Main.rand.NextFloat(-1, 1);
        public override void AI()
        {
            if (globalProj.exploding) return;
            if (stickPos == Vector2.Zero)
            {
                Projectile.rotation += rotationSpeed;
                if (Projectile.velocity.Y < 32)
                    Projectile.velocity.Y += 0.35f;
            }
            else Projectile.position = stickPos;
            if (Projectile.timeLeft == 4 || Main.player[Projectile.owner].dead) globalProj.Explode(150);
            var dust = Main.dust[Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, napalm ? 6 : 206)];
            dust.scale = 0.6f;
            dust.velocity *= 1.3f;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (napalm)
            {
                globalProj.Explode(150);
                return false;
            }

            stickPos = Projectile.position += Vector2.Normalize(oldVelocity) * 2.5f;
            rotationSpeed = 0;
            Projectile.velocity = Vector2.Zero;
            return false;
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (target.type == NPCID.EaterofWorldsHead && !Main.hardMode)
                damage /= 2;
            if (Main.rand.Next(100) < Main.player[Projectile.owner].GetCritChance(DamageClass.Ranged))
                crit = true;

            if (napalm)
            {
                globalProj.Explode(150);
                return;
            }

            if (!globalProj.exploding)
            {
                damage = (int)(damage * 0.4f);
                knockback = Projectile.velocity.Length() * 0.1f;
            }
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (napalm) return null;

            if (globalProj.exploding || Projectile.velocity.Length() > 5f)
                return null;
            else return false;
        }
        public override void Kill(int timeLeft)
        {
            if (!globalProj.exploding) return;

            if (napalm)
            {
                var proj = Main.projectile[Projectile.NewProjectile(Projectile.Center, Vector2.Zero, Mod.Find<ModProjectile>("PentaNapalmProj").Type, Projectile.damage / 6, 0, Projectile.owner)];
                proj.GetGlobalProjectile<wfGlobalProj>().procChances = globalProj.procChances;
            }

            SoundEngine.PlaySound(new Terraria.Audio.LegacySoundStyle(2, 14).WithVolume(0.7f), Projectile.position);
            // Smoke Dust spawn
            for (int i = 0; i < 25; i++)
            {
                int dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 31, 0f, 0f, 100, default(Color), 2f);
                Main.dust[dustIndex].velocity *= 1.3f;
            }
            // Fire Dust spawn
            for (int i = 0; i < 40; i++)
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