using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonsOfDoom
{
    abstract class Weapon : Item
    {
        public int WeaponDamage { get; set; }

        protected Weapon(string name, int weaponDamage) : base(name)
        {
            WeaponDamage = weaponDamage;
        }
    }
}
