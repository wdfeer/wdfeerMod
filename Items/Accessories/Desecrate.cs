using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace wfMod.Items.Accessories
{
    
    public class Desecrate : ExclusiveAccessory
    {
        public const int lifeConsumption = 7;
        public const float maxDistance = 800;
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault($"Whenever an enemy dies nearby, consume {lifeConsumption} life and double the loot");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.rare = ItemRarityID.Expert;
            item.value = Item.sellPrice(gold: lifeConsumption);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            item.rare = ItemRarityID.Expert;
            player.GetModPlayer<wfPlayer>().desecrate = true;
        }
        public static void HurtByDesecration(Player player)
        {
            player.statLife -= lifeConsumption;
            CombatText text = Main.combatText[CombatText.NewText(player.getRect(), Color.Crimson, 7)];
        }
        public static bool CanExtraLoot(Player player, NPC npc)
        {
            if (!player.active || player.dead || !player.GetModPlayer<wfPlayer>().desecrate || player.statLife <= lifeConsumption)
                return false;
            float distance = (player.position - npc.position).Length();
            if (distance > maxDistance)
                return false;

            if (Main.netMode != NetmodeID.SinglePlayer)
                SendDesecrateMessage((byte)player.whoAmI);
            else
                HurtByDesecration(player);
            return true;
        }
        private static void SendDesecrateMessage(byte player)
        {
            var packet = wfMod.mod.GetPacket();
            packet.Write((byte)wfMod.wfMessageType.DesecrateDamage);
            packet.Write(player);
            packet.Send();
        }
    }
}