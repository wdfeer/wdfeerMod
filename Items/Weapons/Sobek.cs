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
            Tooltip.SetDefault("A slow automatic shotgun");
        }
        public override void SetDefaults()
        {
            pathToSound = "Sounds/BoarPrimeSound";
            item.damage = 7; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
            item.crit = 7;
            item.ranged = true; // sets the damage type to ranged
            item.width = 42; // hitbox width of the item
            item.height = 14; // hitbox height of the item
            item.scale = 1.2f;
            item.useTime = 24; // The item's use time in ticks (60 ticks == 1 second.)
            item.useAnimation = 24; // The length of the item's use animation in ticks (60 ticks == 1 second.)
            item.useStyle = ItemUseStyleID.HoldingOut; // how you use the item (swinging, holding out, etc)
            item.noMelee = true; //so the item's animation doesn't do damage
            item.knockBack = 3; // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
            item.value = Item.sellPrice(silver: 80); // how much the item sells for (measured in copper)
            item.rare = 3; // the color that the item's name will be in-game
            item.autoReuse = true; // if you can hold click to automatically use it again
            item.shoot = 10; //idk why but all the guns in the vanilla source have this
            item.shootSpeed = 16f; // the speed of the projectile (measured in pixels per frame)
            item.useAmmo = AmmoID.Bullet; // The "ammo Id" of the ammo item that this weapon uses. Note that this is not an item Id, but just a magic value.
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(6, 0);
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            PlaySound(Main.rand.NextFloat(0.3f, 0.6f), 0.69f);
            for (int i = 0; i < 5; i++)
            {
                var proj = ShootWith(position, speedX, speedY, type, damage, knockBack, 0.09f, item.width - 6);
            }
            return false;
        }
    }
}