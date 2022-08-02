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
            Tooltip.SetDefault("Generate a stasis field around your pet, slowing enemy projectiles within by 50% and frienly ones by 25%,\nIncreasing ally projectiles' damage depending on your minion damage multiplier");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = 4;
            Item.value = Item.buyPrice(gold: 4);
        }
        const float fieldRadius = 450f;
        const float friendlyVelocityMult = 0.75f;
        const float hostileVelocityMult = 0.5f;
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
                if (gProj.stasisFieldApplied || Main.projHook[proj.type] || Main.projPet[proj.type])
                    continue;
                proj.velocity *= proj.friendly ? friendlyVelocityMult : hostileVelocityMult;
                if (proj.friendly)
                {
                    proj.damage = (int)(proj.damage * (1 + (player.GetDamage(DamageClass.Summon) * player.GetDamage(DamageClass.Summon) - 1) * 0.5f));
                }
                gProj.stasisFieldApplied = true;
            }
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            Projectile[] minions = Main.projectile.Where(p => p.active && p.owner == player.whoAmI && Main.projPet[p.type] && p.minionSlots == 0).ToArray();
            if (minions.Length > 0)
                UpdateField(minions[0].Center, player);
        }
    }
}