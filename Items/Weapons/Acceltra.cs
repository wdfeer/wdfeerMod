using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using wfMod.Projectiles;

namespace wfMod.Items.Weapons
{
    internal class Acceltra : wfWeapon
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Rapidly launches rockets that explode on impact after traveling a safe distance\nConsumes bullets as ammo, converting them into rockets\n+40% Critical damage");
        }
        public override void SetDefaults()
        {
            pathToSound = "Sounds/AcceltraSound";
            item.damage = 18;
            item.crit = 28;
            item.knockBack = 6;
            item.ranged = true;
            item.noMelee = true;
            item.width = 58;
            item.height = 17;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.useTime = 5;
            item.useAnimation = 5;
            item.autoReuse = true;
            item.rare = 5;
            item.value = Item.buyPrice(gold: 6);
            item.shoot = mod.ProjectileType("AcceltraProj");
            item.shootSpeed = 16f;
            item.useAmmo = AmmoID.Bullet;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(2,4);
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            PlaySound(Main.rand.NextFloat(-0.15f, 0.15f), 0.85f);

            var proj = ShootWith(position, speedX, speedY, mod.ProjectileType("AcceltraProj"), damage, knockBack, spreadMult: 0.02f, offset: item.width);
            var gProj = proj.GetGlobalProjectile<Projectiles.wfGlobalProj>();
            gProj.critMult = 1.4f;
            AcceltraProj modProj = proj.modProjectile as AcceltraProj;
            modProj.initialPos = proj.position;
            return false;
        }
    }
}