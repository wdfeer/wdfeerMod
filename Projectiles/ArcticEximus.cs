using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System;
using Terraria;
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
                    projectile.timeLeft = 0;
            }
        }
        public void SetDefaultLife()
        {
            float minLife = Main.hardMode ? 500 : 50;
            if (Life < minLife) Life = minLife;
        }
        public override void SetDefaults()
        {
            

            projectile.damage = 0;
            projectile.hostile = true;
            projectile.penetrate = -1;
            projectile.width = 240;
            projectile.height = 240;
            projectile.tileCollide = false;
            projectile.timeLeft = 99999;
            projectile.alpha = 90;
        }
        public NPC parentNPC;
        public override void AI()
        {
            if (parentNPC is null || !parentNPC.active)
            {
                projectile.timeLeft = 0;
                return;
            }
            projectile.Center = parentNPC.Center;
            wfMod.NewDustsCircle(2, projectile.Center, projectile.width / 2, 51, (d) => { d.velocity *= 0; });

            iFramesTimer++;
        }
        public readonly int immunityFrames = 2;
        public int iFramesTimer = 0;
        public bool CollidingWith(Projectile p)
        {
            if (iFramesTimer < immunityFrames) return false;

            float distance = (p.Center - projectile.Center).Length();
            var rect = p.getRect();
            if (distance > rect.Width / 2 + projectile.width / 2) return false;
            if (distance > rect.Height / 2 + projectile.width / 2) return false;
            if (projectile.getRect().Intersects(rect))
            {
                return true;
            }
            return false;
        }
        public void HitByProjectile(Projectile thatProj)
        {
            Main.PlaySound(SoundID.Item50, thatProj.Center);
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