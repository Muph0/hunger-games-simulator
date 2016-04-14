using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace hunger_games_simulator.assets
{
    [Serializable]
    class ItemAsset : UsableAsset
    {
        public string FancyName, Name;
        public int MassMin, MassMax; // g

        public ItemAsset(string name)
            : base(name, Asset.Class.item)
        {

        }
        public ItemAsset(string name, Asset.Class type)
            : base(name, type)
        {

        }

        public void Load(IniFile ini)
        {

            this.Type = Asset.Class.item;
            this.LoadUsage(ini);
            this.FancyName = ini.GetEntryValue(this.Type + ":" + this.Name, "fancyname").ToString();

        }
    }
}
