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

        public int ArmorClass { get; set; }

        public string Name { get; }

        protected Character(int maxHealth, int attackDamage, int armorClass, string name)
        {
            MaxHealth = maxHealth;
            CurrentHealth = maxHealth;
            AttackDamage = attackDamage;
            ArmorClass = armorClass;
            Name = name;
        }

        public string Attack(Character charToAttack)
        {   
            int calculatedAttackDamage = Math.Max(0, AttackDamage - charToAttack.ArmorClass);
            charToAttack.CurrentHealth -= calculatedAttackDamage;
            return $"{Name} attacked {charToAttack.Name} for {calculatedAttackDamage} damage!"; // Should be changed to an object
        }
    }
}
