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
        public string name;
        public int timeLeft = 300;
        public int dmg = 0;

        public StackableProc(int damage, string typeName = null, int duration = 300)
        {
            name = typeName;
            timeLeft = duration;
            dmg = damage;
        }

        public void Update()
        {
            timeLeft -= 1;
            if (timeLeft <= 0) dmg = 0;
        }
    }
    public class wdfeerGlobalNPC : GlobalNPC
    {
        public override bool InstancePerEntity => true;
        public StackableProc[] slashProcs = new StackableProc[999];
        public int slashCounter
        {
            get => SlashCounter;
            set
            {
                if (value < 999) SlashCounter = value;
                else SlashCounter = 0;
            }
        }
        int SlashCounter = 0;
        public StackableProc[] electroProcs = new StackableProc[999];
        public int electroCounter
        {
            get => ElectroCounter;
            set
            {
                if (value < 999) ElectroCounter = value;
                else ElectroCounter = 0;
            }
        }
        int ElectroCounter = 0;
        public void AddStackableProc(string name, int duration, ref int damage)
        {
            switch (name)
            {
                case "slash":
                    slashProcs[slashCounter] = new StackableProc(damage, duration: duration);
                    slashCounter++;
                    break;
                case "electro":
                    electroProcs[electroCounter] = new StackableProc(damage, duration: duration);
                    electroCounter++;
                    break;
                default:
                    break;
            }
        }
        public Color baseColor;
        public override void SetDefaults(NPC npc)
        {
            base.SetDefaults(npc);

            Enumerable.Select<StackableProc, StackableProc>(slashProcs, x => new StackableProc(0));
            Enumerable.Select<StackableProc, StackableProc>(electroProcs, x => new StackableProc(0));
        }
        public override void ResetEffects(NPC npc)
        {
            if (!npc.HasBuff(BuffID.Frozen) && !npc.HasBuff(BuffID.Slow))
                baseColor = npc.color;
        }
        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            if ((npc.HasBuff(BuffID.Electrified)) && npc.lifeRegen > 0)
            {
                npc.lifeRegen = 0;
            }
            if (npc.HasBuff(BuffID.Electrified))
            {
                int totalDamage = 0;
                for(int i = 0; i < electroCounter; i++)
                    totalDamage += electroProcs[i].dmg;
                npc.lifeRegen -= totalDamage;
            }
            else ElectroCounter = 0;
            if (npc.HasBuff(mod.BuffType("SlashProc")))
            {
                int totalDamage = 0;
                for(int i = 0; i < slashCounter; i++)
                    totalDamage += slashProcs[i].dmg;
                npc.lifeRegen -= totalDamage;
            }
            else slashCounter = 0;
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
            {
                for (int i = 0; i < (npc.width < 32 ? 1 : npc.width / 32); i++)
                {
                    int dustIndex = Dust.NewDust(npc.position, npc.width, npc.height, 226, 0f, 0f, 67, default(Color), 0.5f);
                    var dust = Main.dust[dustIndex];
                    dust.noGravity = true;
                }

                for(int i = 0; i < electroCounter; i++)
                    electroProcs[i].Update();
            }

            if (npc.HasBuff(mod.BuffType("SlashProc")))
                for(int i = 0; i < slashCounter; i++)
                    slashProcs[i].Update();
        }
    }
}