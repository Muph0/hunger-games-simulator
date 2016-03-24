using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using hunger_games_simulator.assets;

namespace hunger_games_simulator.core
{
    class GameAssets
    {
        public Dictionary<string, BiomeAsset> Biomes;
        public Dictionary<string, ItemAsset> Items;

        public GameAssets()
        {

        }

        public void LoadLocal()
        {
            LoadLocal("assets");
        }
        private void LoadLocal(string dirpath)
        {
            DirectoryInfo dir = new DirectoryInfo(dirpath);
            foreach (FileInfo file in dir.GetFiles("*.ini"))
            {
                ParseLocal(file.FullName);
            }
            foreach (DirectoryInfo d in dir.GetDirectories())
            {
                LoadLocal(d.FullName);
            }
        }

        public void ParseLocal(string filename)
        {
            IniFile ini = new IniFile(filename);
            string[] section_names = ini.GetSectionNames();

            List<string> biomes_list = new List<string>();
            List<string> items_list = new List<string>();

            foreach (string sec in section_names)
            {
                string entry = sec.Split(':')[0];
                string name = sec.Split(':')[1];
                switch (entry)
                {
                    case "biome":
                        biomes_list.Add(name);
                        break;
                    case "item":
                        items_list.Add(name);
                        break;
                }
            }

            foreach (string biome in biomes_list)
            {
                BiomeAsset b = new BiomeAsset(ini, biome);
                this.Biomes.Add(biome, b);
            }
        }
    }
}
