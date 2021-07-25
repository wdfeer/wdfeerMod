using System;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace wdfeerMod
{
    public class wdfeerPlayer : ModPlayer
    {
        public bool vitalS;
        public bool condOv;
        public bool aviator;
        public bool corrProj;
        public bool hunterMuni;
        public bool slashProc;
        public int slashProcs;
        public override void ResetEffects()
        {
            vitalS = false;
            condOv = false;
            aviator = false;
            corrProj = false;
            hunterMuni = false;

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
            if (hunterMuni && crit && Main.rand.Next(0, 100) < 30)
            {
                target.AddBuff(mod.BuffType("SlashProc"), 360);
                var slashDamage = damage / 5;
                target.GetGlobalNPC<wdfeerGlobalNPC>().AddStackableProc("slash", 360, ref slashDamage);
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
            if (hunterMuni && crit && Main.rand.Next(0, 100) < 30)
            {
                target.AddBuff(mod.BuffType("SlashProc"), 240);
                var slashDamage = damage / 5;
                target.GetGlobalNPC<wdfeerGlobalNPC>().AddStackableProc("slash", 240, ref slashDamage);
            }
        }
    }
}