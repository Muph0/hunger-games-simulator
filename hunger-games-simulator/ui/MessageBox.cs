using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsoleBufferApi;

namespace hunger_games_simulator.ui
{
    class MessageBox
    {
        public static int Show(string text, Buttons buttons)
        {
            List<string> Bs = new List<string>();

            if ((int)buttons % 2 == 0)
                Bs.Add("OK");

            if ((buttons & Buttons.YesNo) == Buttons.YesNo)
            {
                Bs.Add("Yes");
                Bs.Add("No");
            }

            if ((buttons & Buttons.Cancel) == Buttons.Cancel)
            {
                Bs.Add("Cancel");
            }

            return Show(text, Bs.ToArray());
        }
        public static int Show(string text, string[] options)
        {
            Menu menu = new Menu(options) { width = 10, SelectedColor = ConsoleColor.Yellow, fillBackround = false };

            int width = 60;
            if (text.Length < 60)
                width = text.Length + 4;

            text = TextBreaker.BreakText(text, width - 4);
            int height = 3 + text.Count(x => x == '\n') + options.Length;

            ConsoleBuffer buf = new ConsoleBuffer(width, height) { ForegroundColor = ConsoleColor.Yellow };

            buf.SetCursorPosition(0, 0);
            buf.Write("/".PadRight(width - 1, '-') + "\\");
            buf.SetCursorPosition(0, height - 1);
            buf.Write("\\".PadRight(width - 1, '-') + "/");

            buf.SetCursorPosition(0, 1);
            buf.WriteVertical("".PadRight(height - 2, '|'));
            buf.SetCursorPosition(width - 1, 1);
            buf.WriteVertical("".PadRight(height - 2, '|'));

            buf.ForegroundColor = ConsoleColor.Gray;

            string[] lines = text.Split('\n');
            for (int i = 0; i < lines.Length; i++)
            {
                buf.SetCursorPosition(2, 1 + i);
                buf.Write(lines[i]);
            }

            menu.BufferX = (80 - width) / 2;
            menu.BufferY = (25 - height) / 2;

            buf.SetCursorPosition(width / 2 - 4, height - 1 - options.Length);
            menu.buffer = buf;
            menu.ReadMenu();

            return menu.Selected;
        }

        public enum Buttons
        {
            OK = 0,
            YesNo = 1,
            Cancel = 2,
        }
    }
}
