using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using Microsoft.Xna.Framework;

namespace wfMod.Items.Weapons
{
    public class ArcaScisco : wfWeapon
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("+5% Critical and Slash chance on hit, stacks up to 4 times");
        }
        public override void SetDefaults()
        {
            pathToSound = "Sounds/ArcaSciscoSound";
            Item.damage = 29;
            Item.crit = 14;
            Item.DamageType = DamageClass.Magic;
            Item.mana = 7;
            Item.width = 32;
            Item.height = 20;
            Item.useTime = 13;
            Item.useAnimation = 13;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 0;
            Item.value = Item.buyPrice(gold: 3);
            Item.rare = 3;
            Item.autoReuse = false;
            Item.shoot = ProjectileID.MagnetSphereBolt;
            Item.shootSpeed = 16f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.SpaceGun, 1);
            recipe.AddIngredient(ItemID.Handgun, 1);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
        public override bool CanUseItem(Player player)
        {
            var stacks = player.GetModPlayer<wfPlayer>().arcaSciscoStacks;
            Item.crit = 14 + 5 * stacks;
            return base.CanUseItem(player);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            PlaySound(Main.rand.NextFloat(-0.05f, 0.1f), 0.5f);

            var proj = ShootWith(position, speedX, speedY, type, damage, knockBack, offset: Item.width + 1);
            var globalProj = proj.GetGlobalProjectile<Projectiles.wfGlobalProj>();
            globalProj.onHit = (NPC target) =>
            {
                Main.player[proj.owner].AddBuff(Mod.Find<ModBuff>("ArcaSciscoBuff").Type, 180);
                Main.player[proj.owner].GetModPlayer<wfPlayer>().arcaSciscoStacks++;
            };
            globalProj.AddProcChance(new ProcChance(Mod.Find<ModBuff>("SlashProc").Type, 5 * player.GetModPlayer<wfPlayer>().arcaSciscoStacks + 13));

            return false;
        }
    }
}