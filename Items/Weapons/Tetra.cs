using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;

namespace wfMod.Items.Weapons
{
    public class Tetra : wfWeapon
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("-25% Critical Damage");
        }
        public override void SetDefaults()
        {
            pathToSound = "Sounds/TenetTetraPrimarySound";
            item.damage = 22;
            item.crit = 0;
            item.mana = 6;
            item.magic = true;
            item.width = 40;
            item.height = 14;
            item.useTime = 10;
            item.useAnimation = 10;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.noMelee = true;
            item.knockBack = 3;
            item.value = Item.sellPrice(gold: 1);
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

            var proj = ShootWith(position, speedX * 2.5f, speedY * 2.5f, ProjectileID.LaserMachinegunLaser, damage, knockBack, 0.03f, item.width);

            return false;
        }
    }
}