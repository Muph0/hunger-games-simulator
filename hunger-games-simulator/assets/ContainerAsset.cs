using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace hunger_games_simulator.assets
{
    [Serializable]
    class ContainerAsset : ItemAsset
    {
        public List<string> ItemsWhichSpawnHere = new List<string>();
        public string Portable = null;

        public ContainerAsset(string name, Asset.Class type)
            : base(name, type)
        {

        }

        public static ContainerAsset Load(IniFile ini, string name)
        {
            ContainerAsset result = new ContainerAsset(name, Asset.Class.container);

            result.FancyName = ini.GetEntryValue(result.Type + ":" + name, "fancyname").ToString();

            return result;
        }
    }
}
