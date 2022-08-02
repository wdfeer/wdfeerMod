using Terraria;
using Terraria.ModLoader;

namespace wfMod.Buffs
{
    public class EnergyConversionBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Energy Conversion");
            Description.SetDefault("+100% Damage on the next magic cast");
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetDamage(DamageClass.Magic) += 1f;
        }
    }
}
