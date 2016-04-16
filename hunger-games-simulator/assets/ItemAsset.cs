using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace hunger_games_simulator.assets
{
    [Serializable]
    class ItemAsset : UsableAsset
    {
        public string FancyName;
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
            this.FancyName = ini.GetEntryValue(this.ToString(), "fancyname").ToString();

            string mass = ini.GetEntryValue(this.ToString(), "mass").ToString();
            this.ParseNumberOrTuple(mass, ref MassMin, ref MassMax);
        }
    }
}
