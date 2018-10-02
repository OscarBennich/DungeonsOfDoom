using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DungeonsOfDoom.Weapons;
using DungeonsOfDoom.Monsters;

namespace DungeonsOfDoom
{
    abstract class Item : IPickupAble, ISpawnable
    {
        public Rarity Rarity { get; }

        public string Name { get; }

        protected Item(string name, Rarity rarity)
        {
            Name = name;
            Rarity = rarity;
        }

        public void PickUp(Player player)
        {
            player.Backpack.Add(this);
        }

        public abstract void UseItem(Player playerCharacter);
    }
}
