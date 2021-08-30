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
        public Dictionary<int, ProcChance> procChances = new Dictionary<int, ProcChance>();
        public void AddProcChance(ProcChance procChance)
        {
            if (!procChances.ContainsKey(procChance.buffID))
                procChances.Add(procChance.buffID, procChance);
            else procChances[procChance.buffID].chance = ProcChance.AddChance(procChances[procChance.buffID].chance, procChance.chance);
        }
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
        public bool extraPenetrationApplied = false;
        public override void SetDefaults(Projectile projectile)
        {
            proj = projectile;
        }
        public Action onTileCollide = () => { };
        public override bool OnTileCollide(Projectile projectile, Vector2 oldVelocity)
        {
            onTileCollide();
            if (!exploding)
                return base.OnTileCollide(projectile, oldVelocity);
            else return false;
        }
        public override void ModifyHitNPC(Projectile projectile, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            wdfeerPlayer modPl = Main.player[projectile.owner].GetModPlayer<wdfeerPlayer>();
            if (projectile.penetrate >= 0 && !extraPenetrationApplied)
            {
                projectile.penetrate += modPl.penetrate;
                extraPenetrationApplied = true;
                if (!projectile.usesLocalNPCImmunity && !projectile.usesIDStaticNPCImmunity)
                {
                    projectile.usesLocalNPCImmunity = true;
                    projectile.localNPCHitCooldown = projectile.velocity.Length() * (projectile.extraUpdates + 1) > 16f ? 3 : 6;
                }
            }
            if (crit) damage = Convert.ToInt32(critMult * damage);
            if (falloffEnabled)
            {
                distTraveled = (projectile.position - v2).Length();
                if (distTraveled > falloffStartDist)
                {
                    float mult = 1f - falloffMax * (distTraveled < falloffMaxDist ? (distTraveled - falloffStartDist) / (falloffMaxDist - falloffStartDist) : 1.0f);
                    damage = Convert.ToInt32(damage * mult);
                }
            }

            if (modPl.hunterMuni && crit) AddProcChance(new ProcChance(mod.BuffType("SlashProc"), 30, 240));
            if (modPl.internalBleed) AddProcChance(new ProcChance(mod.BuffType("SlashProc"), (int)(30f * (knockback / 20f)), 240));
            foreach (var item in modPl.procChances)
            {
                AddProcChance(item.Value);
            }
            foreach (var procChance in procChances)
            {
                var proc = procChance.Value;
                if (proc.Proc(target))
                {
                    if (proc.buffID == mod.BuffType("SlashProc"))
                    {
                        int slashDamage = (crit ? 2 : 1) * damage / 5;
                        target.GetGlobalNPC<wdfeerGlobalNPC>().AddStackableProc("slash", 300, ref slashDamage);
                    }
                    else if (proc.buffID == BuffID.Electrified)
                    {
                        int electroDamage = (crit ? 2 : 1) * damage / 5 - target.defense * (Main.expertMode ? 3 / 4 : 1 / 2);
                        electroDamage = (int)(electroDamage * modPl.electroMult);
                        target.GetGlobalNPC<wdfeerGlobalNPC>().AddStackableProc("electro", 300, ref electroDamage);
                    }
                }
            }
        }
        public Action<NPC> onHit = (NPC target) => { };
        public override void OnHitNPC(Projectile projectile, NPC target, int damage, float knockback, bool crit)
        {
            onHit(target);
            base.OnHitNPC(projectile, target, damage, knockback, crit);
        }
        public Func<NPC, bool?> canHitNPC = (NPC target) => { return null; };
        public override bool? CanHitNPC(Projectile projectile, NPC target)
        {
            return canHitNPC(target);
        }
        public void Explode(int radius, Action action = null)
        {
            if (exploding) return; else exploding = true;
            proj.width = radius;
            proj.height = radius;
            proj.scale = 1f;
            proj.tileCollide = false;
            proj.velocity *= 0;
            proj.alpha = 255;
            proj.timeLeft = 3;
            proj.penetrate = -1;
            proj.Center = proj.position;

            if (action != null) action();
        }
        public Action<Projectile, int> kill = (Projectile proj, int timeLeft) => { };
        public override void Kill(Projectile projectile, int timeLeft)
        {
            kill(projectile, timeLeft);
        }
        public Action ai = () => { };
        public override void AI(Projectile projectile)
        {
            ai();
            if (impaled) projectile.position = impaledNPC.Center - new Vector2(projectile.width / 2, projectile.height / 2) + impaleOffset;
        }
        Vector2 impaleOffset = Vector2.Zero;
        public void Impale(NPC target, float xOffset = 0, float yOffset = 0)
        {
            impaledNPC = target;
            impaleOffset = new Vector2(xOffset, yOffset);
        }
    }
}