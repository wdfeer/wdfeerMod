using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace wfMod.Items.Weapons
{
    public class KarystPrime : wdfeerWeapon
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("A heavy throwing dagger that inflicts poison or venom and slash with a 20% chance\n+10% Movement Speed while held\n+10% Critical damage");
        }
        public override void SetDefaults()
        {
            item.damage = 107;
            item.crit = 20;
            item.melee = true;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.width = 32;
            item.height = 32;
            item.useTime = 60;
            item.useAnimation = 60;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.knockBack = 7;
            item.value = Item.buyPrice();
            item.rare = 6;
            item.shoot = ModContent.ProjectileType<Projectiles.KarystPrimeProj>();
            item.autoReuse = true;
            item.shootSpeed = 17f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("Karyst"));
            recipe.AddIngredient(ItemID.HallowedBar, 17);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            var projectile = ShootWith(position, speedX, speedY, type, damage, knockBack, sound: SoundID.Item1);
            var gProj = projectile.GetGlobalProjectile<Projectiles.wdfeerGlobalProj>();
            gProj.AddProcChance(new ProcChance(mod.BuffType("SlashProc"), 20));
            gProj.critMult = 1.1f;

            return false;
        }
        public override void HoldItem(Player player)
        {
            player.moveSpeed += 0.11f;
        }
    }
}