using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsOfDoom.Monsters
{
    abstract class Monster : Character
    {
        protected Monster(int CurrentHealth, int attackDamage, string name) : base(CurrentHealth, attackDamage, name) { }
    }
}
