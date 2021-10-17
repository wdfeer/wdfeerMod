using System.ComponentModel;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace wfMod
{
    public class wfConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;

        [Label("Enable minion crits")]
        [Tooltip("Allows summon weapons to benefit from general critical chance bonuses\nDisable if another mod provides a similar effect")]
        [DefaultValue(true)]
        public bool minionCrits;

        [Label("Spawn eximus enemies")]
        [Tooltip("Allow enemies with increased stats and special abilities to randomly spawn\nTheir abilities are disabled while a boss is alive")]
        [DefaultValue(true)]
        public bool eximusSpawn;
    }
}