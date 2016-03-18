using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Muphcode.Xna2D;

namespace HungerGames.ui
{
    class ServersetupMenu
    {
        static Color foreclr = Color.LightGray;
        static Color backclr = Color.FromArgb(10, 10, 20);
        static Color seleclr = Color.FromArgb(10, 80, 150);

        public static void Show(bool init = false)
        {
            Console2D console = Program.console;
            console.BackColor = backclr;

            if (init)
            {
                Program.server_options = new ServerOptions();
                MapMenu.Show();
            }

            console.Clear();

            console.ForeColor = seleclr;
            console.Write("\n   ██         ");
            console.ForeColor = Color.White;
            console.Write("Nastavení serveru");
            console.ForeColor = seleclr;
            console.Write("         ██\n\n");
            console.ForeColor = foreclr;


            console.CursorPos.X++;
            string[] contents = null;
            int selected = 0;
            Point pos = console.CursorPos;
            console.CursorVisible = false;
            while (true)
            {
                int wid = 40;
                string 
                    port = Program.server_options.Port.ToString(),
                    arena = Program.server_options.Arena.ToString(),
                    sestava = Program.server_options.AiSetup.ToString(),
                    nazev = Program.server_options.Nazev.ToString();
                contents = new string[]
                {
                    "Název serveru:" + string.Join(" ", new string[wid - nazev.Length - 14]) + nazev,
                    "PORT:" + string.Join(" ", new string[wid - port.Length - 5]) + port,
                    "Aréna:" + string.Join(" ", new string[wid - arena.Length - 6]) + arena,
                    "AI Sestava:" + string.Join(" ", new string[wid - sestava.Length - 11]) + sestava,
                    "",
                    "Moje postava:",
                    "",
                    "Spustit"
                };

                for (int i = 0; i < contents.Length; i++)
                {
                    console.CursorPos = new Point(pos.X, pos.Y + i);

                    if (i == selected)
                    {
                        console.ForeColor = seleclr;
                        console.Write("» ");
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
                        if (selected == 0)
                        {
                            console.BackColor = seleclr;
                            console.CursorVisible = true;

                            console.CursorPos = new Point(18, 3);
                            console.Write(string.Join(" ", new string[wid - 15]));
                            console.CursorPos = new Point(18, 3);
                            Program.server_options.Nazev = console.ReadLine(nazev, wid - 16);
                            
                            console.BackColor = backclr;
                            console.CursorVisible = false;
                            continue;
                        }
                        if (selected == 1)
                        {
                            console.BackColor = seleclr;
                            console.CursorVisible = true;

                            console.CursorPos = new Point(wid - 3, 4);
                            console.Write("     ");
                            console.CursorPos = new Point(wid - 3, 4);
                            string input = console.ReadLine(port, 5);

                            short mem = 0;
                            short.TryParse(input, out mem);
                            if (mem > 0)
                                Program.server_options.Port = mem;

                            console.BackColor = backclr;
                            console.CursorVisible = false;
                            continue;
                        }

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

            if (selected == 2)
            {
                MapMenu.Show();
                Show();
            }
        }
    }
}
