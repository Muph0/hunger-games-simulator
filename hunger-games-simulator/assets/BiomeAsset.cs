using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace hunger_games_simulator.assets
{
    [Serializable]
    class BiomeAsset : Asset
    {
        public string[] Chars;
        public ConsoleColor[] Foregrounds, Backgrounds;

        public int[] Excepts;
        public int Amount;

        public BiomeAsset(string name)
            : base(name, Class.biome)
        {
            
        }

        public void LoadFrom(IniFile ini)
        {
            Exception e = new Exception("Error parsing " + this.ToString() + " in file " + ini.path);

            string[] split = ini.GetEntryValue(this.ToString(), "colors").ToString().Split(',');
            this.Foregrounds = new ConsoleColor[split.Length];
            this.Backgrounds = new ConsoleColor[split.Length];
            this.Chars = new string[split.Length];
            for (int i = 0; i < split.Length; i++)
            {
                string str = split[i];
                if (str.Length < 3) throw e;
                this.Foregrounds[i] = (ConsoleColor)Convert.ToInt32(str[0].ToString(), 16);
                this.Backgrounds[i] = (ConsoleColor)Convert.ToInt32(str[1].ToString(), 16);
                this.Chars[i] = str.Substring(2);
            }

            string amt = ini.GetEntryValue(this.ToString(), "amount").ToString();
            int.TryParse(amt, out this.Amount);
        }

        public level.Tile GenerateTile(Random rnd)
        {
            level.Tile tile = new level.Tile();

            int picked = rnd.Next(Foregrounds.Length);
            tile.Foreground = Foregrounds[picked];
            tile.Background = Backgrounds[picked];
            tile.AssetName = this.Name;

            tile.Char = Chars[picked][rnd.Next(Chars[picked].Length)];

            return tile;
        }
    }
}
