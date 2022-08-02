using Terraria;
using Terraria.ModLoader;

namespace wfMod.Buffs
{
    public class ArcanePulseBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Arcane Pulse Cooldown");
            Description.SetDefault("Cannot trigger Arcane Pulse");
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            Main.debuff[Type] = true;
        }
    }
}
