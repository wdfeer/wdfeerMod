using Microsoft.Xna.Framework;
using System;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using wfMod.Projectiles;

namespace wfMod.Items.Accessories
{
    
    public class SynergizedProspectus : ExclusiveAccessory
    {
        const int baseDamage = 60;
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault($"{baseDamage} base summon damage\nEvery 5 seconds, all your active minions shoot a homing electrical spark at random foes, prioritizing bosses");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = 5;
            Item.value = Item.buyPrice(gold: 4, silver: 20);
        }
        double timeWhenLastUpdated = -1;
        const int attackCooldown = 300;
        int attackTimer = 0;
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (Main.time - timeWhenLastUpdated > 5)
                attackTimer = 0;
            attackTimer++;
            if (attackTimer >= attackCooldown)
            {
                NPC[] nearbyNPCs = GetNearbyNPCs(player.Center);
                if (nearbyNPCs.Length > 0)
                {
                    Attack(player, nearbyNPCs);
                    attackTimer = 0;
                }
            }

            timeWhenLastUpdated = Main.time;
        }
        const float attackProjectileVelocity = 2.5f;
        private void Attack(Player player, NPC[] nearbyNPCs)
        {
            Projectile[] minions = Main.projectile.Where(p => p.active && p.owner == player.whoAmI && p.minionSlots > 0).ToArray();
            for (int i = 0; i < minions.Length; i++)
            {
                Projectile element = minions[i];
                NPC target = nearbyNPCs.FirstOrDefault(npc => npc.boss);
                if (target == null)
                    target = nearbyNPCs[Main.rand.Next(nearbyNPCs.Length)];
                Vector2 projVelocity = target.Center - element.Center;
                projVelocity.Normalize();
                projVelocity *= attackProjectileVelocity;
                int projectile = Projectile.NewProjectile(element.Center, projVelocity, ProjectileID.DD2LightningBugZap, (int)(baseDamage * player.GetDamage(DamageClass.Summon) * player.GetDamage(DamageClass.Summon)), 1, player.whoAmI);
                Projectile proj = Main.projectile[projectile];
                proj.tileCollide = false;
                proj.hostile = false;
                proj.friendly = true;
                proj.timeLeft = 360;
                var gProj = proj.GetGlobalProjectile<wfGlobalProj>();
                gProj.AddProcChance(new ProcChance(BuffID.Electrified, 50));
                gProj.ai = () =>
                {
                    if (!target.active)
                        return;
                    var diff = target.Center - proj.Center;
                    diff.Normalize();
                    float distance = diff.Length();
                    if (distance <= projNpcSearchRange)
                    {
                        proj.velocity += diff * 0.4f;
                    }
                };
            }
        }
        const int projNpcSearchRange = 500;
        const int minionNpcSearchRange = 750;
        private NPC[] GetNearbyNPCs(Vector2 point)
        {
            return Main.npc.Where(npc => npc.active && !npc.friendly && npc.type != NPCID.TargetDummy &&(npc.Center - point).Length() <= minionNpcSearchRange).ToArray();
        }
    }
}