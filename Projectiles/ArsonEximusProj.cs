using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace wfMod.Projectiles
{
    public class ArsonEximusProj : ModProjectile
    {
        public override string Texture => "wfMod/EmptyTexture";
        List<Player> hitPlayers = new List<Player>();
        public override void SetDefaults()
        {
            Projectile.Name = "Arson Eximus";
            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.width = 32;
            Projectile.height = 32;
            Projectile.hide = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 180;
        }
        int sizeIncrease => 5;
        public override void AI()
        {
            Vector2 oldCenter = Projectile.Center;
            Projectile.width += sizeIncrease;
            Projectile.height += sizeIncrease;
            Projectile.Center = oldCenter;
            for (float i = 0; i < Projectile.width / 17; i++)
            {
                Vector2 pos = Projectile.Center + Vector2.Normalize(new Vector2(Main.rand.NextFloat(-1, 1), Main.rand.NextFloat(-1, 1))) * Projectile.width / 2;
                Dust dust = Dust.NewDustPerfect(pos, 6);
                dust.scale = 1.2f;
            }
        }
        public override bool CanHitPlayer(Player target)
        {
            if (hitPlayers.Contains(target)) return false;
            if ((target.Center - Projectile.Center).Length() <= Projectile.width / 2)
                return base.CanHitPlayer(target);
            return false;
        }
        public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
        {
            hitPlayers.Add(target);
        }
    }
}