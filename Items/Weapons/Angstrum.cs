using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;

namespace wfMod.Items.Weapons
{
    public class Angstrum : wfWeapon
    {
        Random rand = new Random();
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Shoot a single rocket with LMB\nRight click to fire 3 rockets at once");
        }
        public override void SetDefaults()
        {
            item.damage = 45; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
            item.crit = 12;
            item.ranged = true; // sets the damage type to ranged
            item.width = 27; // hitbox width of the item
            item.height = 14; // hitbox height of the item
            item.scale = 1.3f;
            item.useTime = 30;
            item.useAnimation = 30;
            item.useStyle = ItemUseStyleID.HoldingOut; // how you use the item (swinging, holding out, etc)
            item.noMelee = true; //so the item's animation doesn't do damage
            item.knockBack = 1; // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
            item.value = Item.sellPrice(gold: 1); // how much the item sells for (measured in copper)
            item.rare = 3; // the color that the item's name will be in-game
            item.autoReuse = false; // if you can hold click to automatically use it again
            item.shoot = 10; //idk why but all the guns in the vanilla source have this
            item.shootSpeed = 20f; // the speed of the projectile (measured in pixels per frame)
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-1.4f, 1.4f);
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                item.useTime = 72;
                item.useAnimation = 72;
            }
            else
            {
                item.useTime = 30;
                item.useAnimation = 30;
            }
            return base.CanUseItem(player);
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.PlatinumBar, 3);
            recipe.AddIngredient(ItemID.HellstoneBar, 8);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        Microsoft.Xna.Framework.Audio.SoundEffectInstance sound;
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (player.altFunctionUse == 2)
            {
                sound = mod.GetSound("Sounds/PrismaAngstrumAltSound").CreateInstance();
                sound.Pitch += Main.rand.NextFloat(-0.08f, 0.08f);
                Main.PlaySoundInstance(sound);

                for (int i = 0; i < 3; i++)
                {
                    var proj = ShootWith(position, speedX, speedY, ModContent.ProjectileType<Projectiles.AngstrumProj>(), damage, knockBack, 0.18f, item.width);
                }
            }
            else
            {
                sound = mod.GetSound("Sounds/PrismaAngstrumSound").CreateInstance();
                sound.Pitch += Main.rand.NextFloat(0, 0.1f);
                Main.PlaySoundInstance(sound);

                var proj = ShootWith(position, speedX, speedY, ModContent.ProjectileType<Projectiles.AngstrumProj>(), damage, knockBack, 0.005f, item.width, SoundID.Item72.WithVolume(0.6f));
            }
            return false;
        }
    }
}