using Microsoft.Xna.Framework;
using System;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace wfMod.Projectiles
{
    internal class OpticorProj : ModProjectile
    {
        public override string Texture => "wfMod/EmptyTexture";
        wfGlobalProj globalProj;
        public Player owner;
        public Func<Vector2> getPositionNearThePlayer;
        public Func<Vector2> getBaseVelocity;
        public override void SetDefaults()
        {
            globalProj = Projectile.GetGlobalProjectile<wfGlobalProj>();
            Projectile.width = 32;
            Projectile.height = 32;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.friendly = true;
            Projectile.extraUpdates = 0;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 200;
            Projectile.hide = true;
            Projectile.tileCollide = false;
            Projectile.usesIDStaticNPCImmunity = true;
            Projectile.idStaticNPCHitCooldown = 6;
        }
        bool playedSound = false;
        public override void AI()
        {
            if (Projectile.timeLeft >= 95)
            {
                if (Projectile.velocity != Vector2.Zero) Projectile.velocity = Vector2.Zero;
                Projectile.position = owner.position + getPositionNearThePlayer();
                var dust = Main.dust[Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 187, getBaseVelocity().X + owner.velocity.X, getBaseVelocity().Y + owner.velocity.Y)];
                dust.noGravity = true;

                if (Projectile.timeLeft == 146 && !playedSound)
                {
                    playedSound = true;
                }
                if (owner.dead) Projectile.Kill();
            }
            else
            {
                if (Projectile.velocity == Vector2.Zero) Projectile.velocity = getBaseVelocity();
                if (!Projectile.tileCollide)
                    Projectile.tileCollide = true;
                Projectile.extraUpdates = 100;
                for (int num = 0; num < 8; num++)
                {
                    Vector2 position2 = Projectile.position;
                    position2 -= Projectile.velocity * (num * 0.25f);
                    int num353 = Dust.NewDust(position2, 1, 1, 180);
                    Dust dust = Main.dust[num353];
                    dust.position = position2;
                    dust.position.X += Projectile.width / 2;
                    dust.position.Y += Projectile.height / 2;
                    dust.scale = Main.rand.Next(70, 110) * 0.013f;
                    dust.velocity *= 2f;
                    dust.noGravity = true;
                }
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            globalProj.Explode(144);
            for (int i = 0; i < Projectile.width / 4; i++)
            {
                int dustIndex = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 226, 0f, 0f, 80, default(Color), 1.2f);
                var dust = Main.dust[dustIndex];
                dust.noGravity = true;
                dust.velocity *= 1.5f;
            }
            return false;
        }
        public override bool? CanDamage()/* tModPorter Suggestion: Return null instead of true */
        {
            if (Projectile.extraUpdates == 0) return false;
            else return true;
        }
    }
}