using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace wfMod.Items.Weapons
{
    public class BoarPrime : wfWeapon
    {
        public override void SetDefaults()
        {
            pathToSound = "Sounds/BoarPrimeSound";
            Item.damage = 11;
            Item.crit = 11;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 40;
            Item.height = 17;
            Item.scale = 1.2f;
            Item.useTime = 13;
            Item.useAnimation = 13;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 3;
            Item.value = 1500;
            Item.rare = 5;
            Item.autoReuse = true;
            Item.shoot = 10;
            Item.shootSpeed = 20f;
            Item.useAmmo = AmmoID.Bullet; // The "ammo Id" of the ammo item that this weapon uses. Note that this is not an item Id, but just a magic value.
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.SoulofLight, 14);
            recipe.AddIngredient(Mod.Find<ModItem>("Boar").Type, 1);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            PlaySound(Main.rand.NextFloat(-0.1f, 0.1f), 0.64f);
            for (int i = 0; i < 5; i++)
            {
                var proj = ShootWith(position, speedX, speedY, type, damage, knockBack, 0.11f, Item.width);
            }
            return false;
        }
    }
}