using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using System;

namespace wdfeerMod
{
    public class StackableProc
    {
        public int type; //0 for Slash, 1 for Electro
        public int timeLeft = 300;
        public int dmg = 0;

        public StackableProc(int Type, int damage, int duration = 300)
        {
            type = Type;
            timeLeft = duration;
            dmg = damage;
        }

        public void Update()
        {
            if (dmg == 0) return;
            timeLeft -= 1;
            if (timeLeft <= 0) dmg = 0;
        }
    }
    public class wdfeerGlobalNPC : GlobalNPC
    {
        public override bool InstancePerEntity => true;
        public StackableProc[] procs = new StackableProc[999];
        public int procCounter
        {
            get => ProcCounter;
            set
            {
                if (value < 999) ProcCounter = value;
                else ProcCounter = 0;
            }
        }
        int ProcCounter = 0;
        public void AddStackableProc(string name, int duration, ref int damage)
        {
            switch (name)
            {
                case "slash":
                    procs[procCounter] = new StackableProc(0, damage, duration: duration);
                    procCounter++;
                    break;
                case "electro":
                    procs[procCounter] = new StackableProc(1, damage, duration: duration);
                    procCounter++;
                    break;
                default:
                    break;
            }
        }
        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            if ((npc.HasBuff(BuffID.Electrified) || npc.HasBuff(mod.BuffType("SlashProc"))))
                npc.lifeRegen = 0;
            else procCounter = 0;
            if (npc.HasBuff(BuffID.Electrified))
            {
                int totalDamage = 0;
                for (int i = 0; i < procCounter; i++)
                    totalDamage += procs[i].type == 1 ? procs[i].dmg : 0;
                npc.lifeRegen -= totalDamage;
            }
            if (npc.HasBuff(mod.BuffType("SlashProc")))
            {
                int totalDamage = 0;
                for (int i = 0; i < procCounter; i++)
                    totalDamage += procs[i].type == 0 ? procs[i].dmg : 0;
                npc.lifeRegen -= totalDamage;
            }
        }

        public override void AI(NPC npc)
        {
            if (npc.HasBuff(BuffID.Frozen) && !npc.boss)
            {
                npc.velocity *= 0f;

                for (int i = 0; i < (npc.width < 48 ? 1 : npc.width / 48); i++)
                {
                    int dustIndex = Dust.NewDust(npc.position, npc.width, npc.height, 67, 0f, 0f, 67, default(Color), 1f);
                    var dust = Main.dust[dustIndex];
                    dust.noGravity = true;
                }
            }
            else if (npc.HasBuff(BuffID.Slow) && !npc.boss)
            {
                npc.velocity *= 0.9f;

                for (int i = 0; i < (npc.width < 48 ? 1 : npc.width / 48); i++)
                {
                    int dustIndex = Dust.NewDust(npc.position, npc.width, npc.height, 68, 0f, 0f, 67, default(Color), 0.6f);
                    var dust = Main.dust[dustIndex];
                    dust.velocity *= 0.2f;
                    dust.noGravity = true;
                }
            }

            if (npc.HasBuff(BuffID.Electrified))
            {
                for (int i = 0; i < (npc.width < 32 ? 1 : npc.width / 32); i++)
                {
                    int dustIndex = Dust.NewDust(npc.position, npc.width, npc.height, 226, 0f, 0f, 67, default(Color), 0.5f);
                    var dust = Main.dust[dustIndex];
                    dust.velocity *= 0.3f;
                    dust.noGravity = true;
                }
            }
            if (npc.HasBuff(BuffID.Electrified) || npc.HasBuff(mod.BuffType("SlashProc")))
                for (int i = 0; i < procCounter; i++)
                    procs[i].Update();
        }
    }
}