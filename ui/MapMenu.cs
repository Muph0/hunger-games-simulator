using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Muphcode.Xna2D;
using System.Drawing;

namespace HungerGames.ui
{
    public class MapMenu
    {
        static Color foreclr = Color.LightGray;
        static Color backclr = Color.FromArgb(10, 10, 20);
        static Color seleclr = Color.FromArgb(10, 140, 10);

        public static void Show()
        {
            Console2D console = Program.console;
            console.BackColor = backclr;
            console.Clear();

            console.ForeColor = Color.White;
            console.WriteLine("\n Prosím, vyberte arénu ve které dojde k souboji.");
            console.ForeColor = backclr;
            console.BackColor = Color.White;
            console.WriteLine("█" + string.Join((char)295 + "", new string[48]));
            console.ForeColor = foreclr;
            console.BackColor = backclr;

            console.CursorPos.X++;

            string[] contents = new string[]
            {
                "Vytvořit novou arénu",
                ">--------------------",
                "",
                "",
                "Zrušit",
            };
            if (true)
            {
                List<string> list_contents = contents.ToList();
                for (int i = 0; i < 5; i++)
                {
                    list_contents.Insert(3, "<aréna " + (5-i) + ">");
                }
                contents = list_contents.ToArray();
            }

            int selected = 0;
            Point pos = console.CursorPos;
            console.CursorVisible = false;
            while (true)
            {
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

            if (selected > 2 && selected < contents.Length - 2)
            {
                Program.server_options.Arena.Name = contents[selected];
            }
        }
    }
}
