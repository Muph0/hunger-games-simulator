using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsoleBufferApi;

namespace hunger_games_simulator.level
{
    class Arena
    {
        public Tile[] Tiles;
        public Biome[] Biomes;

        public int Width { get; private set; }
        public int Height { get; private set; }

        public Arena(int width, int height)
        {
            this.Width = width;
            this.Height = height;

            Tiles = new Tile[Width * Height];
        }

        public ConsoleBuffer MapBuffer()
        {
            ConsoleBuffer buf = new ConsoleBuffer(Width, Height);
            buf.Clear();
            for (int i = 0; i < Tiles.Length; i++)
            {
                Tile t = Tiles[i];
                buf.SetPoint(ConsoleBuffer.newCharInfo(t.Char, t.Foreground, t.Background), i % Width, i / Width);
            }
            return buf;
        }
    }
}
