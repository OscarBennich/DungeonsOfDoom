using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DungeonsOfDoom.Weapons;
using DungeonsOfDoom.Monsters;

namespace DungeonsOfDoom
{
    abstract class Item
    {
        public string Name { get; }

        protected Item(string name)
        {
            Name = name;
        }

        public void PickUpItem(Player playerCharacter)
        {
            playerCharacter.Backpack.Add(this);
        }

        public abstract void UseItem(Player player);
    }
}
