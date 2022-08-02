using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
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
            Item.damage = 28;
            Item.crit = 5;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.width = 48;
            Item.height = 43;
            Item.useTime = 48;
            Item.useAnimation = 48;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 4;
            Item.value = Item.buyPrice(gold: 10);
            Item.rare = 5;
            Item.shoot = ModContent.ProjectileType<Projectiles.ArumSpinosaProj>();
            Item.UseSound = SoundID.Item39;
            Item.autoReuse = true;
            Item.shootSpeed = 16f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.ChlorophyteBar, 12);
            recipe.AddIngredient(ItemID.SpiderFang, 8);
            
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }

        int proj = 0;
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 spread = new Vector2(speedY, -speedX);
            for (int i = 0; i < 6; i++)
            {
                proj = Projectile.NewProjectile(position, new Vector2(speedX, speedY) + spread * Main.rand.NextFloat(-0.15f, 0.15f), type, damage, knockBack, Main.LocalPlayer.cHead);
                Main.projectile[proj].GetGlobalProjectile<Projectiles.wfGlobalProj>().AddProcChance(new ProcChance(Mod.Find<ModBuff>("SlashProc").Type,50));
            }
            SoundEngine.PlaySound(SoundID.Item1, position);

            return false;
        }
    }
}