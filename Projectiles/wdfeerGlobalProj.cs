using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System.Linq;

namespace wdfeerMod.Projectiles
{
    public class wdfeerGlobalProj : GlobalProjectile
    {
        public override bool InstancePerEntity => true;
        Projectile proj;
        public int glaxionProcs = 0;
        public bool glaxionVandal = false;
        bool exploding = false;
        public override void SetDefaults(Projectile projectile)
        {
            proj = projectile;
        }
        public override bool OnTileCollide(Projectile projectile, Vector2 oldVelocity)
        {
            if (glaxionVandal) 
            {
                Explode(64);
                return false;
            }
            return base.OnTileCollide(projectile,oldVelocity);
        }
        NPC[] hitNPCs = new NPC[99];
        int hitCount = 0;
        public override void OnHitNPC(Projectile projectile, NPC target, int damage, float knockback, bool crit)
        {
            if (glaxionProcs > 0 && Main.rand.Next(0,100) < glaxionProcs)
            {
                if (target.HasBuff(BuffID.Slow)) target.AddBuff(BuffID.Frozen,100);
                else target.AddBuff(BuffID.Slow,100);
            }
            if (glaxionVandal) 
            {
                hitNPCs[hitCount] = target;
                hitCount++;

                Explode(128);
                return;
            }                       
            base.OnHitNPC(projectile, target, damage, knockback, crit);
        }
        public override bool? CanHitNPC (Projectile projectile, NPC target)
        {
            if (!glaxionVandal) return null;
            else if (hitNPCs.Contains<NPC>(target)) return false;
            else return null;
        }
        public void Explode(int radius)
        {
            if (exploding) return; else exploding = true;
            oldPos = proj.Center;
            proj.width = radius;
            proj.height = radius;
            proj.velocity *= 0;
            proj.alpha = 255;
            proj.timeLeft = 3;
            proj.penetrate = -1;
            proj.Center=proj.position;
        }
        Vector2 oldPos;
        public override void Kill(Projectile projectile, int timeLeft)
        {
            if (glaxionVandal && timeLeft <=0)
            {
                for (int i = 0; i < proj.width/16; i++) 
                {
                    int dustIndex = Dust.NewDust(oldPos - new Vector2(proj.width / 2, proj.height / 2), proj.width, proj.height, 226, 0f, 0f, 80, default(Color), 1.2f);
                    var dust = Main.dust[dustIndex];
                    dust.noGravity = true;
                    dust.velocity *= 0.75f;
                }     
            }
        }

    }
}