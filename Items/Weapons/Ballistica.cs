using Terraria;
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
            item.damage = 4;
            item.crit = 11;
            item.ranged = true;
            item.width = 30;
            item.height = 31;
            item.useTime = 18;
            item.useAnimation = 18;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.noMelee = true;
            item.knockBack = 1;
            item.value = Item.sellPrice(silver: 32);
            item.rare = 1;
            item.UseSound = SoundID.Item5;
            item.autoReuse = false;
            item.shoot = ProjectileID.WoodenArrowFriendly;
            item.shootSpeed = 16f;
            item.useAmmo = AmmoID.Arrow; // The "ammo Id" of the ammo item that this weapon uses. Note that this is not an item Id, but just a magic value.
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.LeadBow);
            recipe.AddIngredient(ItemID.LeadBar, 14);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.IronBow);
            recipe.AddIngredient(ItemID.IronBar, 12);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        float lastShotTime = 0;
        float timeSinceLastShot = 60;
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            timeSinceLastShot = player.GetModPlayer<wfPlayer>().longTimer - lastShotTime;
            lastShotTime = player.GetModPlayer<wfPlayer>().longTimer;
            float chargeMult = (timeSinceLastShot / item.useTime) * 0.6f;
            if (chargeMult < 1)
                chargeMult = 1;
            else if (chargeMult > 2)
                chargeMult = 2;

            speedX *= 0.5f * chargeMult;
            speedY *= 0.5f * chargeMult;

            float ammoDamage = (damage / player.rangedDamageMult) / player.rangedDamage - item.damage;
            damage = (int)((item.damage + ammoDamage / 2) * player.rangedDamageMult * player.rangedDamage);
            for (int i = 0; i < 4; i++)
            {
                var proj = ShootWith(position, speedX, speedY, type, (int)(damage * chargeMult), knockBack, 0.09f / chargeMult, item.width);
                proj.localNPCHitCooldown = -1;
                proj.usesLocalNPCImmunity = true;
                var gProj = proj.GetGlobalProjectile<Projectiles.wfGlobalProj>();
                gProj.critMult = 0.75f;
            }
            return false;
        }
    }
}