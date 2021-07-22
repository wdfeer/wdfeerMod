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

        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            if (slashProc) 
            {
				if (npc.lifeRegen > 0) {
					npc.lifeRegen = 0;
				}
				npc.lifeRegen -= slashProcs*2;
            }else slashProcs = 0;
        }
    }
}