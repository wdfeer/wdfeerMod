using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
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
            Item.damage = 166;
            Item.crit = 10;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.width = 32;
            Item.height = 32;
            Item.useTime = 12;
            Item.useAnimation = 12;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 4;
            Item.value = 750000;
            Item.rare = 8;
            Item.shoot = ModContent.ProjectileType<Projectiles.FalcorProj>();
            Item.shootSpeed = 18f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();    
            recipe.AddIngredient(Mod.Find<ModItem>("Fieldron").Type);
            recipe.AddIngredient(ItemID.ChlorophyteBar, 8);
            
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }

        Projectile proj;
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (proj != null && proj.active && proj.ModProjectile is Projectiles.FalcorProj)
            {
                var gProj = proj.GetGlobalProjectile<Projectiles.wfGlobalProj>();
                gProj.AddProcChance(new ProcChance(BuffID.Electrified, 100));
                if (gProj.procChances.ContainsKey(Mod.Find<ModBuff>("SlashProc").Type))
                    gProj.procChances[Mod.Find<ModBuff>("SlashProc").Type].chance = 0;
                gProj.Explode(320);
                proj.idStaticNPCHitCooldown = 4;
            }
            else
            {
                proj = ShootWith(position, speedX, speedY, type, damage, knockBack);
                proj.GetGlobalProjectile<Projectiles.wfGlobalProj>().AddProcChance(new ProcChance(Mod.Find<ModBuff>("SlashProc").Type, 36));
                SoundEngine.PlaySound(SoundID.Item1, position);
            }

            return false;
        }
    }
}