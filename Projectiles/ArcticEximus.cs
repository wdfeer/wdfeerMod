using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace wfMod.Projectiles
{
    public class ArcticEximus : ModProjectile
    {
        private float life = 50;
        public float Life
        {
            get => life; set
            {
                life = value;
                if (life <= 0)
                    Projectile.timeLeft = 0;
            }
        }
        public void SetDefaultLife()
        {
            float minLife = Main.hardMode ? 500 : 50;
            if (Life < minLife) Life = minLife;
        }
        public override void SetDefaults()
        {
            

            Projectile.damage = 0;
            Projectile.hostile = true;
            Projectile.penetrate = -1;
            Projectile.width = 240;
            Projectile.height = 240;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 99999;
            Projectile.alpha = 90;
        }
        public NPC parentNPC;
        public override void AI()
        {
            if (parentNPC is null || !parentNPC.active)
            {
                Projectile.timeLeft = 0;
                return;
            }
            Projectile.Center = parentNPC.Center;
            wfMod.NewDustsCircle(2, Projectile.Center, Projectile.width / 2, 51, (d) => { d.velocity *= 0; });

            iFramesTimer++;
        }
        public readonly int immunityFrames = 2;
        public int iFramesTimer = 0;
        public bool CollidingWith(Projectile p)
        {
            if (iFramesTimer < immunityFrames) return false;

            float distance = (p.Center - Projectile.Center).Length();
            var rect = p.getRect();
            if (distance > rect.Width / 2 + Projectile.width / 2) return false;
            if (distance > rect.Height / 2 + Projectile.width / 2) return false;
            if (Projectile.getRect().Intersects(rect))
            {
                return true;
            }
            return false;
        }
        public void HitByProjectile(Projectile thatProj)
        {
            SoundEngine.PlaySound(SoundID.Item50, thatProj.Center);
            if (thatProj.damage < Life)
            {
                thatProj.velocity *= 0.8f;
                if (thatProj.penetrate != -1)
                    thatProj.penetrate = 0;
                else thatProj.timeLeft = 0;
            }
            Life -= thatProj.damage;
            iFramesTimer = 0;
        }
        public override bool CanHitPlayer(Player target)
        {
            return false;
        }
    }
}