using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace wdfeerMod.Items.Weapons
{
    public class Galatine : ModItem
    {
        Random rand = new Random();
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Applies Slash Procs on hit");
        }
        public override void SetDefaults()
        {
            item.damage = 42; // The damage your item deals
            item.melee = true; // Whether your item is part of the melee class
            item.width = 58; // The item texture's width
            item.height = 58; // The item texture's height
            item.useTime = 60; // The time span of using the weapon. Remember in terraria, 60 frames is a second.
            item.useAnimation = 60; // The time span of the using animation of the weapon, suggest setting it the same as useTime.
            item.knockBack = 9; // The force of knockback of the weapon. Maximum is 20
            item.value = Item.buyPrice(silver: 75); // The value of the weapon in copper coins
            item.rare = ItemRarityID.Orange; // The rarity of the weapon, from -1 to 13. You can also use ItemRarityID.TheColorRarity
            item.UseSound = SoundID.Item1; // The sound when the weapon is being used
            item.autoReuse = true; // Whether the weapon can be used more than once automatically by holding the use button
            item.crit = 10; // The critical strike chance the weapon has. The player, by default, has 4 critical strike chance
            item.scale = 1.1f;
            item.useStyle = ItemUseStyleID.SwingThrow; 
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            // ItemType<ExampleItem>() is how to get the ExampleItem item, 10 is the amount of that item you need to craft the recipe
            recipe.AddRecipeGroup("IronBar", 12);
            recipe.AddIngredient(ItemID.MeteoriteBar, 8);
            // You can use recipe.AddIngredient(ItemID.TheItemYouWantToUse, the amount of items needed); for a vanilla item.
            recipe.AddTile(TileID.Anvils); // Set the crafting tile to ExampleWorkbench
            recipe.SetResult(this); // Set the result to this item (ExampleSword)
            recipe.AddRecipe(); // When your done, add the recipe
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(mod.BuffType("SlashProc"), 300);
            var slashDamage = damage / 5;
            target.GetGlobalNPC<wdfeerGlobalNPC>().AddStackableProc("slash", 300, ref slashDamage);
        }
    }
}