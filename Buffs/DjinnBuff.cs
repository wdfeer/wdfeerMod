using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace wfMod.Buffs
{
    public class DjinnBuff : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Djinn");
            Description.SetDefault("Djinn will shoot toxic darts at enemies");
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            if (player.ownedProjectileCounts[ModContent.ProjectileType<Projectiles.Minions.Djinn>()] > 0)
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