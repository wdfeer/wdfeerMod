using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace wfMod.Items.Weapons
{
    public class Quassus : wdfeerWeapon
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Throws 6 projectiles that inflict Slash");
        }
        public override void SetDefaults()
        {
            item.damage = 18;
            item.crit = 26;
            item.melee = true;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.width = 48;
            item.height = 43;
            item.useTime = 48;
            item.useAnimation = 48;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.knockBack = 4;
            item.value = Item.buyPrice(gold: 10);
            item.rare = 4;
            item.shoot = ModContent.ProjectileType<Projectiles.QuassusProj>();
            item.UseSound = SoundID.Item39;
            item.autoReuse = true;
            item.shootSpeed = 16f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.AdamantiteBar, 12);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.TitaniumBar, 12);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            for (int i = 0; i < 6; i++)
            {
                var proj = ShootWith(position, speedX, speedY, type, damage, knockBack, 0.14f);
                proj.GetGlobalProjectile<Projectiles.wdfeerGlobalProj>().AddProcChance(new ProcChance(mod.BuffType("SlashProc"), 100));
            }
            Main.PlaySound(SoundID.Item1, position);

            return false;
        }
    }
}