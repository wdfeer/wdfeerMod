using Terraria;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;

namespace wdfeerMod
{
    public class ProcChance
    {
        public int buffID;
        public float chance;
        public int duration;
        public ProcChance(int id, int probability, int dur = 300)
        {
            buffID = id;
            chance = probability;
            duration = dur;
        }
        public bool Proc(NPC target)
        {
            if (Main.rand.NextFloat(0, 100) <= chance)
            {
                target.AddBuff(buffID, duration);
                return true;
            }
            else return false;
        }
        public static float AddChance(float chance1, float chance2)
        {
            return 100 - ((100 - chance1) / 100) * ((100 - chance2) / 100);
        }
    }
}