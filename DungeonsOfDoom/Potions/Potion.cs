using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonsOfDoom.Potions
{
    abstract class Potion : Item
    {
        public int HealthValue { get; }

        protected Potion(string name, int healthValue, Rarity rarity) : base(name, rarity)
        {
            HealthValue = healthValue;
        }

        public override void UseItem(Player playerCharacter)
        {
            playerCharacter.CurrentHealth = Math.Min(playerCharacter.CurrentHealth + HealthValue, playerCharacter.MaxHealth);
            playerCharacter.Backpack.Remove(this);
        }
    }
}
