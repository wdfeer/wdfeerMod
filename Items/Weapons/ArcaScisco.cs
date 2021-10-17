using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using Microsoft.Xna.Framework;

namespace wfMod.Items.Weapons
{
    public class ArcaScisco : wdfeerWeapon
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("+5% Critical and Slash chance on hit, stacks up to 4 times");
        }
        public override void SetDefaults()
        {
            item.damage = 29; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
            item.crit = 14;
            item.magic = true;
            item.mana = 7;
            item.width = 32; // hitbox width of the item
            item.height = 20; // hitbox height of the item
            item.useTime = 13; // The item's use time in ticks (60 ticks == 1 second.)
            item.useAnimation = 13; // The length of the item's use animation in ticks (60 ticks == 1 second.)
            item.useStyle = ItemUseStyleID.HoldingOut; // how you use the item (swinging, holding out, etc)
            item.noMelee = true; //so the item's animation doesn't do damage
            item.knockBack = 0; // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
            item.value = Item.buyPrice(gold: 3); // how much the item sells for (measured in copper)
            item.rare = 3; // the color that the item's name will be in-game
            item.autoReuse = false; // if you can hold click to automatically use it again
            item.shoot = ProjectileID.MagnetSphereBolt;
            item.shootSpeed = 16f; // the speed of the projectile (measured in pixels per frame)
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
            var stacks = player.GetModPlayer<wdfeerPlayer>().arcaSciscoStacks;
            item.crit = 14 + 5 * stacks;
            return base.CanUseItem(player);
        }
        Microsoft.Xna.Framework.Audio.SoundEffectInstance sound;
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            var proj = ShootWith(position, speedX, speedY, type, damage, knockBack, offset: item.width + 1);
            var globalProj = proj.GetGlobalProjectile<Projectiles.wdfeerGlobalProj>();
            globalProj.onHit = (NPC target) =>
            {
                Main.player[proj.owner].AddBuff(mod.BuffType("ArcaSciscoBuff"), 180);
                Main.player[proj.owner].GetModPlayer<wdfeerPlayer>().arcaSciscoStacks++;
            };
            globalProj.AddProcChance(new ProcChance(mod.BuffType("SlashProc"), 5 * player.GetModPlayer<wdfeerPlayer>().arcaSciscoStacks + 13));

            sound = mod.GetSound("Sounds/ArcaSciscoSound").CreateInstance();
            sound.Volume = 0.5f;
            sound.Pitch += Main.rand.NextFloat(-0.05f, 0.1f);
            sound.Play();

            return false;
        }
    }
}