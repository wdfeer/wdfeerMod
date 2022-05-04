using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;
using wfMod.Projectiles;

namespace wfMod.Items.Weapons
{
    public abstract class BaseOpticor : wfWeapon
    {
        protected abstract float critDmg { get; }
        protected abstract string soundPath { get; }
        protected abstract int getBaseProjTimeLeft(); 
        public override bool UseItemFrame(Player player)
        {
            return true;
        }
        static Vector2 MousePlayerDiff(Player player)
        {
            return Main.MouseWorld - player.Center;
        }
        static Vector2 MousePlayerDiffNormalized(Player player)
        {
            var diff = MousePlayerDiff(player);
            diff.Normalize();
            return diff;
        }
        bool shootRight = true;
        public override void UseStyle(Player player)
        {
            Vector2 diff = MousePlayerDiff(player);
            
            player.itemRotation = (float)(Math.Atan2(diff.Y, diff.X) + (shootRight ? 0 : Math.PI));
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            sound = mod.GetSound(soundPath).CreateInstance();
            Main.PlaySoundInstance(sound);

            if (speedX < 0)
                shootRight = false;
            else if (speedX > 0)
                shootRight = true;

            Vector2 velocity = new Vector2(speedX, speedY);
            var proj = ShootWith(position, speedX, speedY, type, damage, knockBack, offset: item.width);
            wfPlayer modPl = player.GetModPlayer<wfPlayer>();
            proj.timeLeft = getBaseProjTimeLeft();
            var globalProj = proj.GetGlobalProjectile<Projectiles.wfGlobalProj>();
            globalProj.critMult = critDmg;
            OpticorProj modProj = proj.modProjectile as OpticorProj;
            modProj.owner = player;
            modProj.getPositionNearThePlayer = () => {
                var diff = MousePlayerDiffNormalized(player);
                return diff * item.width;
            };
            modProj.getBaseVelocity = () =>
            {
                var diff = MousePlayerDiffNormalized(player);
                return velocity.Length() * diff;
            };

            return false;
        }
    }
}