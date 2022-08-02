using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace wfMod.Items.Weapons
{
    internal class Tonkor : wfWeapon
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Launches grenades that explode on impact without self-damage\nDamage is not affected by the used grenade's damage\nDeals halved damage to the Eater of Worlds\n+25% Critical Damage");
        }
        public override void SetDefaults()
        {
            Item.damage = 29;
            Item.crit = 21;
            Item.knockBack = 5.5f;
            Item.DamageType = DamageClass.Ranged;
            Item.noMelee = true;
            Item.width = 37;
            Item.height = 14;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item61;
            Item.useTime = 66;
            Item.useAnimation = 66;
            Item.rare = 2;
            Item.value = Item.buyPrice(gold: 2);
            Item.shoot = Mod.Find<ModProjectile>("TonkorProj").Type;
            Item.shootSpeed = 16f;
            Item.useAmmo = ItemID.Grenade;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            var proj = ShootWith(position, speedX, speedY, Mod.Find<ModProjectile>("TonkorProj").Type, damage, knockBack, offset: Item.width + 2);
            proj.damage = (int)(Item.damage * player.GetDamage(DamageClass.Ranged));
            var gProj = proj.GetGlobalProjectile<Projectiles.wfGlobalProj>();
            gProj.critMult = 1.25f;
            gProj.ai = () =>
            {
                if (proj.velocity.Y < 10) 
                    proj.velocity.Y += 0.1f;
            };
            return false;
        }
    }
}