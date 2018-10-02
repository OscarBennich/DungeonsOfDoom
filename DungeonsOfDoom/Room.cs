using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DungeonsOfDoom.Monsters;

namespace DungeonsOfDoom
{
    class Room
    {
        //public Monster Monster { get; set; }
        //public Item Item { get; set; }

        public ISpawnable Spawn { get; set; }
    }
}
