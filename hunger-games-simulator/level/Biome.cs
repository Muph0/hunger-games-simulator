using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using hunger_games_simulator.assets;

namespace hunger_games_simulator.level
{
    [Serializable]
    class Biome
    {
        public Biome(int pivX, int pivY)
        {

        }

        public string Asset;
        public Point Pivot;
        public int[] Tiles;

        [NonSerialized] public Arena Arena;
    }
}
