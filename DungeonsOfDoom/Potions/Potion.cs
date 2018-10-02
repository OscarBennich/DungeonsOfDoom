using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonsOfDoom.Potions
{
    abstract class Potion : Item
    {
        public int HealthValue { get; }

        protected Potion(string name, int healthValue) : base(name)
        {
            HealthValue = healthValue;
        }

        public override void UseItem(Player playerCharacter)
        {
            if (playerCharacter.CurrentHealth + HealthValue > playerCharacter.MaxHealth)
            {
                playerCharacter.CurrentHealth = playerCharacter.MaxHealth;
                playerCharacter.Backpack.Remove(this);
            }
            else
            {
                playerCharacter.CurrentHealth += HealthValue;
                playerCharacter.Backpack.Remove(this);
            }
        }
    }
}
