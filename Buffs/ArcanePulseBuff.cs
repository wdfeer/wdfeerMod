using Terraria;
using Terraria.ModLoader;

namespace wdfeerMod.Buffs
{
    public class ArcanePulseBuff : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Arcane Pulse Cooldown");
            Description.SetDefault("Cannot trigger Arcane Pulse");
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            Main.debuff[Type] = true;
        }
    }
}
