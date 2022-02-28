using Terraria;
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
            item.damage = 29;
            item.crit = 16;
            item.ranged = true;
            item.width = 45;
            item.height = 16;
            item.useTime = 4;
            item.useAnimation = 4;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.noMelee = true;
            item.knockBack = 0;
            item.value = Item.sellPrice(gold: 11);
            item.rare = 8;
            item.autoReuse = true;
            item.shoot = ModContent.ProjectileType<Projectiles.TenetFluxRifleProj>(); ;
            item.shootSpeed = 16f;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-4, 0);
        }
        public override void AddRecipes()
        {
            var recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("FluxRifle"));
            recipe.AddIngredient(mod.ItemType("Fieldron"));
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            PlaySound(Main.rand.NextFloat(-0.1f, 0.1f));

            var gProj = ShootWith(position, speedX, speedY, type, damage, knockBack, 0.002f, item.width).GetGlobalProjectile<Projectiles.wfGlobalProj>();
            gProj.AddProcChance(new ProcChance(mod.BuffType("SlashProc"), 75));
            gProj.critMult = 0.9f;
            return false;
        }
    }
}