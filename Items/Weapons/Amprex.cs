using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;

namespace wfMod.Items.Weapons
{
    public class Amprex : wdfeerWeapon
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Inflicts Electricity debuffs\n+10% Critical Damage\nLimited range, chains between nearby enemies");
        }
        public override void SetDefaults()
        {
            item.damage = 26;
            item.crit = 28;
            item.magic = true;
            item.width = 45;
            item.height = 10;
            item.useTime = 5;
            item.useAnimation = 5;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.noMelee = true;
            item.knockBack = 0;
            item.value = Item.buyPrice(gold: 5);
            item.rare = 7;
            item.UseSound = SoundID.Item93.WithPitchVariance(0f).WithVolume(0.1f);
            item.autoReuse = true;
            item.shoot = ModContent.ProjectileType<Projectiles.AmprexProj>();
            item.shootSpeed = 16f;
            item.mana = 4;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.TitaniumBar,14);
            recipe.AddIngredient(ItemID.Nanites, 16);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.AdamantiteBar,14);
            recipe.AddIngredient(ItemID.Nanites, 16);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            var projectile = ShootWith(position,speedX,speedY,type,damage,knockBack, offset: 64, sound: SoundID.Item91.WithVolume(0.3f));
            var globalProj = projectile.GetGlobalProjectile<Projectiles.wdfeerGlobalProj>();
            globalProj.critMult = 1.1f;
            globalProj.AddProcChance(new ProcChance(BuffID.Electrified, 22));

            return false;
        }
    }
}