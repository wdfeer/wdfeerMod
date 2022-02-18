using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace wfMod.Items.Weapons
{
    public class SynoidSimulor : wfWeapon
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Launches orbs that can't hit enemies directly and bounce off of tiles,\n will attract to each other and merge to create an implosion,\n increasing their damage by 20% up to a maximum of 300%\nImplosions are guaranteed to electrify enemies\nRight Click to force all active orbs to explode");
        }
        public override void SetDefaults()
        {
            item.damage = 77;
            item.crit = 10;
            item.magic = true;
            item.mana = 15;
            item.width = 31;
            item.height = 15;
            item.useTime = 24;
            item.useAnimation = 24;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.noMelee = true;
            item.knockBack = 6.9f;
            item.value = Item.buyPrice(gold: 33);
            item.rare = 6;
            item.autoReuse = false;
            item.shoot = ModContent.ProjectileType<Projectiles.SimulorProj>();
            item.shootSpeed = 16f;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("Simulor"));
            recipe.AddIngredient(ItemID.SoulofSight, 8);
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
        Microsoft.Xna.Framework.Audio.SoundEffectInstance sound;
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            sound = mod.GetSound("Sounds/SynoidSimulorSound").CreateInstance();
            sound.Pitch += Main.rand.NextFloat(-0.08f, 0.08f);
            Main.PlaySoundInstance(sound);

            var proj = ShootWith(position, speedX, speedY, type, damage, knockBack, offset: item.width + 2);
            projs.Add(proj.modProjectile as Projectiles.SimulorProj);
            return false;
        }
    }
}