using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace wdfeerMod.Items
{
    public class wdfeerGlobalItem : GlobalItem
    {
        public override bool InstancePerEntity => true;
        public override void SetDefaults(Item item)
        {
            if (item.type == ItemID.Grenade) item.ammo = item.type;            
        }
        public override float UseTimeMultiplier(Item item, Player player)
        {
            if (!item.melee && player.HasBuff(mod.BuffType("ArcaneAccelerationBuff")))
            {
                return 1.15f;
            }
                
            return base.UseTimeMultiplier(item, player);
        }
    }
}