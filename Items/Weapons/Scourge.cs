using Terraria;
using Terraria.DataStructures;
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
            Item.damage = 24;
            Item.crit = 0;
            Item.DamageType = DamageClass.Magic;
            Item.mana = 5;
            Item.width = 95;
            Item.height = 15;
            Item.scale = 1f;
            Item.useTime = 23;
            Item.useAnimation = 23;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 4;
            Item.value = Item.buyPrice(gold: 1);
            Item.rare = 3;
            Item.UseSound = SoundID.Item43;
            Item.autoReuse = true;
            Item.shoot = Mod.Find<ModProjectile>("ScourgeProj").Type;
            Item.shootSpeed = 16f;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-20, -4);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();          
            recipe.AddIngredient(ItemID.MeteoriteBar, 9);
            recipe.AddIngredient(ItemID.Emerald, 3);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();

            recipe = CreateRecipe();          
            recipe.AddIngredient(ItemID.MeteoriteBar, 9);
            recipe.AddIngredient(ItemID.Ruby, 3);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();

            recipe = CreateRecipe();          
            recipe.AddIngredient(ItemID.MeteoriteBar, 9);
            recipe.AddIngredient(ItemID.Diamond, 3);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            var proj = ShootWith(position, speedX, speedY, type, damage, knockBack, offset: Item.width - 20);
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