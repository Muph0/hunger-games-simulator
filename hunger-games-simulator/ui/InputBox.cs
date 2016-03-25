using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsoleBufferApi;

namespace hunger_games_simulator.ui
{
    class InputBox
    {
        public static void Show(string title, ref string result, int width = 35)
        {
            ConsoleBuffer buf = new ConsoleBuffer(width, 5);

            buf.ForegroundColor = ConsoleColor.Yellow;

            // window borders
            buf.Write("/".PadRight(width - 1, '-') + "\\");
            buf.SetCursorPosition(0, buf.Height - 1);
            buf.Write("\\".PadRight(width - 1, '-') + "/");
            buf.SetCursorPosition(0, 1);
            buf.WriteVertical("|||");
            buf.SetCursorPosition(width - 1, 1);
            buf.WriteVertical("|||");

            // Title
            buf.SetCursorPosition(4, 0);
            buf.Write(" " + title + " ");

            // Textbox frame
            buf.SetCursorPosition(2, 2);
            buf.Write(">");

            int Xoffset = (80 - width) / 2;
            int Yoffset = 9;
            buf.DrawSelf(Xoffset, Yoffset);
            Console.SetCursorPosition(4 + Xoffset, 2 + Yoffset);
            bool vis = Console.CursorVisible;
            Console.CursorVisible = true;
            result = Console.ReadLine();
            Console.CursorVisible = vis;
        }
    }
}
