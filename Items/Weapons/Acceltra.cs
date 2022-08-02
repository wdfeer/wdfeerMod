using Terraria;
using Terraria.DataStructures;
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
            Item.damage = 18;
            Item.crit = 28;
            Item.knockBack = 6;
            Item.DamageType = DamageClass.Ranged;
            Item.noMelee = true;
            Item.width = 58;
            Item.height = 17;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useTime = 5;
            Item.useAnimation = 5;
            Item.autoReuse = true;
            Item.rare = 5;
            Item.value = Item.buyPrice(gold: 6);
            Item.shoot = Mod.Find<ModProjectile>("AcceltraProj").Type;
            Item.shootSpeed = 16f;
            Item.useAmmo = AmmoID.Bullet;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(2,4);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            PlaySound(Main.rand.NextFloat(-0.15f, 0.15f), 0.85f);

            var proj = ShootWith(position, speedX, speedY, Mod.Find<ModProjectile>("AcceltraProj").Type, damage, knockBack, spreadMult: 0.02f, offset: Item.width);
            var gProj = proj.GetGlobalProjectile<Projectiles.wfGlobalProj>();
            gProj.critMult = 1.4f;
            AcceltraProj modProj = proj.ModProjectile as AcceltraProj;
            modProj.initialPos = proj.position;
            return false;
        }
    }
}