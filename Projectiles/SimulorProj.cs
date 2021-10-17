using System;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace wfMod.Projectiles
{
    internal class SimulorProj : ModProjectile
    {
        public const int baseTimeLeft = 660;
        wdfeerGlobalProj gProj;
        public bool implosion = false;
        private float damageMult = 1f;
        public float DamageMult
        {
            get => damageMult;
            set
            {
                if (value > 3) value = 3;
                damageMult = value;
            }
        }
        public override void SetDefaults()
        {
            gProj = projectile.GetGlobalProjectile<wdfeerGlobalProj>();
            projectile.friendly = true;
            projectile.height = 20;
            projectile.width = 20;
            projectile.timeLeft = baseTimeLeft;
            projectile.penetrate = -1;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = -1;
            projectile.hide = true;
            projectile.light = 0.7f;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (Collision.SolidTilesVersatile((int)projectile.Left.X, (int)projectile.Left.Y, (int)projectile.Right.X, (int)projectile.Right.Y))
                projectile.velocity.X = -oldVelocity.X * 0.5f;
            if (Collision.SolidTilesVersatile((int)projectile.Bottom.X, (int)projectile.Bottom.Y, (int)projectile.Top.X, (int)projectile.Top.Y))
                projectile.velocity.Y = -oldVelocity.Y * 0.5f;
            return false;
        }
        public override void AI()
        {
            if (projectile.timeLeft <= 8) Explode();
            if (projectile.velocity.Length() > 0.1f) projectile.velocity -= Vector2.Normalize(projectile.velocity) * 0.2f;
            if (gProj.exploding)
                return;
            #region Interaction between projectiles
            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                Projectile proj = Main.projectile[i];
                if (!proj.active || !(proj.modProjectile is SimulorProj) || proj.GetGlobalProjectile<wdfeerGlobalProj>().exploding) continue;
                SimulorProj simulorProj = proj.modProjectile as SimulorProj;
                if (simulorProj == this) continue;
                float dist = (proj.position - projectile.position).Length();
                if (dist > 480f) continue;
                if (dist > projectile.width / 2)
                {
                    projectile.velocity -= Vector2.Normalize(projectile.position - proj.position) * 100 / dist;
                }
                else
                {
                    if (proj.timeLeft > projectile.timeLeft)
                    {
                        // Increases damage of the surviving projectile by 20% of the projectile with the highest damage
                        if (!Explode(480, 100))
                            continue;
                        DamageMult *= 1.2f;
                        simulorProj.DamageMult *= 1.2f;
                        proj.timeLeft = baseTimeLeft;
                        proj.velocity *= 0.1f;
                    }
                    else
                    {
                        // Increases damage of the surviving projectile by 20% of the projectile with the highest damage
                        if (!simulorProj.Explode(480, 100))
                            continue;
                        DamageMult *= 1.2f;
                        simulorProj.DamageMult *= 1.2f;
                        projectile.timeLeft = baseTimeLeft;
                        projectile.velocity *= 0.1f;
                    }
                }
            }
            #endregion
            wfMod.NewDustsCircleEdge(3, projectile.Center, projectile.width / 2, 206, (dust) =>
            {
                dust.velocity *= 0.5f;
                dust.scale = 1.2f;
                dust.noGravity = true;
            });
        }
        /// <summary>
        /// Makes this projectile explode with the chosen radius (width and height) and chance to proc Electrified
        /// </summary>
        /// <returns>False if the projectile is already exploding, otherwise true</returns>
        public bool Explode(int radius = 200, int electricityChance = 30)
        {
            if (gProj.exploding) return false;
            gProj.Explode(radius);
            projectile.GetGlobalProjectile<Projectiles.wdfeerGlobalProj>().AddProcChance(new ProcChance(BuffID.Electrified, electricityChance));
            if (electricityChance != 30)
            {
                projectile.knockBack = 0f;
                implosion = true;
                for (int i = 0; i < Main.maxNPCs; i++)
                {
                    NPC target = Main.npc[i];
                    if (target.boss || target.type == NPCID.TargetDummy || (target.Center - projectile.Center).Length() > projectile.width) continue;
                    target.velocity += Vector2.Normalize(projectile.Center - target.Center) * 6f;
                }
            }

            Main.PlaySound(new Terraria.Audio.LegacySoundStyle(2, 14).WithVolume(0.5f), projectile.position);
            wfMod.NewDustsCustom(radius / 6, () =>
                Dust.NewDustPerfect(projectile.Center + new Vector2(Main.rand.NextFloat(-1, 1), Main.rand.NextFloat(-1, 1)) * projectile.width / (implosion ? 2 : 3), 226),
                (dust) =>
                {
                    dust.velocity = Vector2.Normalize(dust.position - projectile.Center) * (radius / 40f);
                    if (implosion) dust.velocity *= -1.2f;
                });
            return true;
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (!gProj.exploding) return false;
            return base.CanHitNPC(target);
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            damage = (int)(damage * DamageMult);
            if (Main.rand.Next(100) < Main.player[projectile.owner].rangedCrit)
                crit = true;
            else crit = false;
        }
    }
}