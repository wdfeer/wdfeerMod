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
            item.damage = 733;
            item.crit = 18;
            item.ranged = true;
            item.width = 48;
            item.height = 16;
            item.useTime = 64;
            item.useAnimation = 64;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.noMelee = true;
            item.knockBack = 7;
            item.value = 180000;
            item.rare = 10;
            item.autoReuse = false;
            item.shoot = 1;
            item.shootSpeed = 16f;
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