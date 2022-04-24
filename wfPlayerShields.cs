using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
namespace wfMod
{
    public class wfPlayerShields : ModPlayer
    {
        public int shield = 0;
        public int maxShield = 0;
        public int shieldRegenInterval => maxShield == 0 ? 0 : 1800 / maxShield;
        public override void ResetEffects()
        {
            maxShield = 0;
        }
        private int shieldRegenTimer = 0;
        public override void PreUpdate()
        {
            shieldRegenTimer++;
            if (shieldRegenTimer >= shieldRegenInterval && shield < maxShield)
            {
                shield++;
                shieldRegenTimer = 0;
            }

            if (shield > 0 && maxShield > 0)
                UpdateShieldVisualEffect();
        }
        float dustDistance = 100;
        private void UpdateShieldVisualEffect()
        {
            float intensity = ModContent.GetInstance<wfServerConfig>().shieldEffectsIntensity;
            float circlePortion = (float)shield / (float)maxShield;
            double radians = circlePortion * 2 * Math.PI - Math.PI / 2;
            int particles = intensity == 1 ? shield * 2 : shield;
            wfMod.NewDustsCustom(particles, () =>
            {
                Vector2 pos = new Vector2((float)(dustDistance*Math.Cos(radians)), (float)(dustDistance*Math.Sin(radians)));
                radians -= 2 * Math.PI / particles * circlePortion;
                pos += player.Center;
                var dust = Dust.NewDustPerfect(pos, DustID.SapphireBolt);
                dust.scale = (shield == maxShield ? 0.5f : 0.3f) * intensity;
                return dust;
            });
        }
        public int ModifyIncomingDamage(int damage)
        {
            if (shield <= 0) return damage;
            
            if (shield < damage)
            {
                damage -= shield;
                if (shield == maxShield)
                {
                    shield = 0;
                    return 0;
                }
                shield = 0;
                return damage;
            }
            else
            {
                shield -= damage;
                return 0;
            }
        }
        public override void ModifyHitByNPC(NPC npc, ref int damage, ref bool crit)
        {
            damage = ModifyIncomingDamage(damage);
        }
        public override void ModifyHitByProjectile(Projectile proj, ref int damage, ref bool crit)
        {
            damage = ModifyIncomingDamage(damage);
        }
    }
}

