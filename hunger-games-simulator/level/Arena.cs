using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsoleBufferApi;

namespace hunger_games_simulator.level
{
    class Arena
    {
        public int[] Heatmap;
        public Tile[] Tiles;
        public Biome[] Biomes;
        int seed = -1;
        public int Seed
        {
            get { return seed; }
            set
            {
                if (seed == -1)
                    seed = value;
                else
                    throw new Exception();
            }
        }

        public int Width { get; private set; }
        public int Height { get; private set; }

        public Arena(int width, int height)
        {
            this.Width = width;
            this.Height = height;

            Tiles = new Tile[Width * Height];
            Heatmap = new int[Width * Height];
        }
        public ConsoleBuffer MapBuffer(bool temperature = false)
        {
            ConsoleBuffer buf = new ConsoleBuffer(Width, Height);
            buf.Clear();
            for (int i = 0; i < Tiles.Length; i++)
            {
                Tile t = Tiles[i];
                if (temperature)
                    buf.SetPoint(TemperatureToColor(Heatmap[i]), i % Width, i / Width);
                else
                    buf.SetPoint(ConsoleBuffer.newCharInfo(t.Char, t.Foreground, t.Background), i % Width, i / Width);
            }
            return buf;
        }

        public static ConsoleBuffer.CharInfo TemperatureToColor(int temp)
        {
            ConsoleColor[] colorOrder = new ConsoleColor[]
            {
                ConsoleColor.White,
                ConsoleColor.Gray,
                ConsoleColor.DarkGray,
                ConsoleColor.DarkCyan,
                ConsoleColor.DarkBlue,
                ConsoleColor.Blue,
                ConsoleColor.Cyan,
                ConsoleColor.Green,
                ConsoleColor.Yellow,
                //ConsoleColor.DarkRed,
                ConsoleColor.Red,
                ConsoleColor.Magenta,
            };

            char[] charOrder = new char[] { ' ', '░', '▒', '▓', };

            List<ConsoleBuffer.CharInfo> charmap = new List<ConsoleBuffer.CharInfo>();
            for (int i = 0; i < colorOrder.Length - 1; i++)
            {
                ConsoleColor c0 = colorOrder[i];
                ConsoleColor c1 = colorOrder[i + 1];

                charmap.Add(ConsoleBuffer.newCharInfo(charOrder[0], c1, c0)); //  0%
                charmap.Add(ConsoleBuffer.newCharInfo(charOrder[1], c1, c0)); // 15%
                charmap.Add(ConsoleBuffer.newCharInfo(charOrder[2], c1, c0)); // 30%
                charmap.Add(ConsoleBuffer.newCharInfo(charOrder[3], c1, c0)); // 50%
                charmap.Add(ConsoleBuffer.newCharInfo(charOrder[2], c0, c1)); // 65%
                charmap.Add(ConsoleBuffer.newCharInfo(charOrder[1], c0, c1)); // 80%
            }

            int offset = 12;
            temp += offset;

            if (temp < 0)
                temp = 0;
            if (temp >= charmap.Count)
                temp = charmap.Count - 1;

            return charmap[temp];
        }
    }
}
