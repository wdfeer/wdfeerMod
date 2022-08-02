using System;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace wfMod.Projectiles
{
    internal class SimulorProj : ModProjectile
    {
        public override string Texture => "wfMod/EmptyTexture";
        public const int baseTimeLeft = 660;
        wfGlobalProj gProj;
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
            gProj = Projectile.GetGlobalProjectile<wfGlobalProj>();
            Projectile.friendly = true;
            Projectile.height = 20;
            Projectile.width = 20;
            Projectile.timeLeft = baseTimeLeft;
            Projectile.penetrate = -1;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
            Projectile.hide = true;
            Projectile.light = 0.7f;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (Collision.SolidTilesVersatile((int)Projectile.Left.X, (int)Projectile.Left.Y, (int)Projectile.Right.X, (int)Projectile.Right.Y))
                Projectile.velocity.X = -oldVelocity.X * 0.5f;
            if (Collision.SolidTilesVersatile((int)Projectile.Bottom.X, (int)Projectile.Bottom.Y, (int)Projectile.Top.X, (int)Projectile.Top.Y))
                Projectile.velocity.Y = -oldVelocity.Y * 0.5f;
            return false;
        }
        public override void AI()
        {
            if (Projectile.timeLeft <= 8) Explode();
            if (Projectile.velocity.Length() > 0.1f) Projectile.velocity -= Vector2.Normalize(Projectile.velocity) * 0.2f;
            if (gProj.exploding)
                return;
            #region Interaction between projectiles
            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                Projectile proj = Main.projectile[i];
                if (!proj.active || !(proj.ModProjectile is SimulorProj) || proj.GetGlobalProjectile<wfGlobalProj>().exploding) continue;
                SimulorProj simulorProj = proj.ModProjectile as SimulorProj;
                if (simulorProj == this) continue;
                float dist = (proj.position - Projectile.position).Length();
                if (dist > 480f) continue;
                if (dist > Projectile.width / 2)
                {
                    Projectile.velocity -= Vector2.Normalize(Projectile.position - proj.position) * 100 / dist;
                }
                else
                {
                    if (proj.timeLeft > Projectile.timeLeft)
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
                        Projectile.timeLeft = baseTimeLeft;
                        Projectile.velocity *= 0.1f;
                    }
                }
            }
            #endregion
            wfMod.NewDustsCircleEdge(3, Projectile.Center, Projectile.width / 2, 206, (dust) =>
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
            Projectile.GetGlobalProjectile<Projectiles.wfGlobalProj>().AddProcChance(new ProcChance(BuffID.Electrified, electricityChance));
            if (electricityChance != 30)
            {
                Projectile.knockBack = 0f;
                implosion = true;
                for (int i = 0; i < Main.maxNPCs; i++)
                {
                    NPC target = Main.npc[i];
                    if (target.boss || target.type == NPCID.TargetDummy || (target.Center - Projectile.Center).Length() > Projectile.width) continue;
                    target.velocity += Vector2.Normalize(Projectile.Center - target.Center) * 6f;
                }
            }

            SoundEngine.PlaySound(new Terraria.Audio.LegacySoundStyle(2, 14).WithVolume(0.5f), Projectile.position);
            wfMod.NewDustsCustom(radius / 6, () =>
                Dust.NewDustPerfect(Projectile.Center + new Vector2(Main.rand.NextFloat(-1, 1), Main.rand.NextFloat(-1, 1)) * Projectile.width / (implosion ? 2 : 3), 226),
                (dust) =>
                {
                    dust.velocity = Vector2.Normalize(dust.position - Projectile.Center) * (radius / 40f);
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
            if (Main.rand.Next(100) < Main.player[Projectile.owner].GetCritChance(DamageClass.Ranged))
                crit = true;
            else crit = false;
        }
    }
}