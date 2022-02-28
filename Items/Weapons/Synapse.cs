using Terraria;
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
            item.damage = 18;
            item.crit = 31;
            item.magic = true;
            item.mana = 3;
            item.width = 48;
            item.height = 11;
            item.useTime = 5;
            item.useAnimation = 5;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.noMelee = true;
            item.knockBack = 0;
            item.value = Item.buyPrice(gold: 3);
            item.rare = 4;
            item.UseSound = SoundID.Item91.WithPitchVariance(-0.2f).WithVolume(0.6f);
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("NukorProj");
            item.shootSpeed = 16f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.SoulofNight, 4);
            recipe.AddIngredient(ItemID.Ichor, 8);
            recipe.AddIngredient(ItemID.MythrilBar, 8);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.SoulofNight, 4);
            recipe.AddIngredient(ItemID.Ichor, 8);
            recipe.AddIngredient(ItemID.OrichalcumBar, 8);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            var proj = ShootWith(position, speedX, speedY, type, damage, knockBack, offset: item.width);
            proj.timeLeft = 60;
            var globalProj = proj.GetGlobalProjectile<Projectiles.wfGlobalProj>();
            globalProj.AddProcChance(new ProcChance(BuffID.Ichor, 13));

            return false;
        }
    }
}