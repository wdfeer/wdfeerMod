using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace wfMod.Items.Weapons
{
    public class Fulmin : wfWeapon
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("+20% Critical Damage\n16% Electricity proc chance");
        }
        public override void SetDefaults()
        {
            item.damage = 27;
            item.crit = 26;
            item.ranged = true;
            item.width = 48;
            item.height = 15;
            item.useTime = 28;
            item.useAnimation = 28;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.noMelee = true;
            item.knockBack = 8;
            item.value = 15000;
            item.rare = 3;
            item.autoReuse = true;
            item.shoot = ModContent.ProjectileType<Projectiles.FulminProj>();
            item.shootSpeed = 36f;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.IllegalGunParts);
            recipe.AddIngredient(ItemID.Feather, 8);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        Microsoft.Xna.Framework.Audio.SoundEffectInstance sound;
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            sound = mod.GetSound("Sounds/FulminSound").CreateInstance();
            sound.Volume = 0.5f;
            sound.Pitch += Main.rand.NextFloat(0,0.15f);
            Main.PlaySoundInstance(sound);

            var projectile = ShootWith(position, speedX, speedY, type, damage, knockBack, offset: item.width - 2);
            var globalProj = projectile.GetGlobalProjectile<Projectiles.wfGlobalProj>();
            globalProj.critMult = 1.2f;
            globalProj.AddProcChance(new ProcChance(BuffID.Electrified, 16));

            return false;
        }
    }
}