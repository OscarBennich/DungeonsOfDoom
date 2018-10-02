using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsOfDoom.Monsters
{
    abstract class Monster : Character, IPickupAble, ISpawnable
    {
        public Rarity Rarity { get; }

        public string Name { get; }

        protected Monster(int currentHealth, int attackDamage, int armorClass, string name, Rarity rarity) : base(currentHealth, attackDamage, armorClass, name)
        {
            Rarity = rarity;
            Name = name; 
        }

        public abstract string GetShortName();

        public void PickUp(Player player)
        {
            player.Backpack.Add(this);
        }

        public void UseItem(Player playerCharacter)
        {
            playerCharacter.CurrentHealth = Math.Min(playerCharacter.CurrentHealth + Convert.ToInt32(Math.Ceiling((double)MaxHealth/10)), playerCharacter.MaxHealth);
            playerCharacter.Backpack.Remove(this);
        }
    }
}
