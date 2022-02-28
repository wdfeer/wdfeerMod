using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;

namespace wfMod.Items.Weapons
{
    public class FluxRifle : wfWeapon
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Doesn't consume ammo\n60% Slash chance\n10% Chance to inflict Weak");
        }
        public override void SetDefaults()
        {
            item.damage = 10;
            item.crit = 6;
            item.ranged = true;
            item.width = 45;
            item.height = 16;
            item.useTime = 5;
            item.useAnimation = 5;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.noMelee = true;
            item.knockBack = 0;
            item.value = Item.sellPrice(gold: 2);
            item.rare = 3;
            item.UseSound = SoundID.Item11.WithVolume(0.32f).WithPitchVariance(0.75f);
            item.autoReuse = true;
            item.shoot = ModContent.ProjectileType<Projectiles.FluxRifleProj>();
            item.shootSpeed = 16f;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-4, 0);
        }
        public override void AddRecipes()
        {
            var recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Minishark);
            recipe.AddIngredient(ItemID.TissueSample, 12);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            var gProj = ShootWith(position, speedX, speedY, type, damage, knockBack, 0.0025f, item.width).GetGlobalProjectile<Projectiles.wfGlobalProj>();
            gProj.AddProcChance(new ProcChance(mod.BuffType("SlashProc"), 60));
            gProj.AddProcChance(new ProcChance(BuffID.Weak, 10));
            return false;
        }
    }
}