using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace wfMod.Items.Weapons
{
    public class Orvius : wfWeapon
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Can explode mid-flight and Slow enemies\n 20% Slash chance on Hit \n+10% Critical Damage");
        }
        public override void SetDefaults()
        {
            Item.damage = 69;
            Item.crit = 16;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.width = 32;
            Item.height = 32;
            Item.useTime = 14;
            Item.useAnimation = 14;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 4;
            Item.value = 750000;
            Item.rare = 4;
            Item.shoot = ModContent.ProjectileType<Projectiles.OrviusProj>();
            Item.shootSpeed = 19f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.AdamantiteBar, 8);
            recipe.AddIngredient(Mod.Find<ModItem>("Kuva").Type, 3);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.TitaniumBar, 8);
            recipe.AddIngredient(Mod.Find<ModItem>("Kuva").Type, 3);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }

        Projectile proj;
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (proj != null && proj.active && proj.ModProjectile is Projectiles.OrviusProj)
            {
                (proj.ModProjectile as Projectiles.OrviusProj).Explode();
            }
            else
            {
                proj = ShootWith(position, speedX, speedY, type, damage, knockBack);
                proj.GetGlobalProjectile<Projectiles.wfGlobalProj>().critMult = 1.1f;
                proj.GetGlobalProjectile<Projectiles.wfGlobalProj>().AddProcChance(new ProcChance(Mod.Find<ModBuff>("SlashProc").Type, 20));
                SoundEngine.PlaySound(SoundID.Item1, position);
            }

            return false;
        }
    }
}