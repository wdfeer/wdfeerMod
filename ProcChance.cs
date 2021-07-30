using Terraria;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;

namespace wdfeerMod
{
    public class ProcChance
    {
        public int buffID;
        public int chance;
        public int duration;
        public ProcChance(int id, int probability, int dur = 300)
        {
            buffID = id;
            chance = probability;
            duration = dur;
        }
        public bool Proc(NPC target)
        {
            if (Main.rand.Next(0, 100) <= chance)
            {
                target.AddBuff(buffID, duration);
                return true;
            }
            else return false;
        }
    }
}