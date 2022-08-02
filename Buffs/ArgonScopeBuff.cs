using Terraria;
using Terraria.ModLoader;

namespace wfMod.Buffs
{
    public class ArgonScopeBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Argon Scope");
            Description.SetDefault("+15% Critical Chance");
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetCritChance(DamageClass.Generic) += 15;
            player.GetCritChance(DamageClass.Magic) += 15;
            player.GetCritChance(DamageClass.Ranged) += 15;
        }
    }
}
