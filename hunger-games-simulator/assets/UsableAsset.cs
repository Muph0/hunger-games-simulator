using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace hunger_games_simulator.assets
{
    class UsableAsset : SpawnableAsset
    {
        public bool[] Usage = new bool[(int)UsableAsset.UsageType._Length];

        public UsableAsset(string name, Asset.AssetType type)
            : base(name, type)
        {

        }

        public override void LoadFrom(IniFile ini)
        {
            base.LoadFrom(ini);
            this.LoadUsage(ini);
        }

        private void LoadUsage(IniFile ini)
        {
            this.Usage = new bool[(int)UsableAsset.UsageType._Length];

            string[] split = ini.GetEntryValue("item:" + AssetName, "usage").ToString().Split(',');
            for (int i = 0; i < this.Usage.Length; i++)
            {
                if (split.Contains(((UsableAsset.UsageType)i).ToString().ToUpper()))
                    this.Usage[i] = true;
            }
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
