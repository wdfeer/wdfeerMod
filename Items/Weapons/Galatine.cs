using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace wfMod.Items.Weapons
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
            item.damage = 40;
            item.melee = true;
            item.width = 58;
            item.height = 58;
            item.useTime = 48;
            item.useAnimation = 48;
            item.knockBack = 9;
            item.value = Item.buyPrice(silver: 180);
            item.rare = ItemRarityID.Orange;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.crit = 10;
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
            target.GetGlobalNPC<wfGlobalNPC>().AddStackableProc(ProcType.Slash, 300, slashDamage);
        }
    }
}