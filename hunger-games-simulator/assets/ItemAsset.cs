using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace hunger_games_simulator.assets
{
    [Serializable]
    class ItemAsset : Asset
    {
        public bool[] Usage = new bool[(int)UsageType._Length];
        public string FancyName, Name;
        public int MassMin, MassMax; // g
        public string[] SpawnLocation;
        public int[] SpawnMin;
        public int[] SpawnMax;

        public static ItemAsset Load(IniFile ini, string name)
        {
            ItemAsset result = new ItemAsset() { Name = name };

            result.e = new Exception("Error parsing item " + name + " in file " + ini.path);
            result.Type = AssetType.item;
            result.LoadUsage(ini, ref result.Usage);

            result.FancyName = ini.GetEntryValue(result.Type + ":" + name, "fancyname").ToString();


            string str = ini.GetEntryValue(result.Type + ":" + name, "spawn").ToString();
            string[] split = str.Split(',');
            if (true)
            {
                result.SpawnLocation = new string[split.Length];
                result.SpawnMin = new int[split.Length];
                result.SpawnMax = new int[split.Length];

                for (int i = 0; i < split.Length; i++)
                {
                    result.SpawnLocation[i] = split[i].Split(' ')[0];
                    result.ParseNumberOrTuple(split[i].Split(' ')[1], ref result.SpawnMin[i], ref result.SpawnMax[i]);
                }
            }

            return result;
        }
    }
}
