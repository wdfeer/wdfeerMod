using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using Microsoft.Xna.Framework;

namespace wdfeerMod.Items.Weapons
{
    public class GlaxionVandal : wdfeerWeapon
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
            item.UseSound = SoundID.Item91.WithPitchVariance(Main.rand.NextFloat(-0.1f, 0.1f)).WithVolume(0.67f); // The sound that this item plays when used.
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

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            var projectile = ShootWith(position, speedX, speedY, type, damage, knockBack, offset: item.width + 2);
            var gProj = projectile.GetGlobalProjectile<Projectiles.wdfeerGlobalProj>();
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