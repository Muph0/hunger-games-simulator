using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using hunger_games_simulator.assets.info;

namespace hunger_games_simulator.assets
{
    class SpawnableAsset : Asset
    {
        public SpawnableAsset(string name, Asset.Class type)
            : base(name, type)
        {

        }

        public SpawnLocation[] SpawnLocations;

        protected void LoadSpawn(IniFile ini)
        {
            string line = ini.GetEntryValue(this.Type.ToString() + ":" + this.Name, "spawn").ToString();

            string[] locations = line.Split(',');
            SpawnLocations = new SpawnLocation[locations.Length];

            for (int i = 0; i < locations.Length; i++)
            {
                if (locations[i].Length == 0)
                    continue;

                SpawnLocations[i] = new SpawnLocation(this);
                SpawnLocations[i].LoadFromString(locations[i]);
            }
        }
    }
}
