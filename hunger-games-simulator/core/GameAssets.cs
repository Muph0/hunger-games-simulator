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
        public Dictionary<string, BiomeAsset> BiomeAssets;
        public Dictionary<string, ItemAsset> ItemAssets;
        public Dictionary<string, TileAsset> TileAssets;

        public GameAssets()
        {
            BiomeAssets = new Dictionary<string, BiomeAsset>();
            ItemAssets = new Dictionary<string, ItemAsset>();
            TileAssets = new Dictionary<string, TileAsset>();
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
                if (file.Name.First() != '_')
                ParseLocal(file.FullName);
            }
            foreach (DirectoryInfo d in dir.GetDirectories())
            {
                if (d.Name.First() != '_')
                    LoadLocal(d.FullName);
            }
        }
        public void ParseLocal(string filename)
        {
            IniFile ini = new IniFile(filename);
            string[] section_names = ini.GetSectionNames();

            List<string> biomes_list = new List<string>();
            List<string> items_list = new List<string>();
            List<string> tiles_list = new List<string>();

            foreach (string sec in section_names)
            {
                string entry_type = sec.Split(':')[0];
                string name = sec.Split(':')[1];
                switch (entry_type)
                {
                    case "biome":
                        biomes_list.Add(name);
                        break;
                    case "item":
                        items_list.Add(name);
                        break;
                    case "tile":
                        tiles_list.Add(name);
                        break;
                }
            }

            foreach (string name in biomes_list)
            {
                BiomeAsset b = new BiomeAsset(name);
                b.LoadFrom(ini);
                this.BiomeAssets.Add(name, b);
            }

            foreach (string name in tiles_list)
            {
                TileAsset t = new TileAsset(name);
                t.LoadFrom(ini);
                this.TileAssets.Add(name, t);
            }

            foreach (string name in items_list)
            {
                ItemAsset i = new ItemAsset(name);
                i.Load(ini);
                this.ItemAssets.Add(name, i);
            }
        }

        public string PickBiomeAmountBased(Random rnd)
        {
            int[] amts = new int[BiomeAssets.Count];
            string[] names = new string[BiomeAssets.Count];
            int amt_total = 0;
            for (int i = 0; i < BiomeAssets.Count; i++)
            {
                amt_total += BiomeAssets.ElementAt(i).Value.Amount;
                amts[i] = amt_total;
                names[i] = BiomeAssets.ElementAt(i).Key;
            }

            int pick = rnd.Next(amt_total);

            for (int i = 0; i < BiomeAssets.Count; i++)
            {
                if (pick < amts[i])
                {
                    pick = i;
                    break;
                }
            }

            return names[pick];
        }
    }
}
