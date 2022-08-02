using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace wfMod.Projectiles
{
    internal class RaktaDarkDaggerProj : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.MagicDagger);
            Projectile.height = 48;
            Projectile.width = 48;
            Projectile.scale = 0.8f;
            Projectile.alpha = 0;
            Projectile.penetrate = 3;
            Projectile.light = 0;
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            target.AddBuff(BuffID.Confused, 300);
            int manaAdd = Main.LocalPlayer.statManaMax * ((255 - Projectile.alpha) / 255) / (Main.LocalPlayer.HasBuff(BuffID.ManaSickness) ? 50 : 20);
            int manaSpare = Main.LocalPlayer.statMana + manaAdd - Main.LocalPlayer.statManaMax;
            if (manaSpare > 0) damage += manaSpare * 5 / 4;
            Main.LocalPlayer.statMana += manaAdd;

            damage = Convert.ToInt32((1 - Projectile.alpha / 255) * damage);
            Projectile.alpha += 85;
        }
    }
}