using Terraria;
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
            item.damage = 11;
            item.crit = 11;
            item.ranged = true;
            item.width = 40;
            item.height = 17;
            item.scale = 1.2f;
            item.useTime = 13;
            item.useAnimation = 13;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.noMelee = true;
            item.knockBack = 3;
            item.value = 1500;
            item.rare = 5;
            item.autoReuse = true;
            item.shoot = 10;
            item.shootSpeed = 20f;
            item.useAmmo = AmmoID.Bullet; // The "ammo Id" of the ammo item that this weapon uses. Note that this is not an item Id, but just a magic value.
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.SoulofLight, 14);
            recipe.AddIngredient(mod.ItemType("Boar"), 1);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            PlaySound(Main.rand.NextFloat(-0.1f, 0.1f), 0.64f);
            for (int i = 0; i < 5; i++)
            {
                var proj = ShootWith(position, speedX, speedY, type, damage, knockBack, 0.11f, item.width);
            }
            return false;
        }
    }
}