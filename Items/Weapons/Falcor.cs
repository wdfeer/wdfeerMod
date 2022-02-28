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
            item.damage = 166;
            item.crit = 10;
            item.melee = true;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.width = 32;
            item.height = 32;
            item.useTime = 12;
            item.useAnimation = 12;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.knockBack = 4;
            item.value = 750000;
            item.rare = 8;
            item.shoot = ModContent.ProjectileType<Projectiles.FalcorProj>();
            item.shootSpeed = 18f;
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