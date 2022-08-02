using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;

namespace wfMod.Items.Weapons
{
    public class Quanta : wfWeapon
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Fires two beams that have a 24% chance to inflict Electricity debuffs\nRight Click to launch a static projectile with 7x damage that explodes on contact\nExplosions can chain and be triggered manually with Primary Fire\n+10% Critical Damage");
        }
        public override void SetDefaults()
        {
            Item.damage = 7;
            Item.crit = 12;
            Item.DamageType = DamageClass.Magic;
            Item.width = 35;
            Item.height = 48;
            Item.useTime = 5;
            Item.useAnimation = 5;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 0;
            Item.value = Item.buyPrice(gold: 1);
            Item.rare = 3;
            Item.autoReuse = true;
            Item.shoot = ProjectileID.MagnetSphereBolt;
            Item.shootSpeed = 16f;
            Item.mana = 3;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                Item.mana = 24;
                Item.useTime = 40;
                Item.useAnimation = 40;
            }
            else
            {
                SetDefaults();
            }
            return base.CanUseItem(player);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.SpaceGun, 2);
            recipe.AddIngredient(ItemID.ShadowScale, 8);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.SpaceGun, 2);
            recipe.AddIngredient(ItemID.TissueSample, 8);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.altFunctionUse != 2)
                for (int i = -1; i <= 1; i += 2)
                {
                    Vector2 offset = Vector2.Normalize(new Vector2(speedY, -speedX)) * 20 * i + Vector2.Normalize(new Vector2(speedX, speedY)) * 52;
                    Vector2 pos = position + offset;
                    Vector2 velocity = Vector2.Normalize(Main.MouseWorld - pos) * Item.shootSpeed;
                    var proj = ShootWith(pos, velocity.X, velocity.Y, Mod.Find<ModProjectile>("QuantaProj").Type, damage, knockBack);
                    var globalProj = proj.GetGlobalProjectile<Projectiles.wfGlobalProj>();
                    globalProj.critMult = 1.1f;
                    globalProj.AddProcChance(new ProcChance(BuffID.Electrified, 24));

                    SoundEngine.PlaySound(SoundID.Item91.WithPitchVariance(0.3f).WithVolume(0.33f), pos);
                }
            else
            {
                var proj = ShootWith(position, speedX, speedY, Mod.Find<ModProjectile>("QuantaAltProj").Type, damage * 7, knockBack, offset: 52, sound: SoundID.Item92);
                var globalProj = proj.GetGlobalProjectile<Projectiles.wfGlobalProj>();
                globalProj.critMult = 1.1f;
                globalProj.AddProcChance(new ProcChance(BuffID.Electrified, 24));
            }

            return false;
        }
    }
}