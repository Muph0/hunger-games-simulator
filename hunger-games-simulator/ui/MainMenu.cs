using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsoleBufferApi;
using hunger_games_simulator.core;
using System.Net;

namespace hunger_games_simulator.ui
{
    class MainMenu : Menu
    {
        public MainMenu()
            : base(new string[] { "Host game", "Join game", "!", "Settings", "Exit" })
        {

        }

        public void Show()
        {
            GameClient client = new GameClient();

            while (true)
            {
                buffer = new ConsoleBuffer();

                ConsoleColor logo1 = ConsoleColor.Red, logo2 = ConsoleColor.DarkCyan, black = ConsoleColor.Black;

                buffer.Clear();
                buffer.ForegroundColor = logo1;
                buffer.Write("\n" +
    "              ██░ ██  █    ██  ███▄    █   ▄████ ▓█████  ██▀███  \n" +
    "             ▓██░ ██▒ ██  ▓██▒ ██ ▀█   █  ██▒ ▀█▒▓█   ▀ ▓██ ▒ ██▒\n" +
    "             ▒██▀▀██░▓██  ▒██░▓██  ▀█ ██▒▒██░▄▄▄░▒███   ▓██ ░▄█ ▒\n" +
    "             ░▓█ ░██ ▓▓█  ░██░▓██▒  ▐▌██▒░▓█  ██▓▒▓█  ▄ ▒██▀▀█▄  \n" +
    "             ░▓█▒░██▓▒▒█████▓ ▒██ ░  ▓██░░▒▓███▀▒░▒████▒░██▓ ▒██▒\n" +
    "              ▒ ░░▒░▒░▒▓▒ ▒ ▒ ░ ▒ ░  ▒ ▒  ░▒   ▒ ░░ ▒░ ░░ ▒▓ ░▒▓░\n" +
    "              ▒ ░▒░ ░░░▒░ ░░░ ░ ░ ▒  ░ ▒░  ░   ░  ░ ░  ░  ░▒ ░ ▒░\n" +
    "              ░  ░░ ░ ░░░ ░ ░    ░░  ░ ░ ░ ░   ░    ░     ░░   ░ \n" +
    "              ░  ░  ░   ░              ░       ░    ░  ░   ░     \n"
                );

                if (true)
                {
                    buffer.SetCursorPosition(0, buffer.CursorTop - 3);
                    string online =
        "                ██████╗ ███╗   ██╗██╗     ██╗███╗   ██╗███████╗\n" +
        "               ██╔═══██╗████╗  ██║██║     ██║████╗  ██║██╔════╝\n" +
        "               ██║   ██║██╔██╗ ██║██║     ██║██╔██╗ ██║█████╗  \n" +
        "               ██║   ██║██║╚██╗██║██║     ██║██║╚██╗██║██╔══╝  \n" +
        "               ╚██████╔╝██║ ╚████║███████╗██║██║ ╚████║███████╗\n" +
        "                ╚═════╝ ╚═╝  ╚═══╝╚══════╝╚═╝╚═╝   ╚══╝╚══════╝\n"
                    ;

                    int X = buffer.CursorLeft, Y = buffer.CursorTop;
                    for (int i = 0; i < online.Length; i++)
                    {
                        int x = i % (online.Length / 6) + X, y = i / (online.Length / 6) + Y;
                        ConsoleBuffer.CharInfo ci = buffer[x, y];
                        char overchar = ' ';
                        if (ci.Char.AsciiChar == ConsoleBuffer.ASCII.IndexOf('░'))
                            overchar = '▒';
                        if (ci.Char.AsciiChar == ConsoleBuffer.ASCII.IndexOf('▒'))
                            overchar = '▓';


                        char online_char = online[i];
                        if (online_char == '\n')
                            online_char = ' ';

                        if (online_char == '█')
                        {
                            buffer.DrawText(overchar.ToString(), x, y, logo1, logo2);
                        }
                        else if (online_char == ' ')
                        {
                            buffer.DrawText(" ", x, y, logo2, black);
                        }
                        else
                        {
                            buffer.DrawText(online_char.ToString(), x, y, logo2, overchar != ' ' ? black : black);
                        }
                    }
                }


                buffer.SetCursorPosition(35, buffer.CursorTop + 7);
                int selected = this.ReadMenu();

                if (selected == 0)
                {
                    GameServer server = new GameServer();
                    server.LoadAssets();
                    new MapPreviewMenu().Show(server, client);
                }
                if (selected == 1)
                {
                    string result = "";
                    IPAddress ip = null;
                    int port = GameServer.DEFAULT_PORT;
                    InputBox.Show("Enter IP address", ref result);

                    if (result.Contains(':'))
                    {
                        if (int.TryParse(result.Split(':')[1], out port))
                            result = result.Split(':')[0];
                        else
                        {

                        }
                    }

                    if (IPAddress.TryParse(result, out ip))
                    {
                        IPEndPoint ep = new IPEndPoint(ip, port);
                        new ConnectingMenu().Show(client, ep);
                    }
                    else
                    {

                    }
                }
                if (selected == 3)
                {
                    new SettingsMenu().Show(client);
                }
                if (selected == Items.Length - 1)
                {
                    ConsoleBuffer.Fullscreen = false;
                    return;
                }
            }
        }
    }
}
