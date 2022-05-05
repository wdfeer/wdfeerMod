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
            globalProj = projectile.GetGlobalProjectile<wfGlobalProj>();
            projectile.friendly = true;
            projectile.hide = true;
            projectile.height = 140;
            projectile.width = 140;
            projectile.timeLeft = 300;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 24;
        }
        float rotationSpeed = Main.rand.NextFloat(-1, 1);
        public override void AI()
        {
            for (int i = 0; i < 6; i++)
            {
                var dust = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, 6, Scale: Main.rand.NextFloat(1, 1.2f))];
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
            if (Main.rand.Next(100) < Main.player[projectile.owner].rangedCrit)
                crit = true;
        }
    }
}