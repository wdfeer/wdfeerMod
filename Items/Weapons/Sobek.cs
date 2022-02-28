using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace wfMod.Items.Weapons
{
    public class Sobek : wfWeapon
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("A slow automatic shotgun, shoots 4 pellets");
        }
        public override void SetDefaults()
        {
            pathToSound = "Sounds/BoarPrimeSound";
            item.damage = 5;
            item.crit = 7;
            item.ranged = true;
            item.width = 42;
            item.height = 14;
            item.scale = 1.2f;
            item.useTime = 24;
            item.useAnimation = 24;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.noMelee = true;
            item.knockBack = 3;
            item.value = Item.sellPrice(silver: 80);
            item.rare = 3;
            item.autoReuse = true;
            item.shoot = 10;
            item.shootSpeed = 16f;
            item.useAmmo = AmmoID.Bullet; // The "ammo Id" of the ammo item that this weapon uses. Note that this is not an item Id, but just a magic value.
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(6, 0);
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            PlaySound(Main.rand.NextFloat(0.3f, 0.6f), 0.69f);
            for (int i = 0; i < 4; i++)
            {
                var proj = ShootWith(position, speedX, speedY, type, damage, knockBack, 0.09f, item.width - 6);
            }
            return false;
        }
    }
}