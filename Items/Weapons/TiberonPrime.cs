using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace wdfeerMod.Items.Weapons
{
    public class TiberonPrime : wdfeerWeapon
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Right Click to switch between Auto, Burst and Semi-auto fire modes\n+40%, 50% or 70% Critical damage in Auto, Burst or Semi\n75% Chance not to consume ammo in Auto");
        }
        int mode = 1;
        public int Mode // 0 is Auto, 1 is Burst, 2 is Semi
        {
            get => mode;
            set
            {
                if (value > 2) value = 0;
                mode = value;
                SetDefaults();
            }
        }
        public override void SetDefaults()
        {
            switch (Mode)
            {
                case 0:
                    item.crit = 12;
                    item.useTime = 7;
                    item.useAnimation = 7;
                    item.autoReuse = true;
                    break;
                case 1:
                    item.crit = 24;
                    item.useTime = 20;
                    item.useAnimation = 20;
                    item.autoReuse = false;
                    break;
                default:
                    item.crit = 26;
                    item.useTime = 10;
                    item.useAnimation = 10;
                    item.autoReuse = false;
                    break;
            }
            item.damage = 22;
            item.ranged = true;
            item.noMelee = true;
            item.width = 39;
            item.height = 9;
            item.scale = 1.1f;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.knockBack = 3;
            item.value = Item.buyPrice(gold: 8);
            item.rare = 5;
            item.shoot = 10;
            item.shootSpeed = 17f;
            item.useAmmo = AmmoID.Bullet;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.AvengerEmblem, 1);
            recipe.AddIngredient(ItemID.HallowedBar, 12);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        public override bool ConsumeAmmo(Player player)
        {
            if (Mode == 0 && Main.rand.Next(0, 100) < 75) return false;
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
            int bursts = -1;
            int interval = 0;
            if (Mode == 1)
            {
                bursts = 3;
                interval = item.useTime / 5;
            }
            var projectile = ShootWith(position, speedX, speedY, type, damage, knockBack, 0.002f, item.width, SoundID.Item11, bursts, interval);
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 2;
            var gProj = projectile.GetGlobalProjectile<Projectiles.wdfeerGlobalProj>();
            if (Mode == 0)
                gProj.procChances.Add(new ProcChance(mod.BuffType("SlashProc"), 9));
            gProj.critMult = Mode == 0 ? 1.4f : (Mode == 1 ? 1.5f : 1.7f);

            return false;
        }
    }
}