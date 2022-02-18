using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;

namespace wfMod.Items.Weapons
{
    public class ArcaPlasmor : wfWeapon
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Shoots a piercing projectile\nMight confuse enemies\n-20% Critical Damage");
        }
        public override void SetDefaults()
        {
            item.damage = 222; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
            item.crit = 18;
            item.magic = true; // sets the damage type to ranged
            item.mana = 8;
            item.width = 48; // hitbox width of the item
            item.height = 16; // hitbox height of the item
            item.useTime = 54; // The item's use time in ticks (60 ticks == 1 second.)
            item.useAnimation = 54; // The length of the item's use animation in ticks (60 ticks == 1 second.)
            item.useStyle = ItemUseStyleID.HoldingOut; // how you use the item (swinging, holding out, etc)
            item.noMelee = true; //so the item's animation doesn't do damage
            item.knockBack = 7; // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
            item.value = 150000; // how much the item sells for (measured in copper)
            item.rare = 4; // the color that the item's name will be in-game
            item.autoReuse = true; // if you can hold click to automatically use it again
            item.shoot = ModContent.ProjectileType<Projectiles.ArcaPlasmorProj>();
            item.shootSpeed = 30f; // the speed of the projectile (measured in pixels per frame)
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.CobaltBar, 22);
            recipe.AddIngredient(ItemID.SoulofLight, 12);

            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.PalladiumBar, 22);
            recipe.AddIngredient(ItemID.SoulofLight, 12);

            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        Microsoft.Xna.Framework.Audio.SoundEffectInstance sound;
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            sound = mod.GetSound("Sounds/ArcaPlasmorSound").CreateInstance();
            sound.Pitch += Main.rand.NextFloat(0, 0.15f);
            Main.PlaySoundInstance(sound);

            var projectile = ShootWith(position, speedX, speedY, type, damage, knockBack, offset: item.width);
            projectile.GetGlobalProjectile<Projectiles.wfGlobalProj>().critMult = 0.8f;
            projectile.GetGlobalProjectile<Projectiles.wfGlobalProj>().AddProcChance(new ProcChance(31, 28));
            float rotation = Convert.ToSingle(-Math.Atan2(speedX, speedY));
            projectile.rotation = rotation;

            return false;
        }
    }
}