using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace wdfeerMod.Projectiles
{
    internal class RaktaDarkDaggerProj : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.MagicDagger);
            projectile.height = 48;
            projectile.width = 11;
            projectile.scale = 0.8f;
            projectile.alpha = 0;
            projectile.penetrate = 3;
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            target.AddBuff(BuffID.Confused,300);
            int manaAdd = Main.LocalPlayer.statManaMax / (Main.LocalPlayer.HasBuff(BuffID.ManaSickness) ? 30 : 15);
            int manaSpare = Main.LocalPlayer.statMana + manaAdd - Main.LocalPlayer.statManaMax;
            if (manaSpare > 0) damage+=manaSpare * 5/4;
            Main.LocalPlayer.statMana += manaAdd;

            damage = Convert.ToInt32((1-projectile.alpha/255)*damage);
            projectile.alpha += 85;
        }
    }
}