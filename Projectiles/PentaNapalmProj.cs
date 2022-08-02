using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace wfMod.Projectiles
{
    internal class PentaNapalmProj : ModProjectile
    {
        public override string Texture => "wfMod/EmptyTexture";
        wfGlobalProj globalProj;
        public override void SetDefaults()
        {
            globalProj = Projectile.GetGlobalProjectile<wfGlobalProj>();
            Projectile.friendly = true;
            Projectile.hide = true;
            Projectile.height = 140;
            Projectile.width = 140;
            Projectile.timeLeft = 300;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 24;
        }
        float rotationSpeed = Main.rand.NextFloat(-1, 1);
        public override void AI()
        {
            for (int i = 0; i < 6; i++)
            {
                var dust = Main.dust[Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 6, Scale: Main.rand.NextFloat(1, 1.2f))];
                dust.velocity *= 1.4f;
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            return false;
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (target.type == NPCID.EaterofWorldsHead && !Main.hardMode)
                damage /= 2;
            if (Main.rand.Next(100) < Main.player[Projectile.owner].GetCritChance(DamageClass.Ranged))
                crit = true;
        }
    }
}