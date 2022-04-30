using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;

namespace wfMod.Items.Weapons
{
    public class Vectis : wfWeapon
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Shots penetrate an enemy");
        }
        public override void SetDefaults()
        {
            item.damage = 88;
            item.crit = 21;
            item.ranged = true;
            item.width = 63;
            item.height = 17;
            item.useTime = 60;
            item.useAnimation = 60;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.noMelee = true;
            item.knockBack = 6;
            item.value = 15000;
            item.rare = 4;
            item.UseSound = SoundID.Item40;
            item.autoReuse = false;
            item.shootSpeed = 48f;
            item.shoot = 10;
            item.useAmmo = AmmoID.Bullet; // The "ammo Id" of the ammo item that this weapon uses. Note that this is not an item Id, but just a magic value.
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.MeteoriteBar, 12);
            recipe.AddIngredient(ItemID.Wire, 8);
            recipe.AddIngredient(ItemID.Musket, 1);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.MeteoriteBar, 12);
            recipe.AddIngredient(ItemID.Wire, 8);
            recipe.AddIngredient(ItemID.TheUndertaker, 1);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            sound = mod.GetSound("Sounds/VectisPrimeSound2").CreateInstance();
            sound.Volume = 0.55f;
            sound.Pitch += Main.rand.NextFloat(0f, 0.2f);
            sound.Play();

            var proj = ShootWith(position, speedX, speedY, type, damage, knockBack, offset: item.width);
            proj.extraUpdates = 1;
            proj.penetrate = 2;
            proj.usesLocalNPCImmunity = true;
            proj.localNPCHitCooldown = -1;
            return false;
        }
    }
}