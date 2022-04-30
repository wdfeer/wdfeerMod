using Microsoft.Xna.Framework;
using System;
using System.Linq;
using Terraria;
using Terraria.ID;
using wfMod.Projectiles;
using Terraria.ModLoader;

namespace wfMod.Items.Accessories
{
    
    public class StasisField : ExclusiveAccessory
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Generate a stasis field around your last minion, slowing all projectiles within by 69%,\nIncreasing ally projectiles' damage and decreasing enemy projectiles' damage depending on your minion slots");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.rare = 4;
            item.value = Item.buyPrice(gold: 4);
        }
        const float fieldRadius = 450f;
        void UpdateField(Vector2 center, Player player)
        {
            wfMod.NewDustsCircleEdge(12, center, fieldRadius, DustID.Flare_Blue, (d) => {
                d.velocity = Vector2.Zero;
                d.noGravity = true;
            });
            Projectile[] projectilesWithin = Main.projectile.Where(p => (p.Center - center).Length() < fieldRadius).ToArray();
            for (int i = 0; i < projectilesWithin.Length; i++)
            {
                var proj = projectilesWithin[i];
                var gProj = proj.GetGlobalProjectile<wfGlobalProj>();
                if (gProj.stasisFieldApplied)
                    continue;
                proj.velocity *= 0.31f;
                if (proj.friendly)
                {
                    proj.damage = (int)(proj.damage * (1 + player.maxMinions * 0.04f));
                } else if (proj.hostile)
                {
                    proj.damage = (int)(proj.damage / (1 + player.maxMinions * 0.05));
                }
                gProj.stasisFieldApplied = true;
            }
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            Projectile[] minions = Main.projectile.Where(p => p.active && p.owner == player.whoAmI && Main.projPet[p.type]).ToArray();
            if (minions.Length > 0)
                UpdateField(minions[0].Center, player);
        }
    }
}