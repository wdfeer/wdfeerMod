using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace wfMod.Items.Weapons
{
    public class KuvaNukor : wfWeapon
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Will confuse enemies\nCan hit up to 3 enemies\n+120% Critical Damage\nLimited range");
        }
        public override void SetDefaults()
        {
            Item.damage = 17;
            Item.crit = 3;
            Item.DamageType = DamageClass.Magic;
            Item.width = 32;
            Item.height = 24;
            Item.useTime = 6;
            Item.useAnimation = 6;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 0;
            Item.value = 20000;
            Item.rare = 4;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.NukorProj>();
            Item.shootSpeed = 16f;
            Item.mana = 4;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(Mod.Find<ModItem>("Nukor").Type);
            recipe.AddIngredient(Mod.Find<ModItem>("Kuva").Type, 7);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
        int lastShotTime = 0;
        int timeSinceLastShot = 60;
        int shots = 0;
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            var proj = ShootWith(position, speedX, speedY, type, damage, knockBack, offset: Item.width + 1);
            Projectiles.NukorProj modProj = proj.ModProjectile as Projectiles.NukorProj;
            modProj.chain = true;
            modProj.confusedChance = 50;
            var gProj = proj.GetGlobalProjectile<Projectiles.wfGlobalProj>();
            gProj.critMult = 2.5f;
            gProj.onHit = (NPC target) =>
            {
                gProj.hitNPCs[gProj.hits] = target;
                gProj.hits++;
            };

            timeSinceLastShot = player.GetModPlayer<wfPlayer>().longTimer - lastShotTime;
            lastShotTime = player.GetModPlayer<wfPlayer>().longTimer;

            if (timeSinceLastShot > 12)
            {
                sound = Mod.GetSound("Sounds/KuvaNukorStartSound").CreateInstance();
                sound.Volume = 0.4f;
                sound.Play();
            }
            else if (shots % 2 == 0)
            {
                int rand = Main.rand.Next(3);

                switch (rand)
                {
                    case 0:
                        sound = Mod.GetSound("Sounds/KuvaNukorLoop1").CreateInstance();
                        break;
                    case 1:
                        sound = Mod.GetSound("Sounds/KuvaNukorLoop2").CreateInstance();
                        break;
                    default:
                        sound = Mod.GetSound("Sounds/KuvaNukorLoop3").CreateInstance();
                        break;
                }

                sound.Volume = 0.3f;
                sound.Play();
            }
            shots++;

            return false;
        }
    }
}