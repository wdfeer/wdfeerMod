using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;
using wfMod.Projectiles;
using Terraria.ModLoader.IO;

namespace wfMod.Items.Weapons
{
    public class PrismaTetra : wfWeapon
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("20% to inflict Weak");
        }
        public override void SetDefaults()
        {
            pathToSound = "Sounds/TenetTetraPrimarySound";
            item.damage = 25;
            item.crit = 6;
            item.mana = 5;
            item.magic = true;
            item.width = 41;
            item.height = 14;
            item.useTime = 9;
            item.useAnimation = 9;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.noMelee = true;
            item.knockBack = 3;
            item.value = Item.buyPrice(gold: 40);
            item.rare = 3;
            item.autoReuse = true;
            item.shoot = 10;
            item.shootSpeed = 16f;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(2, 0);
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            float pitch = Main.rand.NextFloat(-0.1f, 0.1f);
            PlaySound(pitch);

            var proj = ShootWith(position, speedX * 3f, speedY * 3f, ProjectileID.LaserMachinegunLaser, damage, knockBack, 0.025f, item.width);
            var gProj = proj.GetGlobalProjectile<wfGlobalProj>();
            gProj.AddProcChance(new ProcChance(BuffID.Weak, 20));
            return false;
        }
    }
}