using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonsOfDoom
{
    interface ISpawnable
    {
        string Name { get; }

        Rarity Rarity { get; }
    }

}
