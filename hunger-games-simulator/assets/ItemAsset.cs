using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace hunger_games_simulator.assets
{
    [Serializable]
    class ItemAsset
    {
        public bool[] Usage = new bool[(int)UsageType.Length];
        public string FancyName;
        public int MassMin, MassMax; // g
        public string[] SpawnLocation;
        public int[] SpawnMin;
        public int[] SpawnMax;

        public ItemAsset(IniFile ini, string name)
        {
            Exception e = new Exception("Error parsing item " + name + " in file " + ini.path);
            string[] split = ini.GetEntryValue("item:" + name, "usage").ToString().Split(',');
            for (int i = 0; i < (int)UsageType.Length; i++)
            {
                if (split.Contains(((UsageType)i).ToString()))
                    Usage[i] = true;
            }

            this.FancyName = ini.GetEntryValue("item:" + name, "fancyname").ToString();

            string m = ini.GetEntryValue("item:" + name, "mass").ToString();
            split = m.Split('-');
            if (split.Length == 1)
            {
                MassMax = 1 + (MassMin = int.Parse(split[0]));
            }
            else if (split.Length == 2)
            {
                MassMin = int.Parse(split[0]);
                MassMax = int.Parse(split[1]);
            }
            else throw e;
        }

        public enum UsageType
        {
            Weapon = 0,
            Defence,
            Build,
            Length
        }
    }
}
