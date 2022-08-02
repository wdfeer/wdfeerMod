using Terraria;
using Terraria.DataStructures;
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
            Item.damage = 25;
            Item.crit = 6;
            Item.mana = 5;
            Item.DamageType = DamageClass.Magic;
            Item.width = 41;
            Item.height = 14;
            Item.useTime = 9;
            Item.useAnimation = 9;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 3;
            Item.value = Item.buyPrice(gold: 40);
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

            var proj = ShootWith(position, speedX * 3f, speedY * 3f, ProjectileID.LaserMachinegunLaser, damage, knockBack, 0.025f, Item.width);
            var gProj = proj.GetGlobalProjectile<wfGlobalProj>();
            gProj.AddProcChance(new ProcChance(BuffID.Weak, 20));
            return false;
        }
    }
}