﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsOfDoom
{
    abstract class Monster : Character
    {
        protected Monster(int health, int attackDamage, string name) : base(health, attackDamage, name) { }
    }
}