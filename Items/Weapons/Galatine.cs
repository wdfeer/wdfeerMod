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
            Item.damage = 40;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.width = 58;
            Item.height = 58;
            Item.useTime = 48;
            Item.useAnimation = 48;
            Item.knockBack = 9;
            Item.value = Item.buyPrice(silver: 180);
            Item.rare = ItemRarityID.Orange;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.crit = 10;
            Item.scale = 1.1f;
            Item.useStyle = ItemUseStyleID.Swing;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
                       
            recipe.AddIngredient(ItemID.Muramasa);
            recipe.AddIngredient(ItemID.MeteoriteBar,8);
            
            recipe.AddTile(TileID.Anvils);
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