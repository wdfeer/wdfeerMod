using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace wdfeerMod.Projectiles
{
    internal class ArsonEximusProj : ModProjectile
    {
        List<Player> hitPlayers = new List<Player>();
        public override void SetDefaults()
        {
            projectile.Name = "Arson Eximus";
            projectile.hostile = true;
            projectile.width = 32;
            projectile.height = 32;
            projectile.hide = true;
            projectile.tileCollide = false;
            projectile.penetrate = -1;
            projectile.npcProj = true;
            projectile.timeLeft = 180;
        }
        int sizeIncrease => 6;
        public override void AI()
        {
            Vector2 oldCenter = projectile.Center;
            projectile.width += sizeIncrease;
            projectile.height += sizeIncrease;
            projectile.Center = oldCenter;
            for (float i = 0; i < projectile.width / 16; i++)
            {
                Vector2 pos = projectile.Center + Vector2.Normalize(new Vector2(Main.rand.NextFloat(-1, 1), Main.rand.NextFloat(-1, 1))) * projectile.width / 2;
                Dust dust = Dust.NewDustPerfect(pos, 6);
                dust.scale = 1.2f;
            }
        }
        public override bool CanHitPlayer(Player target)
        {
            if (hitPlayers.Contains(target)) return false;
            if ((target.Center - projectile.Center).Length() <= projectile.width / 2)
                return base.CanHitPlayer(target);
            return false;
        }
        public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
        {
            hitPlayers.Add(target);
        }
    }
}