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
            Tooltip.SetDefault("Shoots 4 arrows at once with greatly decreased velocity\nNot shooting charges the next shot, increasing damage, accuraccy and velocity\n-25% Critical Damage");
        }
        public override void SetDefaults()
        {
            item.damage = 2; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
            item.crit = 11;
            item.ranged = true; // sets the damage type to ranged
            item.width = 30; // hitbox width of the item
            item.height = 31; // hitbox height of the item
            item.useTime = 18; // The item's use time in ticks (60 ticks == 1 second.)
            item.useAnimation = 18; // The length of the item's use animation in ticks (60 ticks == 1 second.)
            item.useStyle = ItemUseStyleID.HoldingOut; // how you use the item (swinging, holding out, etc)
            item.noMelee = true; //so the item's animation doesn't do damage
            item.knockBack = 1; // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
            item.value = Item.sellPrice(silver: 32); // how much the item sells for (measured in copper)
            item.rare = 1; // the color that the item's name will be in-game
            item.UseSound = SoundID.Item5; // The sound that this item plays when used.
            item.autoReuse = false; // if you can hold click to automatically use it again
            item.shoot = ProjectileID.WoodenArrowFriendly; //idk why but all the guns in the vanilla source have this
            item.shootSpeed = 16f; // the speed of the projectile (measured in pixels per frame)
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