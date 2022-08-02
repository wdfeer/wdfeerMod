using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace wfMod.Items.Weapons
{
    public class Quassus : wfWeapon
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Throws 6 projectiles that inflict Slash");
        }
        public override void SetDefaults()
        {
            Item.damage = 18;
            Item.crit = 26;
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
            Item.rare = 4;
            Item.shoot = ModContent.ProjectileType<Projectiles.QuassusProj>();
            Item.UseSound = SoundID.Item39;
            Item.autoReuse = true;
            Item.shootSpeed = 16f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.AdamantiteBar, 12);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.TitaniumBar, 12);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            for (int i = 0; i < 6; i++)
            {
                var proj = ShootWith(position, speedX, speedY, type, damage, knockBack, 0.14f);
                proj.GetGlobalProjectile<Projectiles.wfGlobalProj>().AddProcChance(new ProcChance(Mod.Find<ModBuff>("SlashProc").Type, 100));
            }
            SoundEngine.PlaySound(SoundID.Item1, position);

            return false;
        }
    }
}