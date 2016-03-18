using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Muphcode;
using Muphcode.Xna2D;
using System.Threading;
using System.Drawing;
using HungerGames.ui;
using HungerGames.world;

namespace HungerGames
{
    class Program
    {
        public static Color zelena = Color.FromArgb(170, 208, 81);
        public static Color modro = Color.FromArgb(113, 182, 208);

        public static Console2D console;
        public static ServerOptions server_options;
        public static GameOptions character_options;
        public static Player player;
        public static int INT;

        static System.Diagnostics.Stopwatch bench_watch = new System.Diagnostics.Stopwatch();

        static void Main(string[] args)
        {
            Console2D.Extend(320);

            console = new Console2D();
            Thread logicThread = new Thread(delegate() { logic(console); });
            logicThread.Start();
            console.Run();
        }
        static void logic(Console2D console)
        {
            items.Item.LoadItems();
            MainMenu.Show(console);
            //Thread.Sleep(160);
            Environment.Exit(1);
        }
        static void Benchmark(string str)
        {
            long elapsed = bench_watch.ElapsedMilliseconds;
            bench_watch.Restart();

            System.Diagnostics.Debug.Print(str + ": " + elapsed + "ms");
        }
    }
}
