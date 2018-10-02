using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsOfDoom
{
    class Player : Character
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Weapon Weapon { get; set; }

        public List<Item> Backpack { get; }

        public Player(int MaxHealth, Weapon weapon, string name, int x, int y) : base(MaxHealth, weapon.WeaponDamage, name)
        {
            X = x;
            Y = y;
            EquipWeapon(weapon);
            Backpack = new List<Item> {weapon};
        }

        public void EquipWeapon(Weapon weapon)
        {
            Weapon = weapon;
            this.AttackDamage = Weapon.WeaponDamage;
        }
    }
}
