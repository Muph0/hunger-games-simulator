using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HungerGames.world
{
    class ArenaGenerator
    {
        public static Arena Generate(int width, int height, int biome_count, int seed)
        {
            Random rnd = new Random(seed);
            Arena result = new Arena(width, height);

            return result;
        }
    }
}
