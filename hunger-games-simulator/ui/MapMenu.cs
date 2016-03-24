using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using hunger_games_simulator.core;
using hunger_games_simulator.level;
using ConsoleBufferApi;

namespace hunger_games_simulator.ui
{
    class MapMenu
    {
        public static void Show(GameServer server)
        {
            server.Arena = ArenaGenerator.Generate(server.GameAssets, 0, 30);
            ConsoleBuffer buf = server.Arena.MapBuffer();
            buf.DrawSelf();
            Console.ReadLine();
        }
    }
}
