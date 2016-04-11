using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace hunger_games_simulator.assets
{
    [Serializable]
    class ItemAsset
    {
        public bool[] Usage = new bool[(int)UsageType._Length];
        public string FancyName;
        public int MassMin, MassMax; // g
        public string[] SpawnLocation;
        public int[] SpawnMin;
        public int[] SpawnMax;

        private ItemAsset()
        {

        }

        public static ItemAsset Load(IniFile ini, string name)
        {
            ItemAsset result = new ItemAsset();

            Exception e = new Exception("Error parsing item " + name + " in file " + ini.path);
            string[] split = ini.GetEntryValue("item:" + name, "usage").ToString().Split(',');
            for (int i = 0; i < (int)UsageType._Length; i++)
            {
                if (split.Contains(((UsageType)i).ToString().ToUpper()))
                    result.Usage[i] = true;
            }

            result.FancyName = ini.GetEntryValue("item:" + name, "fancyname").ToString();

            string m = ini.GetEntryValue("item:" + name, "mass").ToString();
            split = m.Split('-');
            if (split.Length == 1)
            {
                result.MassMax = 1 + (result.MassMin = int.Parse(split[0]));
            }
            else if (split.Length == 2)
            {
                result.MassMin = int.Parse(split[0]);
                result.MassMax = int.Parse(split[1]);
            }
            else throw e;

            m = ini.GetEntryValue("item:" + name, "spawn").ToString();

            return result;
        }

        public enum UsageType
        {
            Weapon = 0,
            Defence,
            Build,
            Ranged,
            _Length
        }
    }
}
