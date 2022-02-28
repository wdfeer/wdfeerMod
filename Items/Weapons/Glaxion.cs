using Terraria;
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
            item.damage = 18;
            item.crit = 4;
            item.magic = true;
            item.mana = 2;
            item.width = 64;
            item.height = 14;
            item.useTime = 5;
            item.useAnimation = 5;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.noMelee = true;
            item.knockBack = 0;
            item.value = 60000;
            item.rare = 4;
            item.autoReuse = true;
            item.shoot = ProjectileID.MagnetSphereBolt;
            item.shootSpeed = 16f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.FrostCore, 1);
            recipe.AddIngredient(ItemID.CobaltBar, 12);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.FrostCore, 1);
            recipe.AddIngredient(ItemID.PalladiumBar, 12);
            recipe.AddTile(TileID.MythrilAnvil);
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

            var proj = ShootWith(position, speedX, speedY, type, damage, knockBack, offset: item.width + 1);
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