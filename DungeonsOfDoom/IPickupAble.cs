using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonsOfDoom
{
    interface IPickupAble
    {
        string Name { get; }

        void PickUp(Player player);

        void UseItem(Player player);
    }
}
