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
            : base(name, Asset.AssetType.item)
        {

        }
        public ItemAsset(string name, Asset.AssetType type)
            : base(name, type)
        {

        }

        public override void LoadFrom(IniFile ini)
        {
            base.LoadFrom(ini);

            this.FancyName = ini.GetEntryValue(this.ToString(), "fancyname").ToString();
            string mass = ini.GetEntryValue(this.ToString(), "mass").ToString();
            this.ParseNumberOrTuple(mass, ref MassMin, ref MassMax);
        }
    }
}
