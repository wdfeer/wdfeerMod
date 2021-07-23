using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace wdfeerMod
{
    public class wdfeerGlobalNPC : GlobalNPC
    {
        public override bool InstancePerEntity => true;
        public bool slashProc;
        public int slashProcs;
        public Color baseColor;
        public override void ResetEffects(NPC npc)
        {
            slashProc = false;
            if (!npc.HasBuff(BuffID.Frozen) && !npc.HasBuff(BuffID.Slow))
                baseColor = npc.color;
        }
        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            if (slashProc)
            {
                if (npc.lifeRegen > 0)
                {
                    npc.lifeRegen = 0;
                }
                npc.lifeRegen -= slashProcs * 2;
            }
            else slashProcs = 0;
            var baseV3 = baseColor.ToVector3();
            if (npc.HasBuff(BuffID.Frozen)) npc.color = new Color(0.82f * baseV3.X, 0.82f * baseV3.Y, baseV3.Z);
            else if (npc.HasBuff(BuffID.Slow)) npc.color = new Color(0.9f * baseV3.X, 0.9f * baseV3.Y, baseV3.Z);
            else npc.color = baseColor;
        }

        public override void AI(NPC npc)
        {
            if (npc.HasBuff(BuffID.Frozen) && !npc.boss) npc.velocity *= 0f;
            else if (npc.HasBuff(BuffID.Slow) && !npc.boss) npc.velocity *= 0.9f;
        }
    }
}