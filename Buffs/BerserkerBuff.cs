using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace wfMod.Buffs
{
    // Ethereal Flames is an example of a buff that causes constant loss of life.
    // See ExamplePlayer.UpdateBadLifeRegen and ExampleGlobalNPC.UpdateLifeRegen for more information.
    public class BerserkerBuff : ModBuff
    {
        Texture2D[] textures = new Texture2D[4];
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Berserker");
            Description.SetDefault("+7% Melee Speed");
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;

            textures[0] = ModContent.GetTexture("wfMod/Buffs/BerserkerBuff");
            textures[1] = ModContent.GetTexture("wfMod/Buffs/BerserkerBuff1");
            textures[2] = ModContent.GetTexture("wfMod/Buffs/BerserkerBuff2");
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetAttackSpeed(DamageClass.Melee) += (0.07f * player.GetModPlayer<wfPlayer>().BerserkerProcs);
            TextureAssets.Buff[Type].Value = textures[player.GetModPlayer<wfPlayer>().BerserkerProcs - 1];
        }

        public override void ModifyBuffTip(ref string tip, ref int rare)
        {
            tip = $"+{Main.LocalPlayer.GetModPlayer<wfPlayer>().BerserkerProcs * 7}% Melee Speed";
        }
    }
}
