using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;

namespace wfMod.Items.Weapons
{
    public class Baza : wfWeapon
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("75% Chance not to consume ammo\n+50% Critical Damage");
        }
        public override void SetDefaults()
        {
            Item.damage = 1;
            Item.crit = 22;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 45;
            Item.height = 18;
            Item.useTime = 4;
            Item.useAnimation = 4;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 0;
            Item.value = 1500;
            Item.rare = 3;
            Item.UseSound = SoundID.Item11.WithVolume(0.1f);
            Item.autoReuse = true;
            Item.shoot = 10;
            Item.shootSpeed = 16f;
            Item.useAmmo = AmmoID.Bullet; // The "ammo Id" of the ammo item that this weapon uses. Note that this is not an item Id, but just a magic value.
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-4,0);
        }
        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            if (Main.rand.Next(0,100) <= 75) return false;
            return base.CanConsumeAmmo(player);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(Mod.Find<ModItem>("Furis").Type);
            recipe.AddIngredient(ItemID.ShadowScale,8);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(Mod.Find<ModItem>("Furis").Type);
            recipe.AddIngredient(ItemID.TissueSample,8);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            var proj = ShootWith(position, speedX, speedY, type, damage, knockBack, 0.001f, Item.width);
            proj.GetGlobalProjectile<Projectiles.wfGlobalProj>().critMult = 1.5f;
            return false;
        }
    }
}