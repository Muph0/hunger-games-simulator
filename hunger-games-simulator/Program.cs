using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using hunger_games_simulator.ui;
using ConsoleBufferApi;
using hunger_games_simulator.core.networking;
using System.Diagnostics;

namespace hunger_games_simulator
{
    class Program
    {
        public static long Time
        { get { return stopky.ElapsedMilliseconds; } }

        static Stopwatch stopky;

        static void Main(string[] args)
        {
            stopky = new Stopwatch();
            stopky.Start();

            Console.CursorVisible = false;
            Console.BufferHeight = 25;
            Console.BufferWidth = 80;
            Console.Clear();

            //ConsoleBuffer.Fullscreen = true;
            new MainMenu().Show();

            ConsoleBuffer.Fullscreen = false;
        }
    }
}
