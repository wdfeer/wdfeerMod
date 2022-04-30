using System;
using System.Collections.Generic;
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
        public int MaxShield => maxUnderShield + maxOvershield;
        public float UnderShield => shield - Overshield;
        public float Overshield => shield > maxUnderShield ? shield - maxUnderShield : 0;
        public int maxOvershield = 100;
        public int maxUnderShield = 0;
        public int shieldRegenInterval => maxUnderShield == 0 ? 0 : (int)(1800 / maxUnderShield / shieldRegen);
        public float shieldRegen = 1;
        public List<Action<float, int>> onShieldsDamaged;
        public override void ResetEffects()
        {
            maxUnderShield = 0;
            consumedManaToShieldConversion = 0;
            shieldRegen = 1;
            onShieldsDamaged = new List<Action<float, int>>();
        }
        private int shieldRegenTimer = 0;
        public override void PreUpdate()
        {
            if (shield > MaxShield)
                shield = MaxShield;

            if (immuneTimeNeedsToBeModified && player.immuneTime > 0)
            {
                player.immuneTime /= 2;
                immuneTimeNeedsToBeModified = false;
            }

            shieldRegenTimer++;
            if (shieldRegenTimer >= shieldRegenInterval && shield < maxUnderShield)
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
            if (UnderShield > 0)
            {
                float circlePortion = shield / maxUnderShield;
                int particles = intensity == 1 ? (int)shield * 2 : (int)shield;
                if (particles > maxParticles)
                    particles = maxParticles;
                NewDustInAPortionOfACircleEvenly(particles, circlePortion, -Math.PI / 2, DustID.SapphireBolt, shieldDustDistance, shield >= maxUnderShield ? 0.4f : 0.6f);
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
        public (float, int) ModifyIncomingDamage(int damage)
        {
            if (shield <= 0) return (0, damage);

            if (shield < damage)
            {
                // Shield Gating
                if (shield == maxUnderShield || (shield > maxUnderShield && maxUnderShield != 0))
                {
                    immuneTimeNeedsToBeModified = true;
                    return (shield, 0);
                }
                return (shield, damage - (int)shield);
            }
            else
            {
                return (damage, 0);
            }
        }
        int ModifyHurt(int damage)
        {
            (float damageToShield, int damageToHealth) = ModifyIncomingDamage(damage);
            shield -= damageToShield;
            if (damageToShield > 0)
                CombatText.NewText(player.getRect(), Color.Cyan, (int)damageToShield);
            damage = damageToHealth;

            onShieldsDamaged.ForEach(act => act(damageToShield, damageToHealth));
            return damage;
        }
        public override void ModifyHitByNPC(NPC npc, ref int damage, ref bool crit)
        {
            if (shield > 0)
                damage = ModifyHurt(damage);
        }
        public override void ModifyHitByProjectile(Projectile proj, ref int damage, ref bool crit)
        {
            if (shield > 0)
                damage = ModifyHurt(damage);
        }
        public override void OnConsumeMana(Item item, int manaConsumed)
        {
            shield += manaConsumed * consumedManaToShieldConversion;
        }
        public override void UpdateDead()
        {
            shield = 0;
        }
    }
}

