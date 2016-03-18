using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HungerGames.world
{
    public class Arena
    {
        public Biome[] tiles;
        public string Name;

        public Arena(int width, int height)
        {
            tiles = new Biome[width * height];
            Name = "<nevybráno>";
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
