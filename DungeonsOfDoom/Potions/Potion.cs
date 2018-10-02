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
    }
}
