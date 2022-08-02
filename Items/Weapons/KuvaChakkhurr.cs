using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;

namespace wfMod.Items.Weapons
{
    public class KuvaChakkhurr : wfWeapon
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Shot bullets explode on impact, dealing 33% of the damage\n+15% Critical Damage");
        }
        public override void SetDefaults()
        {
            Item.damage = 113;
            Item.crit = 46;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 32;
            Item.height = 10;
            Item.scale = 1.7f;
            Item.useTime = 51;
            Item.useAnimation = 51;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 10;
            Item.value = Item.buyPrice(gold: 7);
            Item.rare = 5;
            Item.autoReuse = false;
            Item.shootSpeed = 20f;
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
            recipe.AddIngredient(ItemID.TitaniumBar, 8);
            recipe.AddIngredient(Mod.Find<ModItem>("Kuva").Type, 5);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.AdamantiteBar, 8);
            recipe.AddIngredient(Mod.Find<ModItem>("Kuva").Type, 5);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            sound = Mod.GetSound("Sounds/KuvaChakkhurrSound").CreateInstance();
            sound.Volume = 0.5f;
            sound.Pitch += Main.rand.NextFloat(-0.1f, 0.1f);
            sound.Play();

            var proj = ShootWith(position, speedX, speedY, type, damage, knockBack, offset: Item.width * Item.scale + 2);
            proj.GetGlobalProjectile<Projectiles.wfGlobalProj>().onTileCollide = () =>
            {
                if (proj.GetGlobalProjectile<Projectiles.wfGlobalProj>().exploding) return;
                proj.GetGlobalProjectile<Projectiles.wfGlobalProj>().Explode(180);
                proj.damage /= 3;
            };
            proj.GetGlobalProjectile<Projectiles.wfGlobalProj>().onHit = (NPC victim) => proj.GetGlobalProjectile<Projectiles.wfGlobalProj>().onTileCollide();
            proj.GetGlobalProjectile<Projectiles.wfGlobalProj>().kill = (Projectile p, int timeLeft) =>
            {
                if (p.GetGlobalProjectile<Projectiles.wfGlobalProj>().exploding)
                {
                    SoundEngine.PlaySound(new Terraria.Audio.LegacySoundStyle(2, 14).WithVolume(0.4f), p.position);
                    // Smoke Dust spawn
                    for (int i = 0; i < 50; i++)
                    {
                        int dustIndex = Dust.NewDust(new Vector2(p.position.X, p.position.Y), p.width, p.height, 31, 0f, 0f, 100, default(Color), 2f);
                        Main.dust[dustIndex].velocity *= 1f;
                    }
                }
            };
            proj.GetGlobalProjectile<Projectiles.wfGlobalProj>().critMult = 1.15f;
            proj.penetrate = -1;
            proj.usesLocalNPCImmunity = true;
            proj.localNPCHitCooldown = -1;
            return false;
        }
    }
}