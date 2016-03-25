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
            buffer = new ConsoleBuffer();

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


            buffer.SetCursorPosition(35, buffer.CursorTop + 1);
            int selected = this.ReadMenu();
            
            if (selected == 0)
            {
                GameServer server = new GameServer();
                server.LoadAssets();
                new MapPreviewMenu().Show(server);
                Show();
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
