using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonsOfDoom.Monsters
{
    class Cow : Monster
    {
        public Cow() : base(5, 2, 0, "Cow", Rarity.Common) { }

        public override string GetShortName()
        {
            return "C";
        }
    }
}
