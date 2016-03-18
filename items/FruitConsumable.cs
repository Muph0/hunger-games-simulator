using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HungerGames.items
{
    class FruitConsumable : ConsumableItem
    {
        public override void OnConsume()
        {
            Program.player.Hunger += Damage;
        }
    }
}
