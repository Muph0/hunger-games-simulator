using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using hunger_games_simulator.assets;
using hunger_games_simulator.assets.info;

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

            List<string> ignored_assets = new List<string>();

            foreach (string sec in section_names)
            {
                string entry_type = sec.Split(':')[0];
                string name = sec.Split(':')[1];

                Asset asset = null;

                if (entry_type == Asset.AssetType.biome.ToString())
                {
                    BiomeAsset biomeAsset = new BiomeAsset(name);
                    biomeAsset.LoadFrom(ini);
                    asset = biomeAsset;
                }
                if (entry_type == Asset.AssetType.tile.ToString())
                {
                    TileAsset tileAsset = new TileAsset(name);
                    tileAsset.LoadFrom(ini);
                    asset = tileAsset;
                }

                if (asset != null)
                    this.AddNew(asset);
                else
                {
                    ignored_assets.Add(sec);
                }
            }

            if (ignored_assets.Count > 0)
            {
                FileInfo file = new FileInfo(filename);
                string errormsg = file.Name + ": " + "Following assets could not be loaded, because parser for their type is either not present or not working:\n\n";

                foreach (string s in ignored_assets)
                    errormsg += s + ", ";

                errormsg = errormsg.Substring(0, errormsg.Length - 2);

                ui.MessageBox.Show(errormsg + "\n", ui.MessageBox.Buttons.OK);
            }
        }

        public void AddNew(Asset a)
        {
            string name = a.AssetName;
            bool exists = true;

            try
            {
                this.GetAssetByName(name);
            }
            catch (ArgumentOutOfRangeException)
            {
                exists = false;
            }

            if (!exists)
            {
                if (a.Type == Asset.AssetType.biome)
                    BiomeAssets.Add(a.AssetName, (BiomeAsset)a);
                else if (a.Type == Asset.AssetType.tile)
                    TileAssets.Add(a.AssetName, (TileAsset)a);
                else if (a.Type == Asset.AssetType.item)
                    ItemAssets.Add(a.AssetName, (ItemAsset)a);
            }
        }
        public Asset GetAssetByName(string name)
        {
            if (BiomeAssets.ContainsKey(name))
                return BiomeAssets[name];
            if (TileAssets.ContainsKey(name))
                return BiomeAssets[name];
            if (ItemAssets.ContainsKey(name))
                return BiomeAssets[name];

            throw new ArgumentOutOfRangeException("Asset '" + name + "' does not exist");
        }

        public string PickBiomeAmountBased(Random rnd, int temp)
        {
            Dictionary<string, BiomeAsset> localClimaBiomes;
            localClimaBiomes = BiomeAssets
                .Where(a => (temp >= a.Value.minTemp && temp <= a.Value.maxTemp)).ToDictionary(v => v.Key, v => v.Value);

            int[] amts = new int[localClimaBiomes.Count];
            string[] names = new string[localClimaBiomes.Count];
            int amt_total = 0;
            for (int i = 0; i < localClimaBiomes.Count; i++)
            {
                amt_total += localClimaBiomes.ElementAt(i).Value.Amount;
                amts[i] = amt_total;
                names[i] = localClimaBiomes.ElementAt(i).Key;
            }

            int pick = rnd.Next(amt_total);

            for (int i = 0; i < localClimaBiomes.Count; i++)
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
