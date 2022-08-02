using Terraria;
using Terraria.DataStructures;
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
            Item.damage = 21;
            Item.crit = 0;
            Item.mana = 6;
            Item.DamageType = DamageClass.Magic;
            Item.width = 40;
            Item.height = 14;
            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 3;
            Item.value = Item.sellPrice(gold: 1);
            Item.rare = 3;
            Item.autoReuse = true;
            Item.shoot = 10;
            Item.shootSpeed = 16f;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(2, 0);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float pitch = Main.rand.NextFloat(-0.1f, 0.1f);
            PlaySound(pitch);

            var proj = ShootWith(position, speedX * 2.5f, speedY * 2.5f, ProjectileID.LaserMachinegunLaser, damage, knockBack, 0.04f, Item.width);

            return false;
        }
    }
}