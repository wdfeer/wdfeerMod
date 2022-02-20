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
        public float damageTakenMult = 1;
        public override void SetDefaults()
        {
            Life = Main.hardMode ? 1000 : 100;
            projectile.damage = 0;
            projectile.hostile = true;
            projectile.magic = true;
            projectile.penetrate = -1;
            projectile.width = 240;
            projectile.height = 240;
            projectile.tileCollide = false;
            projectile.timeLeft = 99999;
            projectile.alpha = 90;
        }
        public NPC parentNPC;
        int immunityFrames = 6;
        int iFrameTimer = 0;
        public override void AI()
        {
            if (parentNPC is null || !parentNPC.active)
            {
                projectile.timeLeft = 0;
                return;
            }
            projectile.Center = parentNPC.Center;

            iFrameTimer++;
            if (iFrameTimer > immunityFrames)
            {
                for (int i = 0; i < Main.projectile.Length; i++)
                {
                    var proj = Main.projectile[i];
                    if (!proj.active || !proj.friendly || proj.damage <= 0) continue;
                    if (proj.penetrate == -1) iFrameTimer = 0;
                    if (CollidingWith(proj))
                    {
                        HitByProjectile(proj);
                        break;
                    }
                }
            }

            wfMod.NewDustsCircle(2, projectile.Center, projectile.width / 2, 51, (d) => { d.velocity *= 0; });
        }
        public bool CollidingWith(Projectile p)
        {
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
        public void HitByProjectile(Projectile p)
        {
            Main.PlaySound(SoundID.Item50, p.Center);
            float oldLife = Life;
            Life -= p.damage * damageTakenMult;
            if (p.damage < oldLife)
            {
                p.velocity *= 0.8f;
                if (projectile.penetrate != -1)
                    projectile.penetrate--;
            }
        }
        public override bool CanHitPlayer(Player target)
        {
            return false;
        }
    }
}