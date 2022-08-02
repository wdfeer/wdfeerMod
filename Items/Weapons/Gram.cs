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
            Item.damage = 42;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.width = 58;
            Item.height = 58;
            Item.useTime = 63;
            Item.useAnimation = 63;
            Item.knockBack = 11;
            Item.value = Item.buyPrice(silver: 100);
            Item.rare = ItemRarityID.Orange;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.crit = 11;
            Item.scale = 1.25f;
            //The useStyle of the item. 
            //Use useStyle 1 for normal swinging or for throwing
            //use useStyle 2 for an item that drinks such as a potion,
            //use useStyle 3 to make the sword act like a shortsword
            //use useStyle 4 for use like a life crystal,
            //and use useStyle 5 for staffs or guns
            Item.useStyle = ItemUseStyleID.Swing; // 1 is the useStyle
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            
            recipe.AddIngredient(ItemID.HellstoneBar, 8);
            recipe.AddIngredient(ItemID.Amethyst, 4);            
            
            recipe.AddTile(TileID.Hellforge);
            recipe.Register();
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(Mod.Find<ModBuff>("SlashProc").Type, 300);
            var slashDamage = damage / 5;
            target.GetGlobalNPC<wfGlobalNPC>().AddStackableProc(ProcType.Slash, 300, slashDamage);
        }
    }
}