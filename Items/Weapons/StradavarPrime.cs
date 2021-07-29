using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace wdfeerMod.Items.Weapons
{
    public class StradavarPrime : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Right Click to switch between Auto and Semi-auto fire modes\n+30% Critical Damage in Auto, +40% in Semi-auto\n70% Chance not to consume ammo in Auto");
        }
        int mode = 1;
        public int Mode // 0 is Auto, 1 is Semi
        {
            get => mode;
            set
            {
                if (value > 1) value = 0;
                mode = value;
                SetDefaults();
            }
        }
        public override void SetDefaults()
        {
            switch (Mode)
            {
                case 0:
                    item.damage = 12;
                    item.crit = 20;
                    item.useTime = 6;
                    item.useAnimation = 6;
                    item.autoReuse = true;
                    break;
                default:
                    item.damage = 28;
                    item.crit = 26;
                    item.useTime = 12;
                    item.useAnimation = 12;
                    item.autoReuse = false;
                    break;
            }

            item.ranged = true;
            item.noMelee = true;
            item.width = 40;
            item.height = 13;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.knockBack = 2;
            item.value = Item.buyPrice(gold: 6);
            item.rare = 2;
            item.shoot = 10;
            item.shootSpeed = 16f;
            item.useAmmo = AmmoID.Bullet;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("Stradavar"),1);
            recipe.AddIngredient(ItemID.HallowedBar, 12);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        public override bool ConsumeAmmo(Player player)
        {
            if (Mode == 0 && Main.rand.Next(0, 100) < 70) return false;
            return base.ConsumeAmmo(player);
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        int lastModeChange;
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                if (player.GetModPlayer<wdfeerPlayer>().longTimer - 30 > lastModeChange)
                {
                    Mode++;
                    lastModeChange = player.GetModPlayer<wdfeerPlayer>().longTimer;
                    Main.PlaySound(SoundID.Unlock);
                }
                return false;
            }
            return base.CanUseItem(player);
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Main.PlaySound(SoundID.Item11, position);

            Vector2 spawnOffset = new Vector2(speedX, speedY);
            spawnOffset.Normalize();
            spawnOffset *= item.width;
            position += spawnOffset;
            Vector2 spread = new Vector2(speedY, -speedX);
            int proj = Projectile.NewProjectile(position, new Vector2(speedX, speedY) + spread * Main.rand.NextFloat(0.003f, -0.003f) * (Mode == 0 ? 2f : 1), type, damage, knockBack, Main.LocalPlayer.cHead);
            var projectile = Main.projectile[proj];
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 3;
            if (Mode == 1 && projectile.penetrate < 2 && projectile.penetrate != -1) projectile.penetrate = 2;
            var globalProj = projectile.GetGlobalProjectile<Projectiles.wdfeerGlobalProj>();
            globalProj.critMult = Mode == 0 ? 1.3f : 1.4f;

            return false;
        }
    }
}