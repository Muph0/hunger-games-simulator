using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace hunger_games_simulator.assets
{
    class Asset
    {
        protected Asset() { }

        public string Name;
        public AssetType Type;

        protected Exception e;

        protected void LoadUsage(IniFile ini, ref bool[] usage)
        {
            usage = new bool[(int)UsageType._Length];

            string[] split = ini.GetEntryValue("item:" + Name, "usage").ToString().Split(',');
            for (int i = 0; i < usage.Length; i++)
            {
                if (split.Contains(((UsageType)i).ToString().ToUpper()))
                    usage[i] = true;
            }
        }


        protected void ParseNumberOrTuple(string str, ref int n0, ref int n1)
        {
            string[] split = str.Split('-');
            if (split.Length == 1)
            {
                n1 = 1 + (n0 = int.Parse(split[0]));
            }
            else if (split.Length == 2)
            {
                n0 = int.Parse(split[0]);
                n1 = int.Parse(split[1]);
            }
            else throw e;
        }

        public enum UsageType
        {
            Weapon = 0,
            Defence,
            Build,
            Ranged,
            _Length
        }

        public enum AssetType
        {
            item = 0,
            biome,
            container
        }
    }
}
