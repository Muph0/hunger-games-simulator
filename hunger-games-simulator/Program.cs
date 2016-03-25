using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using hunger_games_simulator.ui;
using ConsoleBufferApi;

namespace hunger_games_simulator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Console.BufferHeight = 25;
            Console.BufferWidth = 80;
            Console.Clear();

            ConsoleBuffer.Fullscreen = true;
            new MainMenu().Show();
        }
    }
}
