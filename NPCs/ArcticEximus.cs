using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace wfMod.NPCs
{
    public class ArcticEximus : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Arctic Eximus");
        }
        public NPC parentNPC;
        public override void SetDefaults()
        {
            if (npc.lifeMax == 0)
            {
                npc.lifeMax = 100;
                npc.life = 100;
            }
            npc.damage = 0;
            npc.knockBackResist = 0f;
            npc.width = 240;
            npc.height = 240;
            npc.noGravity = true;
			npc.noTileCollide = true;
            npc.HitSound = SoundID.Item50;
            npc.value = 0;
			npc.alpha = 100;
        }
        public override void AI()
        {
			if (parentNPC != null && parentNPC.life <= 0) npc.life = 0;
            npc.Center = parentNPC != null ? parentNPC.Center : Main.LocalPlayer.Center;
			for (int i = 0; i < 2; i++)
			{
				var dust = Dust.NewDustPerfect(npc.Center + new Vector2(Main.rand.NextFloat(-1, 1), Main.rand.NextFloat(-1, 1)) * npc.width / 2, 51, Scale: 0.75f);
				dust.velocity *= 0.1f;
			}
        }
        public override bool CanHitPlayer(Player player, ref int cooldownSlot)
        {
            return false;
        }
    }
}