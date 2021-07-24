using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace wdfeerMod.Buffs
{
    public class CarrierBuff : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Carrier");
            Description.SetDefault("Will fight and provide a permanent ammo reservation buff for you");
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            if (player.ownedProjectileCounts[ModContent.ProjectileType<Projectiles.Minions.Carrier>()] > 0)
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