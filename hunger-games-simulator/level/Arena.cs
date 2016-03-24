using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace hunger_games_simulator.level
{
    class Arena
    {
        public Tile[] Tiles;
        public Biome[] Biomes;

        public int Width { get; private set; }
        public int Height { get; private set; }

        public Arena(int width, int height)
        {
            Tiles = new Tile[width * height];
        }
    }
}
