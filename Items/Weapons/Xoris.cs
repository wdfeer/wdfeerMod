using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace wdfeerMod.Items.Weapons
{
    public class Xoris : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Can explode mid-flight \nEvery fourth explosion deals 3x the damage");
        }
        public override void SetDefaults()
        {
            item.damage = 320; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
            item.crit = 20;
            item.melee = true; // sets the damage type to ranged
            item.noMelee = true;
            item.noUseGraphic = true;
            item.width = 32; // hitbox width of the item
            item.height = 32; // hitbox height of the item
            item.useTime = 9; // The item's use time in ticks (60 ticks == 1 second.)
            item.useAnimation = 9; // The length of the item's use animation in ticks (60 ticks == 1 second.)
            item.useStyle = ItemUseStyleID.SwingThrow; // how you use the item (swinging, holding out, etc)
            item.knockBack = 4; // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
            item.value = 750000; // how much the item sells for (measured in copper)
            item.rare = 8; // the color that the item's name will be in-game
            item.shoot = ModContent.ProjectileType<Projectiles.XorisProj>(); //idk why but all the guns in the vanilla source have this
            item.shootSpeed = 24f; // the speed of the projectile (measured in pixels per frame)
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            // ItemType<ExampleItem>() is how to get the ExampleItem item, 10 is the amount of that item you need to craft the recipe
            recipe.AddIngredient(mod.ItemType("Falcor"),1);
            recipe.AddIngredient(ItemID.FragmentSolar,8);
            // You can use recipe.AddIngredient(ItemID.TheItemYouWantToUse, the amount of items needed); for a vanilla item.
            recipe.AddTile(412); // Set the crafting tile to ExampleWorkbench
            recipe.SetResult(this); // Set the result to this item (ExampleSword)
            recipe.AddRecipe(); // When your done, add the recipe
        }

        int proj = 0;
        int explosionCount = 0;
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (Main.projectile[proj].type == type && Main.projectile[proj].owner == Main.LocalPlayer.cHead && Main.projectile[proj].active) 
            {
                bool crit = false;
                if (explosionCount >= 3) {crit = true; explosionCount = 0;}
                Main.projectile[proj].modProjectile.OnHitPvp(Main.LocalPlayer,0,crit);
                explosionCount++;
            }                 
            else 
            {
                proj = Projectile.NewProjectile(position,new Vector2(speedX,speedY),type,damage,knockBack,Main.LocalPlayer.cHead);  
                Main.PlaySound(SoundID.Item1,position);
            }
                          
            return false;
        }
    }
}