using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsoleBufferApi;
using hunger_games_simulator.core;

namespace hunger_games_simulator.ui
{
    class MainMenu : Menu
    {
        public MainMenu()
            :base(new string[] { "Host game", "Join game", "!", "Settings", "Exit" })
        {

        }

        public void Show()
        {
            ConsoleBuffer buffer = new ConsoleBuffer();

            buffer.Clear();
            buffer.ForegroundColor = ConsoleColor.Red;
            buffer.Write("\n" +
"              ██░ ██  █    ██  ███▄    █   ▄████ ▓█████  ██▀███  \n" +
"             ▓██░ ██▒ ██  ▓██▒ ██ ▀█   █  ██▒ ▀█▒▓█   ▀ ▓██ ▒ ██▒\n" +
"             ▒██▀▀██░▓██  ▒██░▓██  ▀█ ██▒▒██░▄▄▄░▒███   ▓██ ░▄█ ▒\n" +
"             ░▓█ ░██ ▓▓█  ░██░▓██▒  ▐▌██▒░▓█  ██▓▒▓█  ▄ ▒██▀▀█▄  \n" +
"             ░▓█▒░██▓▒▒█████▓ ▒██░   ▓██░░▒▓███▀▒░▒████▒░██▓ ▒██▒\n" +
"              ▒ ░░▒░▒░▒▓▒ ▒ ▒ ░ ▒░   ▒ ▒  ░▒   ▒ ░░ ▒░ ░░ ▒▓ ░▒▓░\n" +
"              ▒ ░▒░ ░░░▒░ ░ ░ ░ ░░   ░ ▒░  ░   ░  ░ ░  ░  ░▒ ░ ▒░\n" +
"              ░  ░░ ░ ░░░ ░ ░    ░   ░ ░ ░ ░   ░    ░     ░░   ░ \n" +
"              ░  ░  ░   ░              ░       ░    ░  ░   ░     \n"
            );
            buffer.ForegroundColor = ConsoleColor.DarkCyan;
            buffer.Write(
"                ██████╗ ███╗   ██╗██╗     ██╗███╗   ██╗███████╗\n" +
"               ██╔═══██╗████╗  ██║██║     ██║████╗  ██║██╔════╝\n" +
"               ██║   ██║██╔██╗ ██║██║     ██║██╔██╗ ██║█████╗  \n" +
"               ██║   ██║██║╚██╗██║██║     ██║██║╚██╗██║██╔══╝  \n" +
"               ╚██████╔╝██║ ╚████║███████╗██║██║ ╚████║███████╗\n" +
"                ╚═════╝ ╚═╝  ╚═══╝╚══════╝╚═╝╚═╝   ╚══╝╚══════╝\n"
            );


            buffer.DrawSelf();

            Console.SetCursorPosition(35, buffer.CursorTop + 1);
            int selected = this.ReadLine();
            
            if (selected == 0)
            {
                GameServer server = new GameServer();
                server.LoadAssets();
                MapPreviewMenu.Show(server);
            }
            if (selected == 3)
            {
                new SettingsMenu().Show();
                Show();
            }
            if (selected == Items.Length - 1)
            {
                ConsoleBuffer.Fullscreen = false;
            }
        }
    }
}
