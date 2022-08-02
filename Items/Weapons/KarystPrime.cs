using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace wfMod.Items.Weapons
{
    public class KarystPrime : wfWeapon
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("A heavy throwing dagger that inflicts poison or venom and slash with a 20% chance\n+15% Movement Speed while held\n+10% Critical damage");
        }
        public override void SetDefaults()
        {
            Item.damage = 107;
            Item.crit = 20;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.width = 32;
            Item.height = 32;
            Item.useTime = 60;
            Item.useAnimation = 60;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 7;
            Item.value = Item.buyPrice();
            Item.rare = 6;
            Item.shoot = ModContent.ProjectileType<Projectiles.KarystPrimeProj>();
            Item.autoReuse = true;
            Item.shootSpeed = 17f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(Mod.Find<ModItem>("Karyst").Type);
            recipe.AddIngredient(ItemID.SoulofNight, 17);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            var projectile = ShootWith(position, speedX, speedY, type, damage, knockBack, sound: SoundID.Item1);
            var gProj = projectile.GetGlobalProjectile<Projectiles.wfGlobalProj>();
            gProj.AddProcChance(new ProcChance(Mod.Find<ModBuff>("SlashProc").Type, 20));
            gProj.critMult = 1.1f;

            return false;
        }
        public override void HoldItem(Player player)
        {
            player.moveSpeed += 0.15f;
        }
    }
}