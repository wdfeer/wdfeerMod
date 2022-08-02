using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;

namespace wfMod.Items.Weapons
{
    public class RaktaBallistica : wfWeapon
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Shoots 4 arrows at once\nNot shooting charges the next shot, increasing damage and accuraccy\n10% Chance to inflict Weak");
        }
        public override void SetDefaults()
        {
            Item.damage = 4;
            Item.crit = 16;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 30;
            Item.height = 32;
            Item.useTime = 21;
            Item.useAnimation = 21;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 1;
            Item.value = Item.sellPrice(silver: 42);
            Item.rare = 2;
            Item.UseSound = SoundID.Item5;
            Item.autoReuse = false;
            Item.shoot = ProjectileID.WoodenArrowFriendly;
            Item.shootSpeed = 16f;
            Item.useAmmo = AmmoID.Arrow; // The "ammo Id" of the ammo item that this weapon uses. Note that this is not an item Id, but just a magic value.
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(Mod.Find<ModItem>("Ballistica").Type);
            recipe.AddIngredient(ItemID.TissueSample, 17);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
        float lastShotTime = 0;
        float timeSinceLastShot = 60;
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            timeSinceLastShot = player.GetModPlayer<wfPlayer>().longTimer - lastShotTime;
            lastShotTime = player.GetModPlayer<wfPlayer>().longTimer;
            float chargeMult = timeSinceLastShot / Item.useTime;
            if (chargeMult < 1)
                chargeMult = 1;
            else if (chargeMult > 2)
                chargeMult = 2;

            for (int i = 0; i < 4; i++)
            {
                var proj = ShootWith(position, speedX, speedY, type, (int)(damage * chargeMult), knockBack, 0.069f / chargeMult, Item.width);
                proj.localNPCHitCooldown = -1;
                proj.usesLocalNPCImmunity = true;
                var gProj = proj.GetGlobalProjectile<Projectiles.wfGlobalProj>();
                gProj.AddProcChance(new ProcChance(BuffID.Weak, 10));
                gProj.critMult = 0.75f;
            }
            return false;
        }
    }
}