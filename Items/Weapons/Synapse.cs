using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;

namespace wfMod.Items.Weapons
{
    public class Synapse : wfWeapon
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("13% ichor chance\n +35% Critical Damage");
        }
        public override void SetDefaults()
        {
            Item.damage = 18;
            Item.crit = 31;
            Item.DamageType = DamageClass.Magic;
            Item.mana = 3;
            Item.width = 48;
            Item.height = 11;
            Item.useTime = 5;
            Item.useAnimation = 5;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 0;
            Item.value = Item.buyPrice(gold: 3);
            Item.rare = 4;
            Item.UseSound = SoundID.Item91.WithPitchVariance(-0.2f).WithVolume(0.6f);
            Item.autoReuse = true;
            Item.shoot = Mod.Find<ModProjectile>("NukorProj").Type;
            Item.shootSpeed = 16f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.SoulofNight, 4);
            recipe.AddIngredient(ItemID.Ichor, 8);
            recipe.AddIngredient(ItemID.MythrilBar, 8);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.SoulofNight, 4);
            recipe.AddIngredient(ItemID.Ichor, 8);
            recipe.AddIngredient(ItemID.OrichalcumBar, 8);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            var proj = ShootWith(position, speedX, speedY, type, damage, knockBack, offset: Item.width);
            proj.timeLeft = 60;
            var globalProj = proj.GetGlobalProjectile<Projectiles.wfGlobalProj>();
            globalProj.AddProcChance(new ProcChance(BuffID.Ichor, 13));

            return false;
        }
    }
}