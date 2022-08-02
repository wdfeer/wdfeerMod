using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace wfMod.Buffs
{
    public class CarrierPrimeBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Carrier Prime");
            Description.SetDefault("Will fight and provide a permanent ammo reservation buff for you");
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            if (player.ownedProjectileCounts[ModContent.ProjectileType<Projectiles.Minions.CarrierPrime>()] > 0)
            {
                player.buffTime[buffIndex] = 18000;
                player.AddBuff(BuffID.AmmoReservation, 1);
            }
            else
            {
                player.DelBuff(buffIndex);
                buffIndex--;
            }
        }
    }
}