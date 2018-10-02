using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DungeonsOfDoom.Weapons;
using DungeonsOfDoom.Armors;

namespace DungeonsOfDoom
{
    class Player : Character
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Weapon Weapon { get; set; }

        public Armor Armor { get; set; }


        public List<IPickupAble> Backpack { get; }

        public Player(int maxHealth, Weapon weapon, Armor armor, string name, int x, int y) : base(maxHealth, weapon.WeaponDamage, armor.ArmorClass, name)
        {
            X = x;
            Y = y;
            EquipWeapon(weapon);
            EquipArmor(armor);
            Backpack = new List<IPickupAble>();
        }

        public void EquipWeapon(Weapon weapon)
        {
            Weapon = weapon;
            AttackDamage = Weapon.WeaponDamage;
        }

        public void EquipArmor(Armor armor)
        {
            Armor = armor; 
            AttackDamage = Weapon.WeaponDamage;
        }
    }
}
