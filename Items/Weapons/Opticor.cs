using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;

namespace wfMod.Items.Weapons
{
    public class Opticor : wfWeapon
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Charges to shoot a devastating beam\nFire rate cannot be increased\n+25% Critical Damage");
        }
        public override void SetDefaults()
        {
            item.damage = 840;
            item.crit = 16;
            item.magic = true;
            item.mana = 77;
            item.width = 48;
            item.height = 16;
            item.useTime = 120;
            item.useAnimation = 120;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.noMelee = true;
            item.knockBack = 20;
            item.value = Item.buyPrice(gold: 10);
            item.rare = 4;
            item.autoReuse = false;
            //item.UseSound = mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/OpticorSound").WithVolume(1.2f);
            item.shoot = mod.ProjectileType("OpticorProj");
            item.shootSpeed = 16f;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.SoulofSight, 7);
            recipe.AddIngredient(ItemID.TitaniumBar, 11);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.SoulofSight, 7);
            recipe.AddIngredient(ItemID.AdamantiteBar, 11);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        Microsoft.Xna.Framework.Audio.SoundEffectInstance sound;
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            sound = mod.GetSound("Sounds/OpticorSound").CreateInstance();
            Main.PlaySoundInstance(sound);

            Vector2 velocity = new Vector2(speedX, speedY);
            var proj = ShootWith(position, speedX, speedY, type, damage, knockBack, offset: item.width);
            wfPlayer modPl = Main.player[item.owner].GetModPlayer<wfPlayer>();
            if (modPl.fireRateMult > 1)
                proj.timeLeft = (int)(146 + 54 / modPl.fireRateMult);
            var globalProj = proj.GetGlobalProjectile<Projectiles.wfGlobalProj>();
            globalProj.critMult = 1.25f;
            globalProj.baseVelocity = velocity;
            globalProj.initialPosition = proj.position - Main.LocalPlayer.position;

            return false;
        }
    }
}