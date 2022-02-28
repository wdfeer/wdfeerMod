using Terraria;
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
            item.damage = 29;
            item.crit = 14;
            item.magic = true;
            item.mana = 7;
            item.width = 32;
            item.height = 20;
            item.useTime = 13;
            item.useAnimation = 13;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.noMelee = true;
            item.knockBack = 0;
            item.value = Item.buyPrice(gold: 3);
            item.rare = 3;
            item.autoReuse = false;
            item.shoot = ProjectileID.MagnetSphereBolt;
            item.shootSpeed = 16f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.SpaceGun, 1);
            recipe.AddIngredient(ItemID.Handgun, 1);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        public override bool CanUseItem(Player player)
        {
            var stacks = player.GetModPlayer<wfPlayer>().arcaSciscoStacks;
            item.crit = 14 + 5 * stacks;
            return base.CanUseItem(player);
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            PlaySound(Main.rand.NextFloat(-0.05f, 0.1f), 0.5f);

            var proj = ShootWith(position, speedX, speedY, type, damage, knockBack, offset: item.width + 1);
            var globalProj = proj.GetGlobalProjectile<Projectiles.wfGlobalProj>();
            globalProj.onHit = (NPC target) =>
            {
                Main.player[proj.owner].AddBuff(mod.BuffType("ArcaSciscoBuff"), 180);
                Main.player[proj.owner].GetModPlayer<wfPlayer>().arcaSciscoStacks++;
            };
            globalProj.AddProcChance(new ProcChance(mod.BuffType("SlashProc"), 5 * player.GetModPlayer<wfPlayer>().arcaSciscoStacks + 13));

            return false;
        }
    }
}