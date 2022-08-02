using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace wfMod.Items.Weapons
{
    public class Simulor : wfWeapon
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Launches orbs that can't hit enemies directly and bounce off of tiles,\n will attract to each other and merge to create an implosion,\n increasing their damage by 20% up to a maximum of 300%\nImplosions are guaranteed to electrify enemies\nRight Click to force all active orbs to explode");
        }
        public override void SetDefaults()
        {
            Item.damage = 42;
            Item.crit = 8;
            Item.DamageType = DamageClass.Magic;
            Item.mana = 10;
            Item.width = 36;
            Item.height = 15;
            Item.useTime = 24;
            Item.useAnimation = 24;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 6.9f;
            Item.value = Item.buyPrice(gold: 33);
            Item.rare = 6;
            Item.autoReuse = false;
            Item.shoot = ModContent.ProjectileType<Projectiles.SimulorProj>();
            Item.shootSpeed = 16f;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        List<Projectiles.SimulorProj> projs = new List<Projectiles.SimulorProj>();
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse != 2)
                return base.CanUseItem(player);
            foreach (Projectiles.SimulorProj proj in projs)
            {
                proj.Explode();
            }
            projs = new List<Projectiles.SimulorProj>();
            return false;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            sound = Mod.GetSound("Sounds/SynoidSimulorSound").CreateInstance();
            sound.Pitch += Main.rand.NextFloat(-0.08f, 0.08f);
            Main.PlaySoundInstance(sound);

            var proj = ShootWith(position, speedX, speedY, type, damage, knockBack, offset: Item.width + 2);
            projs.Add(proj.ModProjectile as Projectiles.SimulorProj);
            return false;
        }
    }
}