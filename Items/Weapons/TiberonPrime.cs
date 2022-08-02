using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace wfMod.Items.Weapons
{
    public class TiberonPrime : wfWeapon
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
                    Item.crit = 12;
                    Item.useTime = 7;
                    Item.useAnimation = 7;
                    Item.autoReuse = true;
                    break;
                case 1:
                    Item.crit = 24;
                    Item.useTime = 20;
                    Item.useAnimation = 20;
                    Item.autoReuse = false;
                    break;
                default:
                    Item.crit = 26;
                    Item.useTime = 10;
                    Item.useAnimation = 10;
                    Item.autoReuse = false;
                    break;
            }
            Item.damage = 22;
            Item.DamageType = DamageClass.Ranged;
            Item.noMelee = true;
            Item.width = 39;
            Item.height = 9;
            Item.scale = 1.1f;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 3;
            Item.value = Item.buyPrice(gold: 8);
            Item.rare = 5;
            Item.shoot = 10;
            Item.shootSpeed = 17f;
            Item.useAmmo = AmmoID.Bullet;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.AvengerEmblem, 1);
            recipe.AddIngredient(ItemID.HallowedBar, 12);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            if (Mode == 0 && Main.rand.Next(0, 100) < 75) return false;
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
            sound = Mod.GetSound("Sounds/TiberonPrimeSound").CreateInstance();
            sound.Volume = 0.16f;
            sound.Pitch += Main.rand.NextFloat(-0.1f, 0.1f);
            sound.Play();

            int bursts = -1;
            int interval = 0;
            if (Mode == 1)
            {
                bursts = 3;
                interval = Item.useTime / 5;
            }
            var projectile = ShootWith(position, speedX, speedY, type, damage, knockBack, 0.002f, Item.width, bursts: bursts, burstInterval: interval);
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 2;
            var gProj = projectile.GetGlobalProjectile<Projectiles.wfGlobalProj>();
            if (Mode == 0)
                gProj.AddProcChance(new ProcChance(Mod.Find<ModBuff>("SlashProc").Type, 9));
            gProj.critMult = Mode == 0 ? 1.4f : (Mode == 1 ? 1.5f : 1.7f);

            return false;
        }
    }
}