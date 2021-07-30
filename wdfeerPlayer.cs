using System;
using System.Linq;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace wdfeerMod
{
    public class wdfeerPlayer : ModPlayer
    {
        public bool vitalS;
        public bool condOv;
        public bool aviator;
        public bool corrProj;
        public bool hunterMuni;
        public float electroMult = 1;
        public List<ProcChance> procChances = new List<ProcChance>();
        public bool slashProc;
        public int slashProcs;
        public override void ResetEffects()
        {
            vitalS = false;
            condOv = false;
            aviator = false;
            corrProj = false;
            hunterMuni = false;
            electroMult = 1;

            procChances = new List<ProcChance>();

            slashProc = false;
        }
        public override void UpdateBadLifeRegen()
        {
            if (slashProc)
            {
                if (player.lifeRegen > 0)
                {
                    player.lifeRegen = 0;
                }
                player.lifeRegen -= slashProcs * 2;
            }
            else slashProcs = 0;
        }
        public override void ModifyHitByNPC(NPC npc, ref int damage, ref bool crit)
        {
            if (aviator)
            {
                player.UpdateTouchingTiles();
                if (!player.TouchedTiles.Any()) damage = Convert.ToInt32(damage * 0.75f);
            }
        }
        public override void ModifyHitByProjectile(Projectile proj, ref int damage, ref bool crit)
        {
            if (aviator)
            {
                player.UpdateTouchingTiles();
                if (!player.TouchedTiles.Any()) damage = Convert.ToInt32(damage * 0.75f);
            }
        }
        public override void ModifyHitNPC(Item item, NPC target, ref int damage, ref float knockback, ref bool crit)
        {
            damage = vitalS && crit ? Convert.ToInt32(damage * 1.25f) : damage;

            if (condOv)
            {
                float buffs = 0;
                for (int i = 0; i < BuffLoader.BuffCount; i++)
                {
                    buffs = target.HasBuff(i) ? buffs + 1 : buffs;
                }
                float dmg = damage;
                dmg = dmg * (1 + buffs / 10);
                damage = Convert.ToInt32(dmg);
            }
            if (corrProj)
            {
                float defMult = Main.expertMode ? 0.75f : 0.5f;
                damage += Convert.ToInt32(target.defense * defMult * 0.18f);
            }

            if (hunterMuni && crit)
                new ProcChance(mod.BuffType("SlashProc"), 30, 360).Proc(target);
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
                        electroDamage = (int)(electroDamage * electroMult);
                        target.GetGlobalNPC<wdfeerGlobalNPC>().AddStackableProc("electro", 300, ref electroDamage);
                    }
                }
            }
        }

        public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {

            damage = vitalS && crit ? Convert.ToInt32(damage * 1.25f) : damage;
            if (condOv)
            {
                float buffs = 0;
                for (int i = 0; i < BuffLoader.BuffCount; i++)
                {
                    buffs = target.HasBuff(i) ? buffs + 1 : buffs;
                }
                float dmg = damage;
                dmg = dmg * (1 + buffs / 10);
                damage = Convert.ToInt32(dmg);
            }

            if (corrProj)
            {
                float defMult = Main.expertMode ? 0.75f : 0.5f;
                damage += Convert.ToInt32(target.defense * defMult * 0.18f);
            }
        }
        public Vector2 offsetP;
        public float speedXP;
        public float speedYP;
        public int typeP;
        public int damageP;
        public float knockbackP;
        public int burstInterval = -1;
        public int burstsMax = 2;
        public int burstCount = 1;
        int burstTimer = 0;
        public int longTimer = 0;
        public Items.Weapons.wdfeerWeapon burstItem;
        public override void PreUpdate()
        {
            if (player.dead) return;
            longTimer++;
            if (burstInterval != -1)
            {
                if (burstCount < burstsMax)
                {
                    burstTimer++;
                    if (burstTimer >= burstInterval)
                    {
                        burstCount++;
                        burstTimer = 0;

                        Vector2 posi = player.position + offsetP;
                        float x = speedXP;
                        float y = speedYP;
                        int t = typeP;
                        int dmg = damageP;
                        float kb = knockbackP;
                        burstItem.Shoot(player, ref posi, ref x, ref y, ref t, ref dmg, ref kb);
                    }
                }
                else
                {
                    burstInterval = -1;
                    burstsMax = 2;
                    burstCount = 1;
                }
            }
        }
    }
}