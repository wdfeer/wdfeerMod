using System.ComponentModel;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace wdfeerMod
{
    public class wdfeerConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;

        [Label("Enable minion crits")]
        [Tooltip("Allows summon weapons to benefit from general critical chance bonuses\nDisable if another mod provides a similar effect")]
        [DefaultValue(true)]
        public bool minionCrits;
    }
}