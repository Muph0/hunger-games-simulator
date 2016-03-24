using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace hunger_games_simulator.assets
{
    [Serializable]
    class BiomeAsset
    {
        public string[] Chars;
        public ConsoleColor[] Foregrounds, Backgrounds;

        public int[] Excepts;
        public int Amount;

        public BiomeAsset(IniFile ini, string name)
        {
            Exception e = new Exception("Error parsing biome " + name + " in file " + ini.path);

            string[] split = ini.GetEntryValue("biome:" + name, "colors").ToString().Split(',');
            this.Foregrounds = new ConsoleColor[split.Length];
            this.Backgrounds = new ConsoleColor[split.Length];
            this.Chars = new string[split.Length];
            for (int i = 0; i < split.Length; i++)
            {
                string str = split[i];
                if (str.Length < 3) throw e;
                Foregrounds[i] = (ConsoleColor)Convert.ToInt32(str[0].ToString(), 16);
                Backgrounds[i] = (ConsoleColor)Convert.ToInt32(str[1].ToString(), 16);
                Chars[i] = str.Substring(2);
            }

            string amt = ini.GetEntryValue("biome:" + name, "amount").ToString();
            this.Amount = int.Parse(amt);
        }

        public level.Tile GenerateTile(Random rnd)
        {
            level.Tile tile = new level.Tile();

            int picked = rnd.Next(Foregrounds.Length);
            tile.Foreground = Foregrounds[picked];
            tile.Background = Backgrounds[picked];

            tile.Char = Chars[picked][rnd.Next(Chars[picked].Length)];

            return tile;
        }
    }
}
