using Terraria;
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
            item.damage = 69;
            item.crit = 16;
            item.melee = true;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.width = 32;
            item.height = 32;
            item.useTime = 14;
            item.useAnimation = 14;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.knockBack = 4;
            item.value = 750000;
            item.rare = 4;
            item.shoot = ModContent.ProjectileType<Projectiles.OrviusProj>();
            item.shootSpeed = 19f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.AdamantiteBar, 8);
            recipe.AddIngredient(mod.ItemType("Kuva"), 3);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.TitaniumBar, 8);
            recipe.AddIngredient(mod.ItemType("Kuva"), 3);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        Projectile proj;
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (proj != null && proj.active && proj.modProjectile is Projectiles.OrviusProj)
            {
                (proj.modProjectile as Projectiles.OrviusProj).Explode();
            }
            else
            {
                proj = ShootWith(position, speedX, speedY, type, damage, knockBack);
                proj.GetGlobalProjectile<Projectiles.wfGlobalProj>().critMult = 1.1f;
                proj.GetGlobalProjectile<Projectiles.wfGlobalProj>().AddProcChance(new ProcChance(mod.BuffType("SlashProc"), 20));
                Main.PlaySound(SoundID.Item1, position);
            }

            return false;
        }
    }
}