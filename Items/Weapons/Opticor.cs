using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;

namespace wfMod.Items.Weapons
{
    public class Opticor : wdfeerWeapon
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Charges to shoot a devastating beam\nFire rate cannot be increased\n+25% Critical Damage");
        }
        public override void SetDefaults()
        {
            item.damage = 720; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
            item.crit = 16;
            item.magic = true; // sets the damage type to ranged
            item.mana = 77;
            item.width = 48; // hitbox width of the item
            item.height = 16; // hitbox height of the item
            item.useTime = 120; // The item's use time in ticks (60 ticks == 1 second.)
            item.useAnimation = 120; // The length of the item's use animation in ticks (60 ticks == 1 second.)
            item.useStyle = ItemUseStyleID.HoldingOut; // how you use the item (swinging, holding out, etc)
            item.noMelee = true; //so the item's animation doesn't do damage
            item.knockBack = 20; // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
            item.value = Item.buyPrice(gold: 10); // how much the item sells for (measured in copper)
            item.rare = 4; // the color that the item's name will be in-game
            item.autoReuse = false; // if you can hold click to automatically use it again
            //item.UseSound = mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/OpticorSound").WithVolume(1.2f);
            item.shoot = mod.ProjectileType("OpticorProj");
            item.shootSpeed = 16f; // the speed of the projectile (measured in pixels per frame)
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.SoulofSight, 7);
            recipe.AddIngredient(ItemID.TitaniumBar, 11);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.SoulofSight, 7);
            recipe.AddIngredient(ItemID.AdamantiteBar, 11);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        Microsoft.Xna.Framework.Audio.SoundEffectInstance sound;
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            sound = mod.GetSound("Sounds/OpticorSound").CreateInstance();
            Main.PlaySoundInstance(sound);

            Vector2 velocity = new Vector2(speedX, speedY);
            var proj = ShootWith(position, speedX, speedY, type, damage, knockBack, offset: item.width);
            wdfeerPlayer modPl = Main.player[item.owner].GetModPlayer<wdfeerPlayer>();
            if (modPl.fireRateMult > 1)
                proj.timeLeft = (int)(146 + 54 / modPl.fireRateMult);
            var globalProj = proj.GetGlobalProjectile<Projectiles.wdfeerGlobalProj>();
            globalProj.critMult = 1.25f;
            globalProj.baseVelocity = velocity;
            globalProj.v2 = proj.position - Main.LocalPlayer.position;

            return false;
        }
    }
}