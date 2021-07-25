using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace wdfeerMod.Items.Weapons
{
    public class Fulmin : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("+20% Critical Damage\n16% Electricity proc chance");
        }
        public override void SetDefaults()
        {
            item.damage = 24; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
            item.crit = 26;
            item.ranged = true; // sets the damage type to ranged
            item.width = 48; // hitbox width of the item
            item.height = 15; // hitbox height of the item
            item.useTime = 28; // The item's use time in ticks (60 ticks == 1 second.)
            item.useAnimation = 28; // The length of the item's use animation in ticks (60 ticks == 1 second.)
            item.useStyle = ItemUseStyleID.HoldingOut; // how you use the item (swinging, holding out, etc)
            item.noMelee = true; //so the item's animation doesn't do damage
            item.knockBack = 8; // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
            item.value = 15000; // how much the item sells for (measured in copper)
            item.rare = 3; // the color that the item's name will be in-game
            item.autoReuse = true; // if you can hold click to automatically use it again
            item.UseSound = mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/FulminSound").WithVolume(0.6f);
            item.shoot = ModContent.ProjectileType<Projectiles.FulminProj>();
            item.shootSpeed = 36f; // the speed of the projectile (measured in pixels per frame)
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.ShadowScale, 8);
            recipe.AddIngredient(ItemID.Feather, 8);
            recipe.AddTile(TileID.Hellforge); // Set the crafting tile to ExampleWorkbench
            recipe.SetResult(this); // Set the result to this item (ExampleSword)
            recipe.AddRecipe(); // When your done, add the recipe

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.TissueSample, 8);
            recipe.AddIngredient(ItemID.Feather, 8);
            recipe.AddTile(TileID.Hellforge); // Set the crafting tile to ExampleWorkbench
            recipe.SetResult(this); // Set the result to this item (ExampleSword)
            recipe.AddRecipe(); // When your done, add the recipe
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int proj = Projectile.NewProjectile(position, new Vector2(speedX, speedY), type, damage, knockBack, Main.LocalPlayer.cHead);
            var projectile = Main.projectile[proj];
            projectile.GetGlobalProjectile<Projectiles.wdfeerGlobalProj>().critMult = 1.2f;

            return false;
        }
    }
}