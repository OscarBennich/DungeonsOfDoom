using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonsOfDoom
{
    abstract class Character
    {   
        public int Health { get; set; }

        public int AttackDamage { get; set; }

        public string Name { get; }

        protected Character(int health, int attackDamage, string name)
        {
            Health = health;
            AttackDamage = attackDamage;
            Name = name;
        }

        public void Attack(Character charToAttack)
        {
            charToAttack.Health -= AttackDamage;
        }
    }
}
