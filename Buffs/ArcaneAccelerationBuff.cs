using Terraria;
using Terraria.ModLoader;

namespace wdfeerMod.Buffs
{
    public class ArcaneAccelerationBuff : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Arcane Acceleration");
            Description.SetDefault("+15% Fire Rate on non-melee weapons");
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
        }
    }
}
