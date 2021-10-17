using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace wfMod.Items.Weapons
{
    public class GlaxionVandal : wfWeapon
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Will slow and freeze enemies \nDeals damage in an AoE on impact");
        }
        public override void SetDefaults()
        {
            item.damage = 59; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
            item.crit = 10;
            item.magic = true;
            item.mana = 4;
            item.width = 64; // hitbox width of the item
            item.height = 14; // hitbox height of the item
            item.useTime = 5; // The item's use time in ticks (60 ticks == 1 second.)
            item.useAnimation = 5; // The length of the item's use animation in ticks (60 ticks == 1 second.)
            item.useStyle = ItemUseStyleID.HoldingOut; // how you use the item (swinging, holding out, etc)
            item.noMelee = true; //so the item's animation doesn't do damage
            item.knockBack = 0; // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
            item.value = 500000; // how much the item sells for (measured in copper)
            item.rare = 10; // the color that the item's name will be in-game
            item.autoReuse = true; // if you can hold click to automatically use it again
            item.shoot = ProjectileID.MagnetSphereBolt;
            item.shootSpeed = 16f; // the speed of the projectile (measured in pixels per frame)
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("Glaxion"), 1);
            recipe.AddIngredient(mod.ItemType("Fieldron"));
            recipe.AddIngredient(ItemID.FragmentNebula, 8);
            recipe.AddTile(412);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        int lastShotTime = 0;
        int timeSinceLastShot = 60;
        int shots = 0;
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            timeSinceLastShot = player.GetModPlayer<wfPlayer>().longTimer - lastShotTime;
            lastShotTime = player.GetModPlayer<wfPlayer>().longTimer;
            if (timeSinceLastShot > 10) shots = 0;
            if (shots % 2 == 0)
            {
                int rand = Main.rand.Next(4);
                SoundEffectInstance sound;
                switch (rand)
                {
                    case 0:
                        sound = mod.GetSound("Sounds/GlaxionLoop1").CreateInstance();
                        break;
                    case 1:
                        sound = mod.GetSound("Sounds/GlaxionLoop2").CreateInstance();
                        break;
                    case 2:
                        sound = mod.GetSound("Sounds/GlaxionLoop3").CreateInstance();
                        break;
                    default:
                        sound = mod.GetSound("Sounds/GlaxionLoop4").CreateInstance();
                        break;
                }
                sound.Volume = 0.2f;
                sound.Pitch += 0.1f;
                sound.Play();
            }
            shots++;

            var projectile = ShootWith(position, speedX, speedY, type, damage, knockBack, offset: item.width + 2);
            var gProj = projectile.GetGlobalProjectile<Projectiles.wfGlobalProj>();
            gProj.onTileCollide = () =>
            {
                gProj.Explode(64);
                projectile.damage = projectile.damage * 2 / 3;
            };
            gProj.onHit = (NPC target) =>
            {
                if (Main.rand.Next(0, 100) < 34)
                {
                    if (target.HasBuff(BuffID.Slow)) target.AddBuff(BuffID.Frozen, 100);
                    else target.AddBuff(BuffID.Slow, 100);
                }

                gProj.hitNPCs[gProj.hits] = target;
                gProj.hits++;
                gProj.Explode(128);
                projectile.damage /= 2;
            };
            gProj.canHitNPC = (NPC target) =>
            {
                if (gProj.hitNPCs.Contains<NPC>(target)) return false;
                return null;
            };
            gProj.kill = (Projectile proj, int timeLeft) =>
            {
                if (timeLeft <= 0)
                    for (int i = 0; i < proj.width / 16; i++)
                    {
                        int dustIndex = Dust.NewDust(proj.position, proj.width, proj.height, 226, 0f, 0f, 80, default(Color), 1.2f);
                        var dust = Main.dust[dustIndex];
                        dust.noGravity = true;
                        dust.velocity *= 0.75f;
                    }
            };

            return false;
        }
    }
}