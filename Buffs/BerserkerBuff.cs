using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace wdfeerMod.Buffs
{
    // Ethereal Flames is an example of a buff that causes constant loss of life.
    // See ExamplePlayer.UpdateBadLifeRegen and ExampleGlobalNPC.UpdateLifeRegen for more information.
    public class BerserkerBuff : ModBuff
    {
        Texture2D[] textures = new Texture2D[4];
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Berserker Buff");
            Description.SetDefault("Gain a bonus to Attack Speed");
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;

            textures[0] = ModContent.GetTexture("wdfeerMod/Buffs/BerserkerBuff");
            textures[1] = ModContent.GetTexture("wdfeerMod/Buffs/BerserkerBuff1");
            textures[2] = ModContent.GetTexture("wdfeerMod/Buffs/BerserkerBuff2");
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.meleeSpeed *= 1 + (0.05f * player.GetModPlayer<wdfeerPlayer>().BerserkerProcs);
            Main.buffTexture[Type] = textures[player.GetModPlayer<wdfeerPlayer>().BerserkerProcs - 1];
        }
    }
}
