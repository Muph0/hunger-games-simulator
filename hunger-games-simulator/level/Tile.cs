using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace hunger_games_simulator.level
{
    class Tile
    {
        public char Char;
        public ConsoleColor Foreground, Background;
        public List<Entity> Entities;
    }
}
