using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace hunger_games_simulator.assets
{
    [Serializable]
    class BiomeAsset
    {
        public string Chars;
        public ConsoleColor[] Foregrounds, Backgrounds;

        public int[] Excepts;
        public int Amount;

        public BiomeAsset(IniFile ini, string name)
        {
            Exception e = new Exception("Error parsing biome " + name + " in file " + ini.path);
            string[] split = ini.GetEntryValue("biome:" + name, "chars").ToString().Split(',');
            foreach (string str in split)
            {
                if (str.Length == 1)
                    Chars += str;
                else if (str.Length > 1)
                    Chars += ConsoleBufferApi.ConsoleBuffer.ASCII[int.Parse(str)];
                else throw e;
            }

            split = ini.GetEntryValue("biome:" + name, "colors").ToString().Split(',');
            this.Foregrounds = new ConsoleColor[split.Length];
            this.Backgrounds = new ConsoleColor[split.Length];
            for (int i = 0; i < split.Length; i++)
            {
                string str = split[i];
                if (str.Length != 2) throw e;
                Foregrounds[i] = (ConsoleColor)Convert.ToInt32(str[0].ToString(), 16);
                Backgrounds[i] = (ConsoleColor)Convert.ToInt32(str[1].ToString(), 16);
            }

            split = ini.GetEntryValue("biome:" + name, "except").ToString().Split(',');
            this.Excepts = new int[2 * split.Length];
            for (int i = 0; i < split.Length; i++)
                for (int j = 0; j < 2; j++)
                    Excepts[2 * i + j] = int.Parse(split[i].Split(' ')[j]);

            this.Amount = int.Parse(ini.GetEntryValue("biome:" + name, "except").ToString());
        }
    }
}
