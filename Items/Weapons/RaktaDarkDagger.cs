using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace wdfeerMod.Items.Weapons
{
    public class RaktaDarkDagger : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Inflicts Confused and replenishes mana on hit\nSpare mana will increase the damage\nMana Sickness reduces mana gain by 50%");
        }
        public override void SetDefaults()
        {
            item.damage = 24; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
            item.crit = 8;
            item.magic = true;
            item.mana = 2;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.width = 11; // hitbox width of the item
            item.height = 48; // hitbox height of the item
            item.useTime = 24; // The item's use time in ticks (60 ticks == 1 second.)
            item.useAnimation = 24; // The length of the item's use animation in ticks (60 ticks == 1 second.)
            item.useStyle = ItemUseStyleID.SwingThrow; // how you use the item (swinging, holding out, etc)
            item.knockBack = 4; // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
            item.value = 750000; // how much the item sells for (measured in copper)
            item.rare = 4; // the color that the item's name will be in-game
            item.shoot = ModContent.ProjectileType<Projectiles.RaktaDarkDaggerProj>(); 
            item.autoReuse = true;
            item.shootSpeed = 16f; // the speed of the projectile (measured in pixels per frame)
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            // ItemType<ExampleItem>() is how to get the ExampleItem item, 10 is the amount of that item you need to craft the recipe
            recipe.AddIngredient(ItemID.MagicDagger, 1);
            recipe.AddIngredient(ItemID.SoulofNight, 16);
            // You can use recipe.AddIngredient(ItemID.TheItemYouWantToUse, the amount of items needed); for a vanilla item.
            recipe.AddTile(TileID.MythrilAnvil); // Set the crafting tile to ExampleWorkbench
            recipe.SetResult(this); // Set the result to this item (ExampleSword)
            recipe.AddRecipe(); // When your done, add the recipe
        }

        int proj = 0;
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            proj = Projectile.NewProjectile(position, new Vector2(speedX, speedY), type, damage, knockBack, Main.LocalPlayer.cHead);
            Main.PlaySound(SoundID.Item1, position);

            return false;
        }
    }
}