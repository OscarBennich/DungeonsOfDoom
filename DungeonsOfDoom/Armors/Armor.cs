using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonsOfDoom.Armors
{
    abstract class Armor : Item
    {
        public int ArmorClass { get; }

        protected Armor(string name, int armorClass, Rarity rarity) : base(name, rarity)
        {
            ArmorClass = armorClass;
        }

        public override void UseItem(Player playerCharacter)
        {
            playerCharacter.Backpack.Add(playerCharacter.Armor);
            playerCharacter.Backpack.Remove(this);
            playerCharacter.EquipArmor(this);
        }
    }
}
