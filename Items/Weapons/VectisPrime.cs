using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;

namespace wfMod.Items.Weapons
{
    public class VectisPrime : wfWeapon
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Shoots a high-velocity bullet that penetrates through 2 enemies\nHas to reload after every second shot");
        }
        public override void SetDefaults()
        {
            item.damage = 200;
            item.crit = 26;
            item.ranged = true;
            item.width = 63;
            item.height = 19;
            item.useTime = 40;
            item.useAnimation = 40;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.noMelee = true;
            item.knockBack = 8;
            item.value = Item.buyPrice(gold: 12);
            item.rare = 8;
            item.UseSound = SoundID.Item40;
            item.autoReuse = false;
            item.shootSpeed = 48f;
            item.shoot = 10;
            item.useAmmo = AmmoID.Bullet; // The "ammo Id" of the ammo item that this weapon uses. Note that this is not an item Id, but just a magic value.
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(0, 4);
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.SniperRifle, 1);
            recipe.AddIngredient(ItemID.HallowedBar, 8);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        int shots = 0;
        string soundPath => shots % 2 == 0 ? "Sounds/VectisPrimeSound1" : "Sounds/VectisPrimeSound2";
        public override bool CanUseItem(Player player)
        {
            if (shots % 2 == 0)
            {
                item.useTime = 40;
                item.useAnimation = 40;
            }
            else
            {
                item.useTime = 66;
                item.useAnimation = 66;
            }

            return base.CanUseItem(player);
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            sound = mod.GetSound(soundPath).CreateInstance();
            sound.Volume = 0.55f;
            sound.Pitch += Main.rand.NextFloat(-0.1f, 0.1f);
            sound.Play();
            shots++;

            var proj = ShootWith(position, speedX, speedY, ProjectileID.SniperBullet, damage, knockBack, offset: item.width);
            proj.GetGlobalProjectile<Projectiles.wfGlobalProj>().AddProcChance(new ProcChance(mod.BuffType("SlashProc"), 8));
            proj.ranged = true;
            proj.friendly = true;
            proj.hostile = false;
            proj.extraUpdates = 6;
            proj.penetrate = 3;
            proj.usesLocalNPCImmunity = true;
            proj.localNPCHitCooldown = -1;
            return false;
        }
    }
}