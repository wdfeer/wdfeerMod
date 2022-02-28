using Terraria;
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
            item.damage = 4;
            item.crit = 16;
            item.ranged = true;
            item.width = 30;
            item.height = 32;
            item.useTime = 21;
            item.useAnimation = 21;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.noMelee = true;
            item.knockBack = 1;
            item.value = Item.sellPrice(silver: 42);
            item.rare = 2;
            item.UseSound = SoundID.Item5;
            item.autoReuse = false;
            item.shoot = ProjectileID.WoodenArrowFriendly;
            item.shootSpeed = 16f;
            item.useAmmo = AmmoID.Arrow; // The "ammo Id" of the ammo item that this weapon uses. Note that this is not an item Id, but just a magic value.
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("Ballistica"));
            recipe.AddIngredient(ItemID.TissueSample, 17);
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
            float chargeMult = timeSinceLastShot / item.useTime;
            if (chargeMult < 1)
                chargeMult = 1;
            else if (chargeMult > 2)
                chargeMult = 2;

            for (int i = 0; i < 4; i++)
            {
                var proj = ShootWith(position, speedX, speedY, type, (int)(damage * chargeMult), knockBack, 0.069f / chargeMult, item.width);
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