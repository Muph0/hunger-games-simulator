using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using hunger_games_simulator.core;
using hunger_games_simulator.level;
using ConsoleBufferApi;

namespace hunger_games_simulator.ui
{
    class MapPreviewMenu
    {
        public static void Show(GameServer server)
        {
            for (int i = 0; true; i++)
            {
                server.Arena = ArenaGenerator.Generate(server.GameAssets, i, 30);
                ConsoleBuffer buf = new ConsoleBuffer();
                buf.InsertBuffer(server.Arena.MapBuffer(), 0, 0);
                buf.SetCursorPosition(50, 0);
                buf.WriteVertical("".PadRight(25, '▌'));
                buf.SetCursorPosition(50, 0);
                buf.Write("▌".PadRight(30, '-'));
                
                buf.DrawSelf();
                Console.ReadKey(true);
            }
        }
    }
}
