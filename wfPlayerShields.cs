using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
namespace wfMod
{
    public class wfPlayerShields : ModPlayer
    {
        public float consumedManaToShieldConversion = 0f;

        public float shield = 0;
        public float Shield => shield - Overshield;
        public float Overshield => shield > maxShield ? shield - maxShield : 0;
        public int maxOvershield = 100;
        public int maxShield = 0;
        public int Maxshield => maxShield + maxOvershield;
        public int shieldRegenInterval => maxShield == 0 ? 0 : (int)(1800 / maxShield / shieldRegen);
        public float shieldRegen = 1;
        public override void ResetEffects()
        {
            maxShield = 0;
            consumedManaToShieldConversion = 0;
            shieldRegen = 1;
        }
        private int shieldRegenTimer = 0;
        public override void PreUpdate()
        {
            if (shield > Maxshield)
                shield = Maxshield;

            if (immuneTimeNeedsToBeModified && player.immuneTime > 0)
            {
                player.immuneTime /= 2;
                immuneTimeNeedsToBeModified = false;
            }

            shieldRegenTimer++;
            if (shieldRegenTimer >= shieldRegenInterval && shield < maxShield)
            {
                shield++;
                shieldRegenTimer = 0;
            }

            UpdateShieldVisualEffect();
        }
        const float shieldDustDistance = 75;
        const float overshieldDustDistance = 100;
        private void UpdateShieldVisualEffect()
        {
            float intensity = ModContent.GetInstance<wfServerConfig>().shieldEffectsIntensity;
            int maxParticles = (int)(24 * intensity);
            void NewDustInAPortionOfACircleEvenly(int count, float circlePortion, double phase, int type, float distance, float scale)
            {
                double radians = circlePortion * 2 * Math.PI + phase;
                wfMod.NewDustsCustom(count, () =>
                {
                    Vector2 pos = new Vector2((float)(distance * Math.Cos(radians)), (float)(distance * Math.Sin(radians)));
                    radians -= 2 * Math.PI / count * circlePortion;
                    pos += player.Center;
                    var dust = Dust.NewDustPerfect(pos, type);
                    dust.scale = scale * intensity;
                    return dust;
                });
            }
            if (Shield > 0)
            {
                float circlePortion = shield / maxShield;
                int particles = intensity == 1 ? (int)shield * 2 : (int)shield;
                if (particles > maxParticles)
                    particles = maxParticles;
                NewDustInAPortionOfACircleEvenly(particles, circlePortion, -Math.PI / 2, DustID.SapphireBolt, shieldDustDistance, shield >= maxShield ? 0.4f : 0.6f);
            }
            if (Overshield > 0)
            {
                float circlePortion = Overshield / maxOvershield;
                int particles = intensity == 1 ? (int)Overshield : (int)Overshield / 2;
                if (particles > maxParticles + maxParticles / 4)
                    particles = maxParticles + maxParticles / 4;
                NewDustInAPortionOfACircleEvenly(particles, circlePortion, -Math.PI / 2, DustID.AmethystBolt, overshieldDustDistance, 0.7f);
            }
        }
        private bool immuneTimeNeedsToBeModified = false;
        public int ModifyIncomingDamage(int damage)
        {
            if (shield <= 0) return damage;

            if (shield < damage)
            {
                damage -= (int)shield;
                if (shield == maxShield || (shield > maxShield && maxShield != 0))
                {
                    shield = 0;
                    immuneTimeNeedsToBeModified = true;
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
        public override void OnConsumeMana(Item item, int manaConsumed)
        {
            shield += manaConsumed * consumedManaToShieldConversion;
        }
    }
}

