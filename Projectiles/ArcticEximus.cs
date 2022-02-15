using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace wfMod.Projectiles
{
    public class ArcticEximus : ModProjectile
    {
        private int life = 100;
        public int Life
        {
            get => life; set
            {
                life = value;
                if (life <= 0)
                    projectile.timeLeft = 0;
            }
        }

        public override void SetDefaults()
        {
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
        public override void AI()
        {
            if (parentNPC is null || !parentNPC.active)
            {
                projectile.timeLeft = 0;
                return;
            }
            projectile.Center = parentNPC.Center;

            for (int i = 0; i < Main.projectile.Length; i++)
            {
                var proj = Main.projectile[i];
                if (!proj.active || !proj.friendly || proj.damage <= 0) continue;
                float distance = (proj.Center - projectile.Center).Length();
                var rect = proj.getRect();
                if (distance > rect.Width / 2 + projectile.width / 2) continue;
                if (distance > rect.Height / 2 + projectile.width / 2) continue;
                if (projectile.getRect().Intersects(rect))
                {
                    HitByProjectile(proj);
                    break;
                }
            }

            wfMod.NewDustsCircle(2, projectile.Center, projectile.width / 2, 51, (d) => { d.velocity *= 0; });
        }
        public void HitByProjectile(Projectile p)
        {
            Main.PlaySound(SoundID.Item50, p.Center);
            if (p.damage < Life && p.penetrate != -1)
                p.penetrate = 0;
            p.velocity *= 0.8f;
            Life -= p.damage;
        }
        public override bool CanHitPlayer(Player target)
        {
            return false;
        }
    }
}