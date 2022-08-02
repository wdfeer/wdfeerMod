using Terraria;
using Terraria.ModLoader;

namespace wfMod.Buffs
{
    public class ArcaneStrikeBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Arcane Strike");
            Description.SetDefault("+18% Melee Speed");
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetAttackSpeed(DamageClass.Melee) += 0.18f; 
        }
    }
}
