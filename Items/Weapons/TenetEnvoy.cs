using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;

namespace wfMod.Items.Weapons
{
    public class TenetEnvoy : wfWeapon
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Launches manually-controlled rockets that have a 20% chance to slow enemies\n+30% Critical Damage");
        }
        public override void SetDefaults()
        {
            item.damage = 733; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
            item.crit = 18;
            item.ranged = true; // sets the damage type to ranged
            item.width = 48; // hitbox width of the item
            item.height = 16; // hitbox height of the item
            item.useTime = 64; // The item's use time in ticks (60 ticks == 1 second.)
            item.useAnimation = 64; // The length of the item's use animation in ticks (60 ticks == 1 second.)
            item.useStyle = ItemUseStyleID.HoldingOut; // how you use the item (swinging, holding out, etc)
            item.noMelee = true; //so the item's animation doesn't do damage
            item.knockBack = 7; // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
            item.value = 180000; // how much the item sells for (measured in copper)
            item.rare = 10; // the color that the item's name will be in-game
            item.autoReuse = false; // if you can hold click to automatically use it again
            item.shoot = 1;
            item.shootSpeed = 16f; // the speed of the projectile (measured in pixels per frame)
            item.useAmmo = AmmoID.Rocket;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("Fieldron"), 2);
            recipe.AddIngredient(ItemID.FragmentVortex, 11);
            recipe.AddTile(412);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        Microsoft.Xna.Framework.Audio.SoundEffectInstance sound;
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Main.PlaySound(SoundID.Item61.WithVolume(0.4f));
            sound = mod.GetSound("Sounds/FulminSound").CreateInstance();
            sound.Volume = 0.3f;
            sound.Pitch += Main.rand.NextFloat(0,0.1f);
            Main.PlaySoundInstance(sound);

            var projectile = ShootWith(position, speedX, speedY, mod.ProjectileType("TenetEnvoyProj"), damage, knockBack, offset: item.width);
            var gProj = projectile.GetGlobalProjectile<Projectiles.wfGlobalProj>();
            gProj.AddProcChance(new ProcChance(BuffID.Slow, 20));
            gProj.critMult = 1.3f;
            float rotation = Convert.ToSingle(-Math.Atan2(speedX, speedY));
            projectile.rotation = rotation;

            return false;
        }
    }
}