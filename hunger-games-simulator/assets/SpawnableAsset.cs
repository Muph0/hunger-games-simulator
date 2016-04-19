using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using hunger_games_simulator.assets.info;

namespace hunger_games_simulator.assets
{
    class SpawnableAsset : Asset
    {
        public SpawnableAsset(string name, Asset.AssetType type)
            : base(name, type)
        {

        }

        public SpawnDestination[] SpawnDestinations;

        public override void LoadFrom(IniFile ini)
        {
            this.LoadSpawn(ini);
        }

        private void LoadSpawn(IniFile ini)
        {
            string line = ini.GetEntryValue(this.Type.ToString() + ":" + this.AssetName, "spawn").ToString();

            if (line == "")
            {
                this.SpawnDestinations = new SpawnDestination[0];
                return;
            }

            string[] locations = line.Split(',');
            this.SpawnDestinations = new SpawnDestination[locations.Length];

            for (int i = 0; i < locations.Length; i++)
            {
                if (locations[i].Length == 0)
                    continue;

                this.SpawnDestinations[i] = new SpawnDestination(this);
                this.SpawnDestinations[i].LoadFromString(locations[i]);
            }
        }
    }
}
