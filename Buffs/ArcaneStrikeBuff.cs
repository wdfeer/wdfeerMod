using Terraria;
using Terraria.ModLoader;

namespace wdfeerMod.Buffs
{
    public class ArcaneStrikeBuff : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Arcane Strike");
            Description.SetDefault("+18% Melee Speed");
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.meleeSpeed += 0.18f; 
        }
    }
}
