using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonsOfDoom.Weapons
{
    abstract class Weapon : Item
    {
        public int WeaponDamage { get; set; }

        protected Weapon(string name, int weaponDamage) : base(name)
        {
            WeaponDamage = weaponDamage;
        }

        public override void UseItem(Player playercharacter)
        {   
            playercharacter.Backpack.Add(playercharacter.Weapon);
            playercharacter.Backpack.Remove(this);
            playercharacter.Weapon = this;
        }
    }
}
