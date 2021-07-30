using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System.Linq;
using System;
using System.Collections.Generic;
namespace wdfeerMod.Projectiles
{
    public class wdfeerGlobalProj : GlobalProjectile
    {
        public override bool InstancePerEntity => true;
        public Projectile proj;
        public float critMult = 1.0f;
        public List<ProcChance> procChances = new List<ProcChance>();
        public int glaxionProcs = 0;
        public bool glaxionVandal = false;
        public bool kuvaNukor = false;
        public Vector2 baseVelocity;
        public Vector2 v2;
        public void SetFalloff(Vector2 startPos, int startDist, int endDist, float maxDmgDecrease)
        {
            v2 = startPos;
            falloffStartDist = startDist;
            falloffMaxDist = endDist;
            falloffMax = maxDmgDecrease;
        }
        public int falloffStartDist = -1;
        public int falloffMaxDist = -1;
        public float falloffMax = 0;
        public float distTraveled = 0;
        public bool falloffEnabled => falloffStartDist != -1;
        public NPC[] hitNPCs = new NPC[64];
        public int hits = 0;
        public bool exploding = false;
        bool impaled => impaledNPC != null && impaledNPC.active;
        NPC impaledNPC;
        public override void SetDefaults(Projectile projectile)
        {
            proj = projectile;
        }
        public override bool OnTileCollide(Projectile projectile, Vector2 oldVelocity)
        {
            if (glaxionVandal)
            {
                Explode(64);
                projectile.damage = projectile.damage * 2 / 3;
                return false;
            }
            return base.OnTileCollide(projectile, oldVelocity);
        }
        public override void ModifyHitNPC(Projectile projectile, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (crit) damage = Convert.ToInt32(critMult * damage);

            foreach (var proc in procChances)
            {
                if (proc.Proc(target))
                {
                    if (proc.buffID == mod.BuffType("SlashProc"))
                    {
                        int slashDamage = damage / 5;
                        target.GetGlobalNPC<wdfeerGlobalNPC>().AddStackableProc("slash", 300, ref slashDamage);
                    }
                    else if (proc.buffID == BuffID.Electrified)
                    {
                        int electroDamage = damage / 5 - target.defense * (Main.expertMode ? 3 / 4 : 1 / 2);
                        target.GetGlobalNPC<wdfeerGlobalNPC>().AddStackableProc("electro", 300, ref electroDamage);
                    }
                }
            }

            if (falloffEnabled)
            {
                distTraveled = (projectile.position - v2).Length();
                if (distTraveled > falloffStartDist)
                {
                    float mult = 1f - falloffMax * (distTraveled < falloffMaxDist ? (distTraveled - falloffStartDist) / (falloffMaxDist - falloffStartDist) : 1.0f);
                    damage = Convert.ToInt32(damage * mult);
                }
            }
        }
        public override void OnHitNPC(Projectile projectile, NPC target, int damage, float knockback, bool crit)
        {
            if (glaxionProcs > 0 && Main.rand.Next(0, 100) < glaxionProcs)
            {
                if (target.HasBuff(BuffID.Slow)) target.AddBuff(BuffID.Frozen, 100);
                else target.AddBuff(BuffID.Slow, 100);
            }
            if (glaxionVandal || (kuvaNukor && projectile.type == ModContent.ProjectileType<Projectiles.NukorProj>()))
            {
                hitNPCs[hits] = target;
                hits++;
                if (glaxionVandal)
                {
                    Explode(128);
                    projectile.damage /= 2;
                }
                return;
            }
            base.OnHitNPC(projectile, target, damage, knockback, crit);
        }
        public override bool? CanHitNPC(Projectile projectile, NPC target)
        {
            if (!glaxionVandal) return null;
            else if (hitNPCs.Contains<NPC>(target)) return false;
            else return null;
        }
        public void Explode(int radius)
        {
            if (exploding) return; else exploding = true;
            proj.width = radius;
            proj.height = radius;
            proj.tileCollide = false;
            proj.velocity *= 0;
            proj.alpha = 255;
            proj.timeLeft = 3;
            proj.penetrate = -1;
            proj.Center = proj.position;
        }
        public override void Kill(Projectile projectile, int timeLeft)
        {
            if (glaxionVandal && timeLeft <= 0)
            {
                for (int i = 0; i < proj.width / 16; i++)
                {
                    int dustIndex = Dust.NewDust(proj.position, proj.width, proj.height, 226, 0f, 0f, 80, default(Color), 1.2f);
                    var dust = Main.dust[dustIndex];
                    dust.noGravity = true;
                    dust.velocity *= 0.75f;
                }
            }
        }
        public override void AI(Projectile projectile)
        {
            if (impaled) projectile.position = impaledNPC.Center - new Vector2(projectile.width / 2, projectile.height / 2);
        }
        public void Impale(NPC target)
        {
            impaledNPC = target;
        }
    }
}