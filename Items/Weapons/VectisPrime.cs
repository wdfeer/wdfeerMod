using Terraria;
using Terraria.DataStructures;
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
            Item.damage = 200;
            Item.crit = 26;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 63;
            Item.height = 19;
            Item.useTime = 40;
            Item.useAnimation = 40;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 8;
            Item.value = Item.buyPrice(gold: 12);
            Item.rare = 8;
            Item.UseSound = SoundID.Item40;
            Item.autoReuse = false;
            Item.shootSpeed = 48f;
            Item.shoot = 10;
            Item.useAmmo = AmmoID.Bullet; // The "ammo Id" of the ammo item that this weapon uses. Note that this is not an item Id, but just a magic value.
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(0, 4);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.SniperRifle, 1);
            recipe.AddIngredient(ItemID.HallowedBar, 8);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
        int shots = 0;
        string soundPath => shots % 2 == 0 ? "Sounds/VectisPrimeSound1" : "Sounds/VectisPrimeSound2";
        public override bool CanUseItem(Player player)
        {
            if (shots % 2 == 0)
            {
                Item.useTime = 40;
                Item.useAnimation = 40;
            }
            else
            {
                Item.useTime = 66;
                Item.useAnimation = 66;
            }

            return base.CanUseItem(player);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            sound = Mod.GetSound(soundPath).CreateInstance();
            sound.Volume = 0.55f;
            sound.Pitch += Main.rand.NextFloat(-0.1f, 0.1f);
            sound.Play();
            shots++;

            var proj = ShootWith(position, speedX, speedY, ProjectileID.SniperBullet, damage, knockBack, offset: Item.width);
            proj.GetGlobalProjectile<Projectiles.wfGlobalProj>().AddProcChance(new ProcChance(Mod.Find<ModBuff>("SlashProc").Type, 8));
            proj.DamageType = DamageClass.Ranged;
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