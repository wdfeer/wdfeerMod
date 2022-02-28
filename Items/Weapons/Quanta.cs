using Terraria;
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
            item.damage = 7;
            item.crit = 12;
            item.magic = true;
            item.width = 35;
            item.height = 48;
            item.useTime = 5;
            item.useAnimation = 5;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.noMelee = true;
            item.knockBack = 0;
            item.value = Item.buyPrice(gold: 1);
            item.rare = 3;
            item.autoReuse = true;
            item.shoot = ProjectileID.MagnetSphereBolt;
            item.shootSpeed = 16f;
            item.mana = 3;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                item.mana = 24;
                item.useTime = 40;
                item.useAnimation = 40;
            }
            else
            {
                SetDefaults();
            }
            return base.CanUseItem(player);
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.SpaceGun, 2);
            recipe.AddIngredient(ItemID.ShadowScale, 8);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.SpaceGun, 2);
            recipe.AddIngredient(ItemID.TissueSample, 8);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (player.altFunctionUse != 2)
                for (int i = -1; i <= 1; i += 2)
                {
                    Vector2 offset = Vector2.Normalize(new Vector2(speedY, -speedX)) * 20 * i + Vector2.Normalize(new Vector2(speedX, speedY)) * 52;
                    Vector2 pos = position + offset;
                    Vector2 velocity = Vector2.Normalize(Main.MouseWorld - pos) * item.shootSpeed;
                    var proj = ShootWith(pos, velocity.X, velocity.Y, mod.ProjectileType("QuantaProj"), damage, knockBack);
                    var globalProj = proj.GetGlobalProjectile<Projectiles.wfGlobalProj>();
                    globalProj.critMult = 1.1f;
                    globalProj.AddProcChance(new ProcChance(BuffID.Electrified, 24));

                    Main.PlaySound(SoundID.Item91.WithPitchVariance(0.3f).WithVolume(0.33f), pos);
                }
            else
            {
                var proj = ShootWith(position, speedX, speedY, mod.ProjectileType("QuantaAltProj"), damage * 7, knockBack, offset: 52, sound: SoundID.Item92);
                var globalProj = proj.GetGlobalProjectile<Projectiles.wfGlobalProj>();
                globalProj.critMult = 1.1f;
                globalProj.AddProcChance(new ProcChance(BuffID.Electrified, 24));
            }

            return false;
        }
    }
}