using System;
using Terraria;
using Terraria.ModLoader;
namespace wfMod
{
    public class wfPlayerShields : ModPlayer
    {
        public int shield = 0;
        public int maxShield = 0;
        public int shieldRegenInterval => maxShield == 0 ? 0 : 300 / maxShield;
        public override void ResetEffects()
        {
            maxShield = 0;
        }
        private int shieldRegenTimer = 0;
        public override void PreUpdate()
        {
            shieldRegenTimer++;
            if (shieldRegenTimer >= shieldRegenInterval)
            {
                shield++;
                shieldRegenTimer = 0;
            }
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

