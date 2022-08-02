using System;
using System.Linq;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using wfMod.Items.Accessories;

namespace wfMod
{
    public class wfPlayer : ModPlayer
    {
        public static bool thermiteRounds;
        public bool condOv;
        public bool aviator;
        public bool corrProj;
        public bool hunterMuni;
        public bool berserker;
        public bool avenger;
        public bool guardian;
        public bool acceleration;
        public bool hypeThrusters;
        public bool quickThink;
        public bool synthDeconstruct;
        public bool internalBleed;
        public bool argonScope;
        public bool napalmGrenades = false;
        public bool energyConversion;
        public bool arcaneStrike;
        public bool arcaneEnergize;
        public bool arcanePulse;
        public bool desecrate;
        public float projLifetime = 1;
        public int BerserkerProcs { get => berserkerProcs; set => berserkerProcs = value > 3 ? 3 : value; }
        private int berserkerProcs;
        public float electroMult = 1;
        public Dictionary<int, ProcChance> procChances = new Dictionary<int, ProcChance>();
        public void AddProcChance(ProcChance procChance)
        {
            if (!procChances.ContainsKey(procChance.buffID))
                procChances.Add(procChance.buffID, procChance);
            else procChances[procChance.buffID].chance = ProcChance.AddChance(procChances[procChance.buffID].chance, procChance.chance);
        }
        public List<ProcChance> tempProcChances = new List<ProcChance>();
        public bool slashProc;
        public int slashProcs;
        public int arcaSciscoStacks
        {
            get => ArcaSciscoStacks;
            set
            {
                if (value > 4) value = 4;
                ArcaSciscoStacks = value;
            }
        }
        int ArcaSciscoStacks = 0;
        public float spreadMult;
        public int penetrate = 0; //Extra projectile penetration
        public float fireRateMult = 1;
        public float critDmgMult = 1;
        public override void ResetEffects()
        {
            condOv = false;
            aviator = false;
            corrProj = false;
            hunterMuni = false;
            berserker = false;
            avenger = false;
            guardian = false;
            acceleration = false;
            hypeThrusters = false;
            quickThink = false;
            synthDeconstruct = false;
            internalBleed = false;
            energyConversion = false;
            argonScope = false;
            arcaneStrike = false;
            arcaneEnergize = false;
            arcanePulse = false;
            desecrate = false;
            projLifetime = 1;

            electroMult = 1;
            spreadMult = 0;
            penetrate = 0;
            fireRateMult = 1;
            critDmgMult = 1;
            procChances = new Dictionary<int, ProcChance>();

            slashProc = false;
        }
        public override void SaveData(TagCompound tag)/* tModPorter Suggestion: Edit tag parameter instead of returning new TagCompound */
        {
            return new TagCompound {
                {"napalmGrenades", napalmGrenades}
            };
        }
        public override void LoadData(TagCompound tag)
        {
            napalmGrenades = tag.GetBool("napalmGrenades");
        }
        public override void clientClone(ModPlayer clientClone)
        {
            wfPlayer clone = clientClone as wfPlayer;
            // Here we would make a backup clone of values that are only correct on the local players Player instance.
            // Some examples would be RPG stats from a GUI, Hotkey states, and Extra Item Slots
            clone.napalmGrenades = napalmGrenades;
        }

        public override void SyncPlayer(int toWho, int fromWho, bool newPlayer)
        {
            ModPacket packet = Mod.GetPacket();
            packet.Write((byte)wfMessageType.SyncPlayer);
            packet.Write((byte)Player.whoAmI);
            packet.Write(napalmGrenades);
            packet.Send(toWho, fromWho);
        }

        public override void SendClientChanges(ModPlayer clientPlayer)
        {
            // Here we would sync something like an RPG stat whenever the player changes it.
            wfPlayer clone = clientPlayer as wfPlayer;
            if (clone.napalmGrenades != napalmGrenades)
            {
                // Send a Mod Packet with the changes.
                var packet = Mod.GetPacket();
                packet.Write((byte)wfMessageType.NapalmGrenadesChanged);
                packet.Write((byte)Player.whoAmI);
                packet.Write(napalmGrenades);
                packet.Send();
            }
        }
        public override void UpdateBadLifeRegen()
        {
            if (slashProc)
            {
                if (Player.lifeRegen > 0)
                {
                    Player.lifeRegen = 0;
                }
                Player.lifeRegen -= slashProcs * 2;
            }
            else slashProcs = 0;

            if (!Player.HasBuff(Mod.Find<ModBuff>("BerserkerBuff").Type)) BerserkerProcs = 0;
            if (!Player.HasBuff(Mod.Find<ModBuff>("ArcaSciscoBuff").Type))
            {
                if (arcaSciscoStacks > 1)
                {
                    Player.AddBuff(Mod.Find<ModBuff>("ArcaSciscoBuff").Type, 180);
                    arcaSciscoStacks--;
                }
                else arcaSciscoStacks = 0;
            }
        }
        public override bool PreHurt(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
            if (aviator)
            {
                Player.UpdateTouchingTiles();
                if (!Player.TouchedTiles.Any()) damage = (int)(damage * 0.75f);
            }

            return true;
        }
        public override void ModifyHitByNPC(NPC npc, ref int damage, ref bool crit)
        {
            if (avenger && damage > 4 && !npc.SpawnedFromStatue && Main.rand.Next(100) < 35)
            {
                Player.AddBuff(Mod.Find<ModBuff>("ArcaneAvengerBuff").Type, 720);
            }
            if (guardian && damage > 4 && !npc.SpawnedFromStatue && Main.rand.Next(100) < 35)
            {
                Player.AddBuff(Mod.Find<ModBuff>("ArcaneGuardianBuff").Type, 1200);
            }

            if (npc.HasBuff(BuffID.Weak)) damage = (int)(damage * (npc.boss ? 0.88f : 0.8f));
        }
        public override void ModifyHitByProjectile(Projectile proj, ref int damage, ref bool crit)
        {
            if (avenger && damage > 4 && Main.rand.Next(100) < 21)
            {
                Player.AddBuff(Mod.Find<ModBuff>("ArcaneAvengerBuff").Type, 720);
            }
            if (guardian && damage > 4 && Main.rand.Next(100) < 21)
            {
                Player.AddBuff(Mod.Find<ModBuff>("ArcaneGuardianBuff").Type, 1200);
            }

            if (proj.owner <= Main.npc.Length && proj.owner >= 0)
            {
                var npc = Main.npc[proj.owner];
                if (npc.HasBuff(BuffID.Weak)) damage = (int)(damage * (npc.boss ? 0.88f : 0.8f));
            }

        }
        public override bool PreKill(double damage, int hitDirection, bool pvp, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
            if (quickThink && !Player.HasBuff(BuffID.ManaSickness))
            {
                if (Player.statMana > damage * 8)
                {
                    Player.statMana -= (int)(damage * 8);
                    return false;
                }
                else Player.statMana = 0;
            }
            return base.PreKill(damage, hitDirection, pvp, ref playSound, ref genGore, ref damageSource);
        }
        public override void ModifyHitNPC(Item item, NPC target, ref int damage, ref float knockback, ref bool crit)
        {
            damage = crit ? (int)(damage * critDmgMult) : damage;

            if (condOv)
            {
                float buffs = 0;
                for (int i = 0; i < BuffLoader.BuffCount; i++)
                {
                    buffs = target.HasBuff(i) ? buffs + 1 : buffs;
                }
                float dmg = damage;
                dmg = dmg * (1 + buffs / 10);
                damage = (int)(dmg);
            }
            if (corrProj)
            {
                float defMult = Main.expertMode ? 0.75f : 0.5f;
                damage += (int)(target.defense * defMult * 0.18f);
            }
            if (berserker && crit)
            {
                Player.AddBuff(Mod.Find<ModBuff>("BerserkerBuff").Type, 360);
                BerserkerProcs++;
            }
            if (arcaneStrike && Main.rand.Next(100) < 15)
                Player.AddBuff(Mod.Find<ModBuff>("ArcaneStrikeBuff").Type, 1080);

            if (hunterMuni && crit)
                tempProcChances.Add(new ProcChance(Mod.Find<ModBuff>("SlashProc").Type, 30, 360));
            if (internalBleed)
                tempProcChances.Add(new ProcChance(Mod.Find<ModBuff>("SlashProc").Type, (int)(30f * (knockback / 20f)), 240));
            #region Procs
            foreach (var procChance in procChances)
            {
                var proc = procChance.Value;
                if (proc.Proc(target))
                {
                    if (proc.buffID == Mod.Find<ModBuff>("SlashProc").Type)
                    {
                        int slashDamage = damage / 5;
                        target.GetGlobalNPC<wfGlobalNPC>().AddStackableProc(ProcType.Slash, 300, slashDamage);
                    }
                    else if (proc.buffID == BuffID.Electrified)
                    {
                        int electroDamage = damage / 5 - target.defense * (Main.expertMode ? 3 / 4 : 1 / 2);
                        electroDamage = (int)(electroDamage * electroMult);
                        target.GetGlobalNPC<wfGlobalNPC>().AddStackableProc(ProcType.Electricity, 300, electroDamage);
                    }
                }
            }
            foreach (var proc in tempProcChances)
            {
                if (proc.Proc(target))
                {
                    if (proc.buffID == Mod.Find<ModBuff>("SlashProc").Type)
                    {
                        int slashDamage = damage / 5;
                        target.GetGlobalNPC<wfGlobalNPC>().AddStackableProc(ProcType.Slash, 300, slashDamage);
                    }
                    else if (proc.buffID == BuffID.Electrified)
                    {
                        int electroDamage = damage / 5 - target.defense * (Main.expertMode ? 3 / 4 : 1 / 2);
                        electroDamage = (int)(electroDamage * electroMult);
                        target.GetGlobalNPC<wfGlobalNPC>().AddStackableProc(ProcType.Electricity, 300, electroDamage);
                    }
                }
            }
            tempProcChances = new List<ProcChance>();
            #endregion
        }
        public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (proj.minion && ModContent.GetInstance<wfServerConfig>().minionCrits)
            {
                int[] cc = { Player.GetCritChance(DamageClass.Generic), Player.GetCritChance(DamageClass.Magic), Player.GetCritChance(DamageClass.Ranged) };
                if (Main.rand.Next(100) < cc.Min()) crit = true;
                else crit = false;
            }
            if (crit)
            {
                damage = (int)(damage * critDmgMult);
            }
            if (condOv)
            {
                float buffs = 0;
                for (int i = 0; i < BuffLoader.BuffCount; i++)
                {
                    buffs = target.HasBuff(i) ? buffs + 1 : buffs;
                }
                float dmg = damage;
                dmg += dmg * buffs * ConditionOverload.damagePerStatus;
                damage = (int)(dmg);
            }
            if (corrProj)
            {
                float defMult = Main.expertMode ? 0.75f : 0.5f;
                damage += (int)(target.defense * defMult * 0.18f);
            }
            if (berserker && proj.CountsAsClass(DamageClass.Melee) && crit)
            {
                Player.AddBuff(Mod.Find<ModBuff>("BerserkerBuff").Type, 360);
                BerserkerProcs++;
            }
            if (argonScope && (Vector2.Normalize(target.velocity) + Vector2.Normalize(proj.velocity)).Length() < 0.4f)
                Player.AddBuff(Mod.Find<ModBuff>("ArgonScopeBuff").Type, 541);
            if (arcaneStrike && Main.rand.Next(100) < 15)
                Player.AddBuff(Mod.Find<ModBuff>("ArcaneStrikeBuff").Type, 1080);
            if (acceleration && crit && !proj.CountsAsClass(DamageClass.Melee) && Main.rand.Next(100) < 30)
                Player.AddBuff(Mod.Find<ModBuff>("ArcaneAccelerationBuff").Type, 540);
        }
        public override float UseTimeMultiplier(Item item)
        {
            if (item.noMelee && item.type != Mod.Find<ModItem>("Opticor").Type && item.type != Mod.Find<ModItem>("OpticorVandal").Type)
                return fireRateMult;
            return base.UseTimeMultiplier(item);
        }
        public Vector2 offsetP;
        public float speedXP;
        public float speedYP;
        public int typeP;
        public int damageP;
        public float knockbackP;
        public int burstInterval = -1;
        public int burstsMax = 2;
        public int burstCount = 1;
        int burstTimer = 0;
        public int longTimer = 0;
        public Items.Weapons.wfWeapon burstItem;
        public override void PreUpdate()
        {
            if (Player.dead) return;
            longTimer++;
            #region burst
            if (burstInterval != -1)
            {
                if (burstCount < burstsMax)
                {
                    burstTimer++;
                    if (burstTimer >= burstInterval)
                    {
                        burstCount++;
                        burstTimer = 0;

                        Vector2 posi = Player.position + offsetP;
                        float x = speedXP;
                        float y = speedYP;
                        int t = typeP;
                        int dmg = damageP;
                        float kb = knockbackP;
                        burstItem.Shoot(Player, ref posi, ref x, ref y, ref t, ref dmg, ref kb);
                    }
                }
                else
                {
                    burstInterval = -1;
                    burstsMax = 2;
                    burstCount = 1;
                }
            }
            #endregion   
            GrabItems();
        }
        void GrabItems() // Needed for the Arcane Energize and Pulse, Energy Conversion
        {
            if (!arcaneEnergize && !arcanePulse && !energyConversion) return;
            for (int i = 0; i < 400; i++)
            {
                if (!Main.item[i].active || Main.item[i].noGrabDelay != 0 || Main.item[i].playerIndexTheItemIsReservedFor != this.Player.whoAmI || !ItemLoader.CanPickup(Main.item[i], this.Player))
                {
                    continue;
                }

                if (new Rectangle((int)this.Player.position.X - 2, (int)this.Player.position.Y - 2, this.Player.width + 2, this.Player.height + 2).Intersects(new Rectangle((int)Main.item[i].position.X, (int)Main.item[i].position.Y, Main.item[i].width, Main.item[i].height)))
                {
                    if ((Main.item[i].type == 184 || Main.item[i].type == 1735 || Main.item[i].type == 1868))
                    {
                        if (energyConversion && !Player.HasBuff(Mod.Find<ModBuff>("EnergyConversionBuff").Type))
                        {
                            Player.AddBuff(Mod.Find<ModBuff>("EnergyConversionBuff").Type, 3600);
                        }
                        if (!Main.item[i].GetGlobalItem<Items.wfGlobalItem>().energized && arcaneEnergize)
                        {
                            if (wfMod.Roll(60))
                            {
                                this.Player.statMana += 100;
                                this.Player.ManaEffect(100);
                                SoundEngine.PlaySound(SoundID.MenuTick);
                            }

                            Main.item[i].GetGlobalItem<Items.wfGlobalItem>().energized = true;
                        }
                    }
                    else if ((Main.item[i].type == 58 || Main.item[i].type == 1734 || Main.item[i].type == 1867) && arcanePulse && !Main.item[i].GetGlobalItem<Items.wfGlobalItem>().energized && !this.Player.HasBuff(Mod.Find<ModBuff>("ArcanePulseBuff").Type))
                    {
                        if (Main.rand.Next(100) < 60)
                        {
                            this.Player.statLife += 40;
                            this.Player.HealEffect(40);
                            this.Player.AddBuff(Mod.Find<ModBuff>("ArcanePulseBuff").Type, 900);
                            SoundEngine.PlaySound(SoundID.MenuTick);
                        }

                        Main.item[i].GetGlobalItem<Items.wfGlobalItem>().energized = true;
                    }
                }
            }
        }
    }
}