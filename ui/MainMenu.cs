using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Muphcode.Xna2D;
using System.Drawing;

namespace HungerGames.ui
{
    class MainMenu
    {
        static Color foreclr = Color.LightGray;
        static Color backclr = Color.FromArgb(10, 10, 20);
        static Color seleclr = Color.FromArgb(140, 10, 0);

        public static void Show(Console2D console)
        {
            console.BackColor = backclr;
            console.ForeColor = Color.FromArgb(182, 11, 11);
            console.Clear();
            console.Write("\n" +
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
            console.ForeColor = Color.FromArgb(160, 194, 240);
            console.Write(
"                ██████╗ ███╗   ██╗██╗██╗     ███╗   ██╗███████╗\n" +
"               ██╔═══██╗████╗  ██║██║██║     ████╗  ██║██╔════╝\n" +
"               ██║   ██║██╔██╗ ██║██║██║     ██╔██╗ ██║█████╗  \n" +
"               ██║   ██║██║╚██╗██║██║██║     ██║╚██╗██║██╔══╝  \n" +
"               ╚██████╔╝██║ ╚████║██║███████╗██║ ╚████║███████╗\n" +
"                ╚═════╝ ╚═╝  ╚═══╝╚═╝╚══════╝╚═╝  ╚═══╝╚══════╝\n"
            );
            console.CursorPos.X = 40 - "=====================".Length / 2;
            string[] contents = new string[]
            {
                "Hostovat hru",
                "Připojit se ke hře",
                "",
                "Sestavy charakterů",
                "Nastavení",
                "",
                "Konec",
            };
            #region while menu
            Point pos = console.CursorPos;
            int selected = 0;
            console.ForeColor = foreclr;

            console.CursorVisible = false;
            while (true)
            {
                for (int i = 0; i < contents.Length; i++)
                {
                    console.CursorPos = new Point(pos.X, pos.Y + i);

                    if (i == selected)
                    {
                        console.ForeColor = seleclr;
                        console.Write("► ");
                        console.ForeColor = foreclr;
                        console.BackColor = seleclr;
                    }
                    else
                    {
                        console.Write("  ");
                    }

                    if (contents[i].ToString().Length > 0 && contents[i].ToString().First() == '>')
                        console.Write(contents[i].ToString().Substring(1));
                    else
                        console.Write(contents[i]);

                    console.BackColor = backclr;
                    //console.Write(" ");
                }

                console.ClearKeyEvents();
                char key = console.Read(true);

                int old_selected = selected;

                try
                {
                    int move = 0;
                    if (key == '↑')
                    {
                        move--;
                    }
                    if (key == '↓')
                    {
                        move++;
                    }
                    if (key == '\r')
                    {
                        break;
                    }

                    do
                    {
                        selected += move;
                    } while (contents[selected].ToString().Length == 0 || contents[selected].ToString().First() == '>');
                }
                catch
                {
                    selected = old_selected;
                }
            }
            #endregion
            console.CursorVisible = true;

            if (selected == 0)
            {
                ServersetupMenu.Show(true);
            }
            if (selected != 6)
            {
                Show(console);
            }
        }
    }
}
