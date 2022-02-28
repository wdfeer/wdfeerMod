using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;

namespace wfMod.Items.Weapons
{
    public class Scourge : wfWeapon
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Fires explosive projectiles");
        }
        public override void SetDefaults()
        {
            item.damage = 24;
            item.crit = 0;
            item.magic = true;
            item.mana = 5;
            item.width = 95;
            item.height = 15;
            item.scale = 1f;
            item.useTime = 23;
            item.useAnimation = 23;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.noMelee = true;
            item.knockBack = 4;
            item.value = Item.buyPrice(gold: 1);
            item.rare = 3;
            item.UseSound = SoundID.Item43;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("ScourgeProj");
            item.shootSpeed = 16f;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-20, -4);
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);          
            recipe.AddIngredient(ItemID.MeteoriteBar, 9);
            recipe.AddIngredient(ItemID.Emerald, 3);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);          
            recipe.AddIngredient(ItemID.MeteoriteBar, 9);
            recipe.AddIngredient(ItemID.Ruby, 3);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);          
            recipe.AddIngredient(ItemID.MeteoriteBar, 9);
            recipe.AddIngredient(ItemID.Diamond, 3);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            var proj = ShootWith(position, speedX, speedY, type, damage, knockBack, offset: item.width - 20);
            var globalProj = proj.GetGlobalProjectile<Projectiles.wfGlobalProj>();
            globalProj.critMult = 1.4f;
            proj.penetrate = 3;
            proj.extraUpdates = 0;
            proj.usesLocalNPCImmunity = true;
            proj.localNPCHitCooldown = -1;

            return false;
        }
    }
}