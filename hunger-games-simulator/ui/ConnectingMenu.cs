using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsoleBufferApi;
using System.Threading;
using hunger_games_simulator.core;

namespace hunger_games_simulator.ui
{
    class ConnectingMenu : Menu
    {
        ConsoleBuffer headline;
        static string[] proitems1 = new string[] { "!Failed", "!", "OK" };
        public ConnectingMenu()
            : base(proitems1)
        {
            this.SelectedColor = ConsoleColor.Blue;
            string connecting_str =
"╔═╗╔═╗╔╗╔╔╗╔╔═╗╔═╗╔╦╗╦╔╗╔╔═╗" +
"║  ║ ║║║║║║║║╣ ║   ║ ║║║║║ ╦" +
"╚═╝╚═╝╝╚╝╝╚╝╚═╝╚═╝ ╩ ╩╝╚╝╚═╝";

            headline = new ConsoleBuffer(connecting_str.Length / 3, 3);
            headline.ForegroundColor = ConsoleColor.White;
            headline.Write(connecting_str);
        }

        public void Show(GameClient client)
        {
            buffer.SetCursorPosition(10, 3);
            buffer.ForegroundColor = ConsoleColor.Gray;
            buffer.InsertBuffer(headline, 26, 4);
            string ip = client.ServerEp.Address.ToString();
            buffer.SetCursorPosition((80 - ip.Length) / 2, 8);
            buffer.Write(ip);
            buffer.DrawSelf();

            Console.SetCursorPosition(26, 12);
            Console.ForegroundColor = SelectedColor;
            Console.Write(pretoken);
            Console.ResetColor();
            Console.BackgroundColor = SelectedColor;
            Console.Write("Cancel");
            Console.ResetColor();

            client.Connect();



            long starttime = Program.Time;
            while (starttime > Program.Time - 20 * 1000/*ms*/)
            {
                double time = Program.Time / 500.0 * Math.PI;
                ConsoleBuffer slider = new ConsoleBuffer(headline.Width, 1);

                string s = "██████";
                slider.DrawText(s, (int)(0.55 * Math.Sin(time) * slider.Width + 0.5 * slider.Width) - s.Length / 2, 0, this.SelectedColor, ConsoleColor.Black);

                slider.DrawText(" ", 0, 0, ConsoleColor.Black, ConsoleColor.White);
                slider.DrawText(" ", slider.Width - 1, 0, ConsoleColor.Black, ConsoleColor.White);
                slider.DrawSelf(26, 10);
                Thread.Sleep(20);

                if (Console.KeyAvailable)
                    if (Console.ReadKey(true).Key == ConsoleKey.Enter)
                        return;

                if (client.Connected)
                    break;
            }

            buffer.DrawSelf();
            buffer.SetCursorPosition(26, 10);

            if (client.Connected)
            {
                buffer.Write("Connected!");
                buffer.DrawSelf();
                client.Sync(); // waits for client to finish stuff
            }
            else
            {
                this.ReadMenu();
            }
        }
    }
}
