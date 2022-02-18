using Terraria;
using Terraria.ModLoader;

namespace wfMod.Buffs
{
    public class EnergyConversionBuff : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Energy Conversion");
            Description.SetDefault("+100% Damage on the next magic cast");
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.magicDamageMult += 1f;
        }
    }
}
