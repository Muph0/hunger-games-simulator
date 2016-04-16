using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsoleBufferApi;

namespace hunger_games_simulator.ui
{
    class Menu
    {
        public string pretoken;
        public int Selected = 0;
        public bool fillBackround = true, Escapable = false;
        public ConsoleColor BackgroundColor, ForegroundColor, SelectedColor, HeadingColor;

        public ConsoleBuffer buffer;
        public int BufferX, BufferY;

        public string[] Items;
        protected int X, Y;
        public int width;

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
            buffer = new ConsoleBuffer();
            pretoken = "► ";
            this.Items = (string[])items.Clone();
            this.ResetColors();

            for (int i = 0; i < Items.Length; i++)
            {
                if (Items[i].First() != '!')
                {
                    Selected = i;
                    break;
                }
            }
        }

        public void ResetColors()
        {
            BackgroundColor = ConsoleColor.Black;
            ForegroundColor = ConsoleColor.Gray;
            SelectedColor = ConsoleColor.DarkRed;
            HeadingColor = ConsoleColor.White;
        }

        public bool NumberSetting(int index, ConsoleKeyInfo key, int min, int max, int length, ref int val)
        {
            int oldval = val;
            if (Selected == index)
            {
                if (key.Key == ConsoleKey.Enter)
                {
                    if (this.fillBackround)
                        buffer.BackgroundColor = this.SelectedColor;
                    buffer.SetCursorPosition(this.X + width + 2, this.Y + Selected);
                    val = buffer.ReadNumberAlignRight(length, min, val.ToString());
                    buffer.ResetColor();
                }

                if (key.Key == ConsoleKey.RightArrow)
                    val++;
                if (key.Key == ConsoleKey.LeftArrow)
                    val--;

                if (val == min - 1)
                    val = max - 1;
                if (val == max)
                    val = min;

                if (val >= max)
                    val = max - 1;
                if (val < min)
                    val = min;
            }

            return oldval != val;
        }
        public bool NumberLRSetting(int index, ConsoleKeyInfo key, int min, int max, ref int val)
        {
            int oldval = val;
            if (Selected == index)
            {
                if (key.Key == ConsoleKey.RightArrow)
                    val++;
                if (key.Key == ConsoleKey.LeftArrow)
                    val--;

                if (val == min - 1)
                    val = max - 1;
                if (val == max)
                    val = min;
            }
            return val != oldval;
        }
        public bool BoolSetting(int index, ConsoleKeyInfo key, ref bool val)
        {
            bool oldval = val;

            if (Selected == index)
            {
                if (key.Key == ConsoleKey.Enter || key.Key == ConsoleKey.LeftArrow || key.Key == ConsoleKey.RightArrow)
                    val = !val;

                return oldval != val;
            }

            return false;
        }
        public bool StringSetting(int index, ConsoleKeyInfo key, int length, ref string val)
        {
            string oldval = val;
            if (Selected == index)
            {
                if (key.Key == ConsoleKey.Enter)
                {
                    if (this.fillBackround)
                        buffer.BackgroundColor = this.SelectedColor;
                    buffer.SetCursorPosition(this.X + width + 2, this.Y + Selected);
                    //ConsoleBuffer.SendKeys(oldval.ToString());
                    val = buffer.ReadLineAlingRight(length, oldval);
                    buffer.ResetColor();
                }
            }

            return val != oldval;
        }

        public virtual int ReadMenu()
        {
            while (Items[Selected].First() == '!')
                Selected++;

            this.X = buffer.CursorLeft;
            this.Y = buffer.CursorTop;

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

        public virtual ConsoleKeyInfo Read()
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

        public virtual void Draw()
        {
            buffer.ForegroundColor = ForegroundColor;
            for (int i = 0; i < Items.Length; i++)
            {
                buffer.SetCursorPosition(X, Y + i);
                string item = Items[i];

                buffer.ForegroundColor = ForegroundColor;
                buffer.BackgroundColor = BackgroundColor;

                if (i == Selected)
                {
                    buffer.ForegroundColor = SelectedColor;
                    buffer.Write(pretoken);

                    if (fillBackround)
                    {
                        buffer.ForegroundColor = ForegroundColor;
                        buffer.BackgroundColor = SelectedColor;
                    }
                }
                else
                {
                    if (IsValidMenuItem(item))
                        buffer.Write("".PadLeft(pretoken.Length));
                    else
                        buffer.SetCursorPosition(X + pretoken.Length, Y + i);
                }

                if (!IsValidMenuItem(item))
                    item = (item.Substring(1));

                if (!IsValidMenuItem(item))
                {
                    item = (item.Substring(1));
                    buffer.ForegroundColor = HeadingColor;
                }

                buffer.Write(item);
                buffer.BackgroundColor = BackgroundColor;
            }

            buffer.DrawSelf(this.BufferX, this.BufferY);
        }

        public static bool IsValidMenuItem(string item)
        {
            return item.Length == 0 || item[0] != '!';
        }
    }
}
