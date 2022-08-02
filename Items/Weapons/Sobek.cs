using Terraria;
using Terraria.DataStructures;
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
            Item.damage = 5;
            Item.crit = 7;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 42;
            Item.height = 14;
            Item.scale = 1.2f;
            Item.useTime = 24;
            Item.useAnimation = 24;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 3;
            Item.value = Item.sellPrice(silver: 80);
            Item.rare = 3;
            Item.autoReuse = true;
            Item.shoot = 10;
            Item.shootSpeed = 16f;
            Item.useAmmo = AmmoID.Bullet; // The "ammo Id" of the ammo item that this weapon uses. Note that this is not an item Id, but just a magic value.
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(6, 0);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            PlaySound(Main.rand.NextFloat(0.3f, 0.6f), 0.69f);
            for (int i = 0; i < 4; i++)
            {
                var proj = ShootWith(position, speedX, speedY, type, damage, knockBack, 0.09f, Item.width - 6);
            }
            return false;
        }
    }
}