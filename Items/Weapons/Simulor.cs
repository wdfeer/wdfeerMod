using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace wfMod.Items.Weapons
{
    public class Simulor : wdfeerWeapon
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Launches orbs that can't hit enemies directly and bounce off of tiles,\n will attract to each other and merge to create an implosion,\n increasing their damage by 20% up to a maximum of 300%\nImplosions are guaranteed to electrify enemies\nRight Click to force all active orbs to explode");
        }
        public override void SetDefaults()
        {
            item.damage = 56;
            item.crit = 8;
            item.magic = true;
            item.mana = 14;
            item.width = 36;
            item.height = 15;
            item.useTime = 24;
            item.useAnimation = 24;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.noMelee = true;
            item.knockBack = 6.9f;
            item.value = Item.buyPrice(gold: 33);
            item.rare = 6;
            item.autoReuse = false;
            item.UseSound = SoundID.Item117.WithVolume(0.75f);
            item.shoot = ModContent.ProjectileType<Projectiles.SimulorProj>();
            item.shootSpeed = 16f;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override void AddRecipes()
        {
            RecipeGroup.RegisterGroup("Dungeon Key", new RecipeGroup(() => "Dungeon Key", new int[] { ItemID.FrozenKey, ItemID.JungleKey, ItemID.CrimsonKey, ItemID.CorruptionKey, ItemID.HallowedKey }));
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.AdamantiteBar, 8);
            recipe.AddRecipeGroup("Dungeon Key");
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.TitaniumBar, 8);
            recipe.AddRecipeGroup("Dungeon Key");
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        List<Projectiles.SimulorProj> projs = new List<Projectiles.SimulorProj>();
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse != 2)
                return base.CanUseItem(player);
            foreach (Projectiles.SimulorProj proj in projs)
            {
                proj.Explode();
            }
            projs = new List<Projectiles.SimulorProj>();
            return false;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            var proj = ShootWith(position, speedX, speedY, type, damage, knockBack, offset: item.width + 2);
            projs.Add(proj.modProjectile as Projectiles.SimulorProj);
            return false;
        }
    }
}