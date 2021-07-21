using wdfeerMod;
using System;
using System.Linq;
using Terraria;
using Terraria.ModLoader;

namespace wdfeerMod
{
    public class wdfeerPlayer : ModPlayer
    {
        public bool vitalS;
        public bool condOv;
        public bool aviator;
        public bool corrProj;
        public override void ResetEffects()
        {
            vitalS=false;
            condOv=false;
            aviator=false;
            corrProj=false;
        }
        public override void ModifyHitByNPC(NPC npc, ref int damage, ref bool crit)
        {
            if (aviator)
            {
                player.UpdateTouchingTiles();
                if (!player.TouchedTiles.Any()) damage=Convert.ToInt32(damage*0.75f);                
            } 
        }
        public override void ModifyHitByProjectile(Projectile proj, ref int damage, ref bool crit)
        {
            if (aviator)
            {
                player.UpdateTouchingTiles();
                if (!player.TouchedTiles.Any()) damage=Convert.ToInt32(damage*0.75f);                
            } 
        }
        public override void ModifyHitNPC(Item item,NPC target,ref int damage,ref float knockback,ref bool crit )
        {      
            damage = vitalS && crit ? Convert.ToInt32(damage * 1.4f) : damage;     
                           
            if (condOv)
            {
                float buffs = 0;        
                for (int i = 0; i < BuffLoader.BuffCount; i++)
                {
                    buffs = target.HasBuff(i) ? buffs+1 : buffs;
                }
                float dmg = damage;
                dmg = dmg * (1+buffs/10);
                damage=Convert.ToInt32(dmg);  
            }        
            if (corrProj)
            {
                float defMult = Main.expertMode ? 0.75f : 0.5f;
                damage+=Convert.ToInt32(target.defense*defMult*0.18f);
            }
        }

        public override void ModifyHitNPCWithProj(Projectile proj,NPC target,ref int damage,ref float knockback,ref bool crit , ref int hitDirection)
        {        
            damage = vitalS && crit ? Convert.ToInt32(damage * 1.4f) : damage;           
            if (condOv)
            {             
                float buffs = 0;        
                for (int i = 0; i < BuffLoader.BuffCount; i++)
                {
                    buffs = target.HasBuff(i) ? buffs+1 : buffs;
                }
                float dmg = damage;
                dmg = dmg * (1+buffs/10);
                damage=Convert.ToInt32(dmg);
            }   
            if (corrProj)
            {
                float defMult = Main.expertMode ? 0.75f : 0.5f;
                damage+=Convert.ToInt32(target.defense*defMult*0.18f);
            }   
        }
    }
}