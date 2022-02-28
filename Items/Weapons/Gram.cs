using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace wfMod.Items.Weapons
{
    public class Gram : ModItem
    {
        Random rand = new Random();
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Applies Slash Procs on hit");
        }
        public override void SetDefaults()
        {
            item.damage = 42;
            item.melee = true;
            item.width = 58;
            item.height = 58;
            item.useTime = 63;
            item.useAnimation = 63;
            item.knockBack = 11;
            item.value = Item.buyPrice(silver: 100);
            item.rare = ItemRarityID.Orange;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.crit = 11;
            item.scale = 1.25f;
            //The useStyle of the item. 
            //Use useStyle 1 for normal swinging or for throwing
            //use useStyle 2 for an item that drinks such as a potion,
            //use useStyle 3 to make the sword act like a shortsword
            //use useStyle 4 for use like a life crystal,
            //and use useStyle 5 for staffs or guns
            item.useStyle = ItemUseStyleID.SwingThrow; // 1 is the useStyle
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            
            recipe.AddIngredient(ItemID.HellstoneBar, 8);
            recipe.AddIngredient(ItemID.Amethyst, 4);            
            
            recipe.AddTile(TileID.Hellforge);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(mod.BuffType("SlashProc"), 300);
            var slashDamage = damage / 5;
            target.GetGlobalNPC<wfGlobalNPC>().AddStackableProc(ProcType.Slash, 300, slashDamage);
        }
    }
}