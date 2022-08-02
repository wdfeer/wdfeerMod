using Terraria;
using Terraria.DataStructures;
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
            Item.damage = 10;
            Item.crit = 6;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 45;
            Item.height = 16;
            Item.useTime = 5;
            Item.useAnimation = 5;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 0;
            Item.value = Item.sellPrice(gold: 2);
            Item.rare = 3;
            Item.UseSound = SoundID.Item11.WithVolume(0.32f).WithPitchVariance(0.75f);
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.FluxRifleProj>();
            Item.shootSpeed = 16f;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-4, 0);
        }
        public override void AddRecipes()
        {
            var recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Minishark);
            recipe.AddIngredient(ItemID.TissueSample, 12);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            var gProj = ShootWith(position, speedX, speedY, type, damage, knockBack, 0.0025f, Item.width).GetGlobalProjectile<Projectiles.wfGlobalProj>();
            gProj.AddProcChance(new ProcChance(Mod.Find<ModBuff>("SlashProc").Type, 60));
            gProj.AddProcChance(new ProcChance(BuffID.Weak, 10));
            return false;
        }
    }
}