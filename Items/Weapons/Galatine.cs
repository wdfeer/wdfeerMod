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
            item.damage = 40; // The damage your item deals
            item.melee = true; // Whether your item is part of the melee class
            item.width = 58; // The item texture's width
            item.height = 58; // The item texture's height
            item.useTime = 48; // The time span of using the weapon. Remember in terraria, 60 frames is a second.
            item.useAnimation = 48; // The time span of the using animation of the weapon, suggest setting it the same as useTime.
            item.knockBack = 9; // The force of knockback of the weapon. Maximum is 20
            item.value = Item.buyPrice(silver: 180); // The value of the weapon in copper coins
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
                       
            recipe.AddIngredient(ItemID.Muramasa);
            recipe.AddIngredient(ItemID.MeteoriteBar,8);
            
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(mod.BuffType("SlashProc"), 300);
            var slashDamage = damage / 5;
            target.GetGlobalNPC<wdfeerGlobalNPC>().AddStackableProc(ProcType.Slash, 300, slashDamage);
        }
    }
}