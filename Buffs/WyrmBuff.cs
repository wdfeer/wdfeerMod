using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace wfMod.Buffs
{
    public class WyrmBuff : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Wyrm");
            Description.SetDefault("Will periodically release a shockwave with high knockback when an enemy is nearby");
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            if (player.ownedProjectileCounts[ModContent.ProjectileType<Projectiles.Minions.Wyrm>()] > 0)
            {
                player.buffTime[buffIndex] = 18000;
            }
            else
            {
                player.DelBuff(buffIndex);
                buffIndex--;
            }
        }
    }
}