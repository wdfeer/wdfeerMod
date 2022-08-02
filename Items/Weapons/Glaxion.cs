using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace wfMod.Items.Weapons
{
    public class Glaxion : wfWeapon
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Will slow and freeze enemies");
        }
        public override void SetDefaults()
        {
            Item.damage = 18;
            Item.crit = 4;
            Item.DamageType = DamageClass.Magic;
            Item.mana = 2;
            Item.width = 64;
            Item.height = 14;
            Item.useTime = 5;
            Item.useAnimation = 5;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 0;
            Item.value = 60000;
            Item.rare = 4;
            Item.autoReuse = true;
            Item.shoot = ProjectileID.MagnetSphereBolt;
            Item.shootSpeed = 16f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.FrostCore, 1);
            recipe.AddIngredient(ItemID.CobaltBar, 12);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.FrostCore, 1);
            recipe.AddIngredient(ItemID.PalladiumBar, 12);
            recipe.AddTile(TileID.MythrilAnvil);
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

            var proj = ShootWith(position, speedX, speedY, type, damage, knockBack, offset: Item.width + 1);
            var globalProj = proj.GetGlobalProjectile<Projectiles.wfGlobalProj>();
            globalProj.onHit = (NPC target) =>
            {
                if (Main.rand.Next(0, 100) < 30)
                {
                    if (target.HasBuff(BuffID.Slow)) target.AddBuff(BuffID.Frozen, 100);
                    else target.AddBuff(BuffID.Slow, 100);
                }
            };
            globalProj.canHitNPC = (NPC target) =>
            {
                if (globalProj.hitNPCs.Contains<NPC>(target)) return false;
                return null;
            };

            return false;
        }
    }
}