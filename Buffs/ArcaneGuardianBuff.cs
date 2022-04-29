using Terraria;
using Terraria.ModLoader;

namespace wfMod.Buffs
{
    public class ArcaneGuardianBuff : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Arcane Guardian");
            Description.SetDefault("+11 Defense");
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.statDefense += 11;
        }
    }
}
