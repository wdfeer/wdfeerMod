using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace wdfeerMod.Projectiles
{

    internal class NukorProj : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 8;
			projectile.height = 8;
            projectile.magic=true;
			projectile.friendly = true;
			projectile.extraUpdates = 100;
			projectile.timeLeft = 44;
            projectile.hide = true;
        }
        public override void AI()
        {
            for (int num = 0; num < 4; num++)
				{
					Vector2 position2 = projectile.position;
					position2 -= projectile.velocity * ((float)num * 0.25f);
					int num353 = Dust.NewDust(position2, 1, 1, 162);
					Main.dust[num353].position = position2;
					Main.dust[num353].position.X += projectile.width / 2;
					Main.dust[num353].position.Y += projectile.height / 2;
					Main.dust[num353].scale = (float)Main.rand.Next(70, 110) * 0.013f;
					Dust dust = Main.dust[num353];
					dust.velocity *= 0.2f;
				}
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (Main.rand.Next(0,100)<=29) target.AddBuff(BuffID.Confused,100);
        }
    }
}