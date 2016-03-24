using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace hunger_games_simulator.ui
{
    class Menu
    {
        public string pretoken;
        public int Selected = 0;
        public bool Backround = true, Escapable = false;
        public ConsoleColor BackgroundColor, ForegroundColor, SelectedColor;

        public string[] Items;
        int x, y;

        public int SelectableItemCount
        {
            get
            {
                int total = 0;
                for (int i = 0; i < Items.Length; i++)
                {
                    if (Items[i].Length > 0 && Items[i][0] != '!')
                        total++;
                }
                return total;
            }
        }

        public string LongestItem
        {
            get
            {
                return Items.OrderByDescending(x => x.Length).ToArray()[0];
            }
        }

        public Menu(string[] items)
        {
            pretoken = "► ";
            this.Items = (string[])items.Clone();
            this.ResetColors();
        }

        public void ResetColors()
        {
            BackgroundColor = ConsoleColor.Black;
            ForegroundColor = ConsoleColor.Gray;
            SelectedColor = ConsoleColor.DarkRed;
        }

        public int ReadLine()
        {
            this.x = Console.CursorLeft;
            this.y = Console.CursorTop;

            while (true)
            {
                this.Draw();
                ConsoleKeyInfo key = this.Read();
                if (key.Key == ConsoleKey.Enter)
                    return Selected;
                if (key.Key == ConsoleKey.Escape && Escapable)
                    return -1;
            }
        }

        public ConsoleKeyInfo Read()
        {
            while (Console.KeyAvailable)
                Console.ReadKey(true);

            ConsoleKeyInfo key = Console.ReadKey(true);

            do
            {
                if (key.Key == ConsoleKey.UpArrow)
                {
                    Selected--;
                }
                if (key.Key == ConsoleKey.DownArrow)
                {
                    Selected++;
                }

                if (Selected < 0)
                    Selected = Items.Length - 1;
                if (Selected >= Items.Length)
                    Selected = 0;
            }
            while (!IsValidMenuItem(Items[Selected]));

            return key;
        }

        public void Draw()
        {
            Console.ForegroundColor = ForegroundColor;
            for (int i = 0; i < Items.Length; i++)
            {
                Console.SetCursorPosition(x, y + i);
                string item = Items[i];

                Console.ForegroundColor = ForegroundColor;
                Console.BackgroundColor = BackgroundColor;

                if (i == Selected)
                {
                    Console.ForegroundColor = SelectedColor;
                    Console.Write(pretoken);

                    Console.ForegroundColor = ForegroundColor;
                    Console.BackgroundColor = SelectedColor;
                }
                else
                {
                    Console.Write("".PadLeft(pretoken.Length));
                }

                if (!IsValidMenuItem(item))
                    item = (item.Substring(1));

                Console.Write(item);
                Console.BackgroundColor = BackgroundColor;
            }
        }

        public static bool IsValidMenuItem(string item)
        {
            return item.Length == 0 || item[0] != '!';
        }
    }
}
