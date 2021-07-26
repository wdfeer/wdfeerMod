using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace wdfeerMod.Items.Weapons
{
    public class Orvius : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Can explode mid-flight and Slow enemies\n 20% Slash chance on Hit \n+10% Critical Damage");
        }
        public override void SetDefaults()
        {
            item.damage = 69; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
            item.crit = 16;
            item.melee = true; // sets the damage type to ranged
            item.noMelee = true;
            item.noUseGraphic = true;
            item.width = 32; // hitbox width of the item
            item.height = 32; // hitbox height of the item
            item.useTime = 14; // The item's use time in ticks (60 ticks == 1 second.)
            item.useAnimation = 14; // The length of the item's use animation in ticks (60 ticks == 1 second.)
            item.useStyle = ItemUseStyleID.SwingThrow; // how you use the item (swinging, holding out, etc)
            item.knockBack = 4; // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
            item.value = 750000; // how much the item sells for (measured in copper)
            item.rare = 4; // the color that the item's name will be in-game
            item.shoot = ModContent.ProjectileType<Projectiles.OrviusProj>(); //idk why but all the guns in the vanilla source have this
            item.shootSpeed = 19f; // the speed of the projectile (measured in pixels per frame)
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.AdamantiteBar,8);
            recipe.AddIngredient(mod.ItemType("Kuva"),3);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.TitaniumBar,8);
            recipe.AddIngredient(mod.ItemType("Kuva"),3);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        int proj = 0;
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (Main.projectile[proj].type == type && Main.projectile[proj].owner == Main.LocalPlayer.cHead && Main.projectile[proj].active)
                Main.projectile[proj].modProjectile.OnHitPvp(Main.LocalPlayer, 0, false);
            else
            {
                proj = Projectile.NewProjectile(position, new Vector2(speedX, speedY), type, damage, knockBack, Main.LocalPlayer.cHead);
                Main.projectile[proj].GetGlobalProjectile<Projectiles.wdfeerGlobalProj>().critMult = 1.1f;
                Main.projectile[proj].GetGlobalProjectile<Projectiles.wdfeerGlobalProj>().slashChance = 20;
                Main.PlaySound(SoundID.Item1, position);
            }

            return false;
        }
    }
}