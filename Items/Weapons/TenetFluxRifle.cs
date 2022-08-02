using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace wfMod.Items.Weapons
{
    public class TenetFluxRifle : wfWeapon
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Doesn't consume ammo\n75% Slash chance\n-10% Critical Damage");

        }
        public override void SetDefaults()
        {
            pathToSound = "Sounds/TenetFluxRifleSound";
            Item.damage = 29;
            Item.crit = 16;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 45;
            Item.height = 16;
            Item.useTime = 4;
            Item.useAnimation = 4;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 0;
            Item.value = Item.sellPrice(gold: 11);
            Item.rare = 8;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.TenetFluxRifleProj>(); ;
            Item.shootSpeed = 16f;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-4, 0);
        }
        public override void AddRecipes()
        {
            var recipe = CreateRecipe();
            recipe.AddIngredient(Mod.Find<ModItem>("FluxRifle").Type);
            recipe.AddIngredient(Mod.Find<ModItem>("Fieldron").Type);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            PlaySound(Main.rand.NextFloat(-0.1f, 0.1f));

            var gProj = ShootWith(position, speedX, speedY, type, damage, knockBack, 0.002f, Item.width).GetGlobalProjectile<Projectiles.wfGlobalProj>();
            gProj.AddProcChance(new ProcChance(Mod.Find<ModBuff>("SlashProc").Type, 75));
            gProj.critMult = 0.9f;
            return false;
        }
    }
}