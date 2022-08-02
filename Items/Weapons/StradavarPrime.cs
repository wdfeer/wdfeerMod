using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace wfMod.Items.Weapons
{
    public class StradavarPrime : wfWeapon
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
                    Item.damage = 12;
                    Item.crit = 20;
                    Item.useTime = 6;
                    Item.useAnimation = 6;
                    Item.autoReuse = true;
                    break;
                default:
                    Item.damage = 28;
                    Item.crit = 26;
                    Item.useTime = 12;
                    Item.useAnimation = 12;
                    Item.autoReuse = false;
                    break;
            }

            Item.DamageType = DamageClass.Ranged;
            Item.noMelee = true;
            Item.width = 40;
            Item.height = 13;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 2;
            Item.value = Item.buyPrice(gold: 6);
            Item.rare = 2;
            Item.shoot = 10;
            Item.shootSpeed = 16f;
            Item.useAmmo = AmmoID.Bullet;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(Mod.Find<ModItem>("Stradavar").Type, 1);
            recipe.AddIngredient(ItemID.HallowedBar, 12);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            if (Mode == 0 && Main.rand.Next(0, 100) < 70) return false;
            return base.CanConsumeAmmo(player);
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
                if (player.GetModPlayer<wfPlayer>().longTimer - 30 > lastModeChange)
                {
                    Mode++;
                    lastModeChange = player.GetModPlayer<wfPlayer>().longTimer;
                    SoundEngine.PlaySound(SoundID.Unlock);
                }
                return false;
            }
            return base.CanUseItem(player);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            SoundEngine.PlaySound(SoundID.Item11, position);

            Vector2 spread = new Vector2(speedY, -speedX);
            var projectile = ShootWith(position, speedX, speedY, type, damage, knockBack, Mode == 0 ? 0.005f : 0.002f, Item.width);
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 3;
            if (Mode == 1 && projectile.penetrate < 2 && projectile.penetrate != -1) projectile.penetrate = 2;
            var globalProj = projectile.GetGlobalProjectile<Projectiles.wfGlobalProj>();
            globalProj.critMult = Mode == 0 ? 1.3f : 1.4f;

            return false;
        }
    }
}