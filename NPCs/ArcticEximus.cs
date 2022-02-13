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
            npc.alpha = 90;
            for (int i = 0; i < npc.buffImmune.Length; i++)
            {
                npc.buffImmune[i] = true;
            }
        }
        public override void AI()
        {
            if (parentNPC is null || parentNPC.life <= 0)
            {
                npc.life = 0;
                return;
            }
            npc.Center = parentNPC.Center;

            for (int i = 0; i < 2; i++)
            {
                var dust = Dust.NewDustPerfect(npc.Center + new Vector2(Main.rand.NextFloat(-1, 1), Main.rand.NextFloat(-1, 1)) * npc.width / 2, 51, Scale: 0.75f);
                dust.velocity *= 0.1f;
            }
        }
        public override void OnHitByProjectile(Projectile projectile, int damage, float knockback, bool crit)
        {
            if (damage < npc.life && projectile.penetrate != -1)
                projectile.penetrate--;
        }
        public override bool CanHitPlayer(Player player, ref int cooldownSlot)
        {
            return false;
        }
    }
}