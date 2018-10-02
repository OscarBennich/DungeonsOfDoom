using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonsOfDoom
{
    abstract class Character
    {   
        public int CurrentHealth { get; set; }

        public int MaxHealth { get; set; }

        public int AttackDamage { get; set; }

        public string Name { get; }

        protected Character(int maxHealth, int attackDamage, string name)
        {
            MaxHealth = maxHealth;
            CurrentHealth = maxHealth;
            AttackDamage = attackDamage;
            Name = name;
        }

        public void Attack(Character charToAttack)
        {
            charToAttack.CurrentHealth -= AttackDamage;
        }
    }
}
