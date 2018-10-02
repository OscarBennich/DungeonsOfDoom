using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonsOfDoom.Monsters
{
    class Bear : Monster
    {
        public Bear() : base(25, 10, 2, "Bear") { }

        public override string GetShortName()
        {
            return "B";
        }
    }
}
