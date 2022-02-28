using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace wfMod.Items.Weapons
{
    public class ArumSpinosa : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Throws 6 projectiles that inflict Slash and Venom");
        }
        public override void SetDefaults()
        {
            item.damage = 28;
            item.crit = 5;
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
            item.rare = 5;
            item.shoot = ModContent.ProjectileType<Projectiles.ArumSpinosaProj>();
            item.UseSound = SoundID.Item39;
            item.autoReuse = true;
            item.shootSpeed = 16f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.ChlorophyteBar, 12);
            recipe.AddIngredient(ItemID.SpiderFang, 8);
            
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        int proj = 0;
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 spread = new Vector2(speedY, -speedX);
            for (int i = 0; i < 6; i++)
            {
                proj = Projectile.NewProjectile(position, new Vector2(speedX, speedY) + spread * Main.rand.NextFloat(-0.15f, 0.15f), type, damage, knockBack, Main.LocalPlayer.cHead);
                Main.projectile[proj].GetGlobalProjectile<Projectiles.wfGlobalProj>().AddProcChance(new ProcChance(mod.BuffType("SlashProc"),50));
            }
            Main.PlaySound(SoundID.Item1, position);

            return false;
        }
    }
}