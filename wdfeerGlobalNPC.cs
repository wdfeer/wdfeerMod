using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace wdfeerMod
{
	public class wdfeerGlobalNPC : GlobalNPC
	{
        public override bool InstancePerEntity => true;
        public bool slashProc;
        public int slashProcs;
        public override void ResetEffects(NPC npc)
        {
            slashProc = false;
        }
        public override void SetDefaults(NPC npc)
        {
            
        }
        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            if (slashProc) 
            {
				if (npc.lifeRegen > 0) {
					npc.lifeRegen = 0;
				}
				npc.lifeRegen -= slashProcs*2;
            }else slashProcs = 0;

            if (npc.HasBuff(BuffID.Frozen)) npc.color = new Color(0.5f,0.5f,1f);
            else if (npc.HasBuff(BuffID.Slow)) npc.color = new Color(0.8f,0.8f,1f);
            else npc.color = new Color(1f,1f,1f);
        }

        public override void AI(NPC npc)
        {
            if (npc.HasBuff(BuffID.Frozen) && !npc.boss) npc.velocity*=0f;
            else if (npc.HasBuff(BuffID.Slow) && !npc.boss) npc.velocity*=0.75f;
        }
    }
}