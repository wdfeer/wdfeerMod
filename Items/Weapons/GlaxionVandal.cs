using Terraria;
using Terraria.DataStructures;
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
            Item.damage = 59;
            Item.crit = 10;
            Item.DamageType = DamageClass.Magic;
            Item.mana = 4;
            Item.width = 64;
            Item.height = 14;
            Item.useTime = 5;
            Item.useAnimation = 5;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 0;
            Item.value = 500000;
            Item.rare = 10;
            Item.autoReuse = true;
            Item.shoot = ProjectileID.MagnetSphereBolt;
            Item.shootSpeed = 16f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(Mod.Find<ModItem>("Glaxion").Type, 1);
            recipe.AddIngredient(Mod.Find<ModItem>("Fieldron").Type);
            recipe.AddIngredient(ItemID.FragmentNebula, 8);
            recipe.AddTile(412);
            recipe.Register();
        }
        int lastShotTime = 0;
        int timeSinceLastShot = 60;
        int shots = 0;
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
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
                        sound = Mod.GetSound("Sounds/GlaxionLoop1").CreateInstance();
                        break;
                    case 1:
                        sound = Mod.GetSound("Sounds/GlaxionLoop2").CreateInstance();
                        break;
                    case 2:
                        sound = Mod.GetSound("Sounds/GlaxionLoop3").CreateInstance();
                        break;
                    default:
                        sound = Mod.GetSound("Sounds/GlaxionLoop4").CreateInstance();
                        break;
                }
                sound.Volume = 0.2f;
                sound.Pitch += 0.1f;
                sound.Play();
            }
            shots++;

            var projectile = ShootWith(position, speedX, speedY, type, damage, knockBack, offset: Item.width + 2);
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