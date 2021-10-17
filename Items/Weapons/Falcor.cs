using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace wfMod.Items.Weapons
{
    public class Falcor : wfWeapon
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Can explode mid-flight applying Electricity procs\n 36% Slash chance on Hit");
        }
        public override void SetDefaults()
        {
            item.damage = 166; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
            item.crit = 10;
            item.melee = true; // sets the damage type to ranged
            item.noMelee = true;
            item.noUseGraphic = true;
            item.width = 32; // hitbox width of the item
            item.height = 32; // hitbox height of the item
            item.useTime = 12; // The item's use time in ticks (60 ticks == 1 second.)
            item.useAnimation = 12; // The length of the item's use animation in ticks (60 ticks == 1 second.)
            item.useStyle = ItemUseStyleID.SwingThrow; // how you use the item (swinging, holding out, etc)
            item.knockBack = 4; // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
            item.value = 750000; // how much the item sells for (measured in copper)
            item.rare = 8; // the color that the item's name will be in-game
            item.shoot = ModContent.ProjectileType<Projectiles.FalcorProj>(); //idk why but all the guns in the vanilla source have this
            item.shootSpeed = 18f; // the speed of the projectile (measured in pixels per frame)
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);    
            recipe.AddIngredient(mod.ItemType("Fieldron"));
            recipe.AddIngredient(ItemID.ChlorophyteBar, 8);
            
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        Projectile proj;
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (proj != null && proj.active && proj.modProjectile is Projectiles.FalcorProj)
            {
                var gProj = proj.GetGlobalProjectile<Projectiles.wfGlobalProj>();
                gProj.AddProcChance(new ProcChance(BuffID.Electrified, 100));
                if (gProj.procChances.ContainsKey(mod.BuffType("SlashProc")))
                    gProj.procChances[mod.BuffType("SlashProc")].chance = 0;
                gProj.Explode(320);
                proj.idStaticNPCHitCooldown = 4;
            }
            else
            {
                proj = ShootWith(position, speedX, speedY, type, damage, knockBack);
                proj.GetGlobalProjectile<Projectiles.wfGlobalProj>().AddProcChance(new ProcChance(mod.BuffType("SlashProc"), 36));
                Main.PlaySound(SoundID.Item1, position);
            }

            return false;
        }
    }
}