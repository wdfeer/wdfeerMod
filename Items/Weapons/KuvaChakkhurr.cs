using Terraria;
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
            item.damage = 113; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
            item.crit = 46;
            item.ranged = true; // sets the damage type to ranged
            item.width = 32; // hitbox width of the item
            item.height = 10; // hitbox height of the item
            item.scale = 1.7f;
            item.useTime = 51; // The item's use time in ticks (60 ticks == 1 second.)
            item.useAnimation = 51; // The length of the item's use animation in ticks (60 ticks == 1 second.)
            item.useStyle = ItemUseStyleID.HoldingOut; // how you use the item (swinging, holding out, etc)
            item.noMelee = true; //so the item's animation doesn't do damage
            item.knockBack = 10; // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
            item.value = Item.buyPrice(gold: 7); // how much the item sells for (measured in copper)
            item.rare = 5; // the color that the item's name will be in-game
            item.autoReuse = false; // if you can hold click to automatically use it again
            item.shootSpeed = 20f; // the speed of the projectile (measured in pixels per frame)
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
            recipe.AddIngredient(ItemID.TitaniumBar, 8);
            recipe.AddIngredient(mod.ItemType("Kuva"), 5);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.AdamantiteBar, 8);
            recipe.AddIngredient(mod.ItemType("Kuva"), 5);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        Microsoft.Xna.Framework.Audio.SoundEffectInstance sound;
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            sound = mod.GetSound("Sounds/KuvaChakkhurrSound").CreateInstance();
            sound.Volume = 0.5f;
            sound.Pitch += Main.rand.NextFloat(-0.1f, 0.1f);
            sound.Play();

            var proj = ShootWith(position, speedX, speedY, type, damage, knockBack, offset: item.width * item.scale + 2);
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
                    Main.PlaySound(new Terraria.Audio.LegacySoundStyle(2, 14).WithVolume(0.4f), p.position);
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