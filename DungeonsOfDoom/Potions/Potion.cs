using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonsOfDoom
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
