using System;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace wfMod.Items.Weapons
{
    internal class SecuraPenta : wfWeapon
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Launches grenades that stick to surfaces and have to be detonated manually\nRight Click to trigger the explosions\nDirect hits deal 40% of the damage\nOnly five grenades can be active at a time");
        }
        public override void SetDefaults()
        {
            item.damage = 66;
            item.crit = 22;
            item.knockBack = 9.2f;
            item.ranged = true;
            item.noMelee = true;
            item.width = 46;
            item.height = 16;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.useTime = 32;
            item.useAnimation = 32;
            item.rare = 6;
            item.value = Item.buyPrice(gold: 4);
            item.shoot = mod.ProjectileType("PentaProj");
            item.shootSpeed = 17f;
            item.useAmmo = ItemID.Grenade;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse != 2 && nextProjIndex != -1)
                return base.CanUseItem(player);
            else if (player.altFunctionUse == 2 && projs.Any<Projectile>(X => X.active && X.type == item.shoot))
            {
                for (int i = 0; i < projs.Length; i++)
                {
                    if (!projs[i].active || projs[i].type != item.shoot) continue;
                    projs[i].GetGlobalProjectile<Projectiles.wfGlobalProj>().Explode(150);
                    projs[i].localNPCHitCooldown = -1;
                }
                projs = new Projectile[5] { new Projectile(), new Projectile(), new Projectile(), new Projectile(), new Projectile() };

                return false;
            }
            else return false;
        }
        int nextProjIndex
        {
            get
            {
                int n = Array.FindIndex<Projectile>(projs, x => !x.active || x.type != item.shoot);
                if (n >= 0 && n < 5)
                {
                    projs[n] = new Projectile();
                    return n;
                }
                else
                    return -1;
            }
        }
        Projectile[] projs = new Projectile[5] { new Projectile(), new Projectile(), new Projectile(), new Projectile(), new Projectile() };
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            var proj = ShootWith(position, speedX, speedY, mod.ProjectileType("PentaProj"), damage, knockBack, offset: 16);
            projs[nextProjIndex] = proj;
            var gProj = proj.GetGlobalProjectile<Projectiles.wfGlobalProj>();
            if (player.GetModPlayer<wfPlayer>().napalmGrenades)
                gProj.AddProcChance(new ProcChance(BuffID.OnFire, 30));

            if (player.GetModPlayer<wfPlayer>().napalmGrenades)
                sound = mod.GetSound("Sounds/PentaNapalmSound").CreateInstance();
            else sound = mod.GetSound("Sounds/PentaSound").CreateInstance();
            sound.Volume = 0.5f;
            sound.Pitch += Main.rand.NextFloat(0f, 0.1f);
            sound.Play();

            return false;
        }
    }
}