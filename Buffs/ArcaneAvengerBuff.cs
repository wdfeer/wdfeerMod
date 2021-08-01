using Terraria;
using Terraria.ModLoader;

namespace wdfeerMod.Buffs
{
    public class ArcaneAvengerBuff : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Arcane Avenger");
            Description.SetDefault("+45% Critical Chance");
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.magicCrit += 45;
            player.meleeCrit += 45;
            player.rangedCrit += 45;
        }
    }
}
