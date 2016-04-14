using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace hunger_games_simulator.assets
{
    class UsableAsset : SpawnableAsset
    {
        public bool[] Usage = new bool[(int)UsableAsset.Class._Length];

        public UsableAsset(string name, Asset.Class type)
            : base(name, type)
        {

        }

        protected void LoadUsage(IniFile ini)
        {
            this.Usage = new bool[(int)UsableAsset.Class._Length];

            string[] split = ini.GetEntryValue("item:" + Name, "usage").ToString().Split(',');
            for (int i = 0; i < this.Usage.Length; i++)
            {
                if (split.Contains(((UsableAsset.Class)i).ToString().ToUpper()))
                    this.Usage[i] = true;
            }
        }

        public enum Class
        {
            Weapon = 0,
            Defence,
            Build,
            Ranged,
            _Length
        }
    }
}
