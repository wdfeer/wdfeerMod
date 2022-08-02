using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;

namespace wfMod.Items.Weapons
{
    public class Ballistica : wfWeapon
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Shoots 4 arrows at once with greatly decreased velocity\nNot shooting charges the next shot, increasing damage, accuraccy and velocity\nDamage is affected only by 50% of the arrows' damage\n-25% Critical Damage");
        }
        public override void SetDefaults()
        {
            Item.damage = 4;
            Item.crit = 11;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 30;
            Item.height = 31;
            Item.useTime = 18;
            Item.useAnimation = 18;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 1;
            Item.value = Item.sellPrice(silver: 32);
            Item.rare = 1;
            Item.UseSound = SoundID.Item5;
            Item.autoReuse = false;
            Item.shoot = ProjectileID.WoodenArrowFriendly;
            Item.shootSpeed = 16f;
            Item.useAmmo = AmmoID.Arrow; // The "ammo Id" of the ammo item that this weapon uses. Note that this is not an item Id, but just a magic value.
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.LeadBow);
            recipe.AddIngredient(ItemID.LeadBar, 14);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.IronBow);
            recipe.AddIngredient(ItemID.IronBar, 12);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
        float lastShotTime = 0;
        float timeSinceLastShot = 60;
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            timeSinceLastShot = player.GetModPlayer<wfPlayer>().longTimer - lastShotTime;
            lastShotTime = player.GetModPlayer<wfPlayer>().longTimer;
            float chargeMult = (timeSinceLastShot / Item.useTime) * 0.6f;
            if (chargeMult < 1)
                chargeMult = 1;
            else if (chargeMult > 2)
                chargeMult = 2;

            speedX *= 0.5f * chargeMult;
            speedY *= 0.5f * chargeMult;

            float ammoDamage = (damage / player.GetDamage(DamageClass.Ranged)) / player.GetDamage(DamageClass.Ranged) - Item.damage;
            damage = (int)((Item.damage + ammoDamage / 2) * player.GetDamage(DamageClass.Ranged) * player.GetDamage(DamageClass.Ranged));
            for (int i = 0; i < 4; i++)
            {
                var proj = ShootWith(position, speedX, speedY, type, (int)(damage * chargeMult), knockBack, 0.09f / chargeMult, Item.width);
                proj.localNPCHitCooldown = -1;
                proj.usesLocalNPCImmunity = true;
                var gProj = proj.GetGlobalProjectile<Projectiles.wfGlobalProj>();
                gProj.critMult = 0.75f;
            }
            return false;
        }
    }
}