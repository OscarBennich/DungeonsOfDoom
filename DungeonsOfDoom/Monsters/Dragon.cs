using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonsOfDoom.Monsters
{
    class Dragon : Monster
    {   
        public Dragon() : base(50, 20, 5, "Dragon") { }

        public override string GetShortName()
        {
            return "D";
        }
    }
}
