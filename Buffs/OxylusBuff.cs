using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace wfMod.Buffs
{
    public class OxylusBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Oxylus");
            Description.SetDefault("Oxylus electrifies your foes for you! +33% Increased fishing power");
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            if (player.ownedProjectileCounts[ModContent.ProjectileType<Projectiles.Minions.Oxylus>()] > 0)
            {
                player.buffTime[buffIndex] = 18000;
                player.fishingSkill += 33;
            }
            else
            {
                player.DelBuff(buffIndex);
                buffIndex--;
            }
        }
    }
}