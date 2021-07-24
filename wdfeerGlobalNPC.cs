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
        public int electroProcs = 0;
        public Color baseColor;
        public override void ResetEffects(NPC npc)
        {
            slashProc = false;
            if (!npc.HasBuff(BuffID.Frozen) && !npc.HasBuff(BuffID.Slow))
                baseColor = npc.color;
        }
        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            if ((npc.HasBuff(BuffID.Electrified) || slashProc) && npc.lifeRegen > 0)
            {
                npc.lifeRegen = 0;
            }
            if (npc.HasBuff(BuffID.Electrified))
            {
                npc.lifeRegen -= (electroProcs > 0 ? electroProcs * 2 / 3 : 8);
            }
            else electroProcs = 0;
            if (slashProc)
            {
                npc.lifeRegen -= slashProcs * 2 / 3;
            }
            else slashProcs = 0;
            var baseV3 = baseColor.ToVector3();
            if (npc.HasBuff(BuffID.Frozen)) npc.color = new Color(0.9f * baseV3.X, 0.9f * baseV3.Y, baseV3.Z);
            else if (npc.HasBuff(BuffID.Slow)) npc.color = new Color(0.95f * baseV3.X, 0.95f * baseV3.Y, baseV3.Z);
            else npc.color = baseColor;
        }

        public override void AI(NPC npc)
        {
            if (npc.HasBuff(BuffID.Frozen) && !npc.boss) npc.velocity *= 0f;
            else if (npc.HasBuff(BuffID.Slow) && !npc.boss) npc.velocity *= 0.9f;

            if (npc.HasBuff(BuffID.Electrified))
                for (int i = 0; i < (npc.width < 32 ? 1 : npc.width / 32); i++)
                {
                    int dustIndex = Dust.NewDust(npc.position, npc.width, npc.height, 226, 0f, 0f, 67, default(Color), 0.5f);
                    var dust = Main.dust[dustIndex];
                    dust.noGravity = true;
                }
        }
    }
}