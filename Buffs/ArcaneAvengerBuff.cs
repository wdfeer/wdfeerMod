using Terraria;
using Terraria.ModLoader;

namespace wfMod.Buffs
{
    public class ArcaneAvengerBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Arcane Avenger");
            Description.SetDefault("+45% Critical Chance");
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetCritChance(DamageClass.Magic) += 45;
            player.GetCritChance(DamageClass.Generic) += 45;
            player.GetCritChance(DamageClass.Ranged) += 45;
        }
    }
}
