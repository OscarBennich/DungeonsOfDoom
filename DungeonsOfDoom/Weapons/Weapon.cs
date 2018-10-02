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

        public override void UseItem(Player playerCharacter)
        {   
            playerCharacter.Backpack.Add(playerCharacter.Weapon);
            playerCharacter.Backpack.Remove(this);
            playerCharacter.EquipWeapon(this);
        }
    }
}
