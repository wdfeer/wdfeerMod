using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace wfMod.Items.Weapons
{
    public class Stradavar : wdfeerWeapon
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Right Click to switch between Auto and Semi-auto fire modes\n50% Chance not to consume ammo in Auto");
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
                    item.damage = 3;
                    item.crit = 20;
                    item.useTime = 6;
                    item.useAnimation = 6;
                    item.autoReuse = true;
                    break;
                default:
                    item.damage = 12;
                    item.crit = 24;
                    item.useTime = 12;
                    item.useAnimation = 12;
                    item.autoReuse = false;
                    break;
            }

            item.ranged = true;
            item.noMelee = true;
            item.width = 40;
            item.height = 12;
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
            recipe.AddIngredient(ItemID.Minishark, 1);
            recipe.AddIngredient(ItemID.Handgun, 1);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        public override bool ConsumeAmmo(Player player)
        {
            if (Mode == 0 && Main.rand.Next(0, 100) < 50) return false;
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
            var proj = ShootWith(position, speedX, speedY, type, damage, knockBack, (Mode == 0 ? 0.01f : 0.005f), item.width, SoundID.Item11);
            proj.usesLocalNPCImmunity = true;
            proj.localNPCHitCooldown = 3;

            return false;
        }
    }
}