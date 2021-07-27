using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace wdfeerMod.Items.Weapons
{
    public class Sarpa : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Fires 5 rounds in a burst\n28% chance to proc Slash\nDamage Falloff starts at 25 tiles, stops after 50");
        }
        public override void SetDefaults()
        {
            item.damage = 17;
            item.crit = 10;
            item.melee = true;
            item.noMelee = true;
            item.width = 48;
            item.height = 24;
            item.scale = 1f;
            item.useTime = 60;
            item.useAnimation = 60;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.knockBack = 2;
            item.value = Item.buyPrice(gold: 1);
            item.rare = 3;
            item.autoReuse = true;
            item.shoot = ProjectileID.Bullet;
            item.shootSpeed = 16f;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-4, 2);
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.MeteoriteBar, 8);
            recipe.AddIngredient(ItemID.Handgun, 1);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Main.PlaySound(SoundID.Item11, position);

            var modPlayer = Main.LocalPlayer.GetModPlayer<wdfeerPlayer>();
            if (modPlayer.burstInterval == -1)
            {
                modPlayer.offsetP = position - player.position;
                modPlayer.burstItem = item.modItem;
                modPlayer.burstInterval = 3;
                modPlayer.burstsMax = 5;
                modPlayer.burstCount = 1;
                modPlayer.speedXP = speedX;
                modPlayer.speedYP = speedY;
                modPlayer.typeP = type;
                modPlayer.damageP = damage;
                modPlayer.knockbackP = knockBack;
            }

            Vector2 spawnOffset = new Vector2(speedX, speedY);
            spawnOffset.Normalize();
            spawnOffset *= item.width;
            position += spawnOffset;
            Vector2 spread = new Vector2(speedY, -speedX);
            int proj = Projectile.NewProjectile(position, new Vector2(speedX, speedY) + spread*Main.rand.NextFloat(0.05f,-0.05f), type, damage, knockBack, Main.LocalPlayer.cHead);
            var projectile = Main.projectile[proj];
            projectile.ranged = false;
            projectile.melee = true;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 2;
            var gProj = projectile.GetGlobalProjectile<Projectiles.wdfeerGlobalProj>();
            gProj.v2 = projectile.position;
            gProj.falloffStartDist = 500;
            gProj.falloffMaxDist = 1000;
            gProj.falloffMax = 0.86f;
            gProj.slashChance = 28;

            return false;
        }
    }
}