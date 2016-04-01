using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using hunger_games_simulator.core;
using hunger_games_simulator.level;
using ConsoleBufferApi;
using System.Net;

namespace hunger_games_simulator.ui
{
    class MapPreviewMenu : Menu
    {
        int seed = 0;
        int biome_count = 30;
        int port = GameServer.DEFAULT_PORT;
        int max_players = 12;
        string game_name = "Hunger Online";

        Arena arena;

        static string[] proitems = new string[] { "!!Map options", "!", "Seed:", "Number of biomes:",
            "!", "!", "!", "!", "!!Server settings", "!", "Port:",
            "Max players:", "!Game name:", "!", "!", "!", "!", "!", "Start server", "Back"
        };
        public MapPreviewMenu()
            : base(proitems)
        {
            this.width = 25;
            this.SelectedColor = ConsoleColor.DarkGreen;

            seed = (int)(DateTime.Now.Ticks % 100000);
        }

        void UpdateItems()
        {
            Items[2] = proitems[2] + seed.ToString().PadLeft(width - proitems[2].Length);
            Items[3] = proitems[3] + biome_count.ToString().PadLeft(width - proitems[3].Length);
            Items[10] = proitems[10] + ((port == GameServer.DEFAULT_PORT ? "(default) " : "") + port).PadLeft(width - proitems[10].Length);
            Items[11] = proitems[11] + max_players.ToString().PadLeft(width - proitems[11].Length);
            Items[13] = game_name.PadLeft(width);
        }

        public void Show(GameServer server, GameClient client)
        {
            while (true)
            {
                arena = ArenaGenerator.Generate(server.GameAssets, seed, biome_count);
                UpdateItems();

                ConsoleBuffer mapbuf = arena.MapBuffer();
                buffer = new ConsoleBuffer();
                buffer.InsertBuffer(mapbuf, 0, 0);
                buffer.SetCursorPosition(mapbuf.Width, 0);
                buffer.WriteVertical("".PadRight(25, '▌'));

                buffer.SetCursorPosition(mapbuf.Width, 7);
                buffer.Write("█".PadRight(30, '─'));

                buffer.SetCursorPosition(mapbuf.Width + 1, 1);

                this.ReadMenu();

                if (Selected == proitems.Length - 1)
                    return;
                if (Selected == proitems.Length - 2)
                {
                    GameState gs = new GameState(arena, max_players, port, game_name);
                    server.Open(gs);

                    IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);
                    new ConnectingMenu().Show(client, ep);

                    client.Close();
                    server.Close();
                    return;
                }
            }
        }

        public override ConsoleKeyInfo Read()
        {
            ConsoleKeyInfo key = base.Read();
            bool regen = false, redraw = false;

            // Seed
            if (NumberSetting(2, key, 0, int.MaxValue, 8, ref seed))
                regen = true;

            // Biome ct
            if (NumberSetting(3, key, 1, 200, 3, ref biome_count))
                regen = true;

            // server port
            if (NumberSetting(10, key, 0, 65536, 5, ref port))
                redraw = true;

            // max players
            if (NumberSetting(11, key, 1, 100, 2, ref max_players))
                redraw = true;

            // Game name
            if (StringSetting(13, key, width, ref game_name))
                redraw = true;

            if (key.Key == ConsoleKey.Enter && Selected == proitems.Length - 1)
                regen = true;

            if (regen)
                return new ConsoleKeyInfo('\n', ConsoleKey.Enter, false, false, false);
            if (redraw)
            {
                UpdateItems();
                buffer.DrawSelf();
            }

            return key;
        }
    }
}
