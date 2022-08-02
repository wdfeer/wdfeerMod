using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
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
            Item.damage = 733;
            Item.crit = 18;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 48;
            Item.height = 16;
            Item.useTime = 64;
            Item.useAnimation = 64;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 7;
            Item.value = 180000;
            Item.rare = 10;
            Item.autoReuse = false;
            Item.shoot = 1;
            Item.shootSpeed = 16f;
            Item.useAmmo = AmmoID.Rocket;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(Mod.Find<ModItem>("Fieldron").Type, 2);
            recipe.AddIngredient(ItemID.FragmentVortex, 11);
            recipe.AddTile(412);
            recipe.Register();
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            SoundEngine.PlaySound(SoundID.Item61.WithVolume(0.4f));
            sound = Mod.GetSound("Sounds/FulminSound").CreateInstance();
            sound.Volume = 0.3f;
            sound.Pitch += Main.rand.NextFloat(0, 0.1f);
            Main.PlaySoundInstance(sound);

            var projectile = ShootWith(position, speedX, speedY, Mod.Find<ModProjectile>("TenetEnvoyProj").Type, damage, knockBack, offset: Item.width);
            var gProj = projectile.GetGlobalProjectile<Projectiles.wfGlobalProj>();
            gProj.AddProcChance(new ProcChance(BuffID.Slow, 20));
            gProj.critMult = 1.3f;
            float rotation = Convert.ToSingle(-Math.Atan2(speedX, speedY));
            projectile.rotation = rotation;

            return false;
        }
    }
}