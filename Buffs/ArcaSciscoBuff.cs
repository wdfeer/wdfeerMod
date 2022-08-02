using Terraria;
using Terraria.ModLoader;

namespace wfMod.Buffs
{
    public class ArcaSciscoBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Arca Scisco");
            Description.SetDefault("+5% Crit and Slash chance on the Arca Scisco");
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
        }

        public override void ModifyBuffTip(ref string tip, ref int rare)
        {
            var stacks = Main.LocalPlayer.GetModPlayer<wfPlayer>().arcaSciscoStacks;
            tip = $"+{5 * stacks}% Crit and Slash chance on the Arca Scisco";
        }
    }
}
