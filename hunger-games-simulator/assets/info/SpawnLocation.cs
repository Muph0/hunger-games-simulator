using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace hunger_games_simulator.assets.info
{
    class SpawnLocation
    {
        SpawnableAsset asset;

        public string Name { get; private set; }
        public int Min, Max;

        private SpawnLocation() { }
        public SpawnLocation(SpawnableAsset asset)
        {
            this.asset = asset;
        }

        public void LoadFromString(string str)
        {
            string[] bits = str.Split(' ');

            this.Name = bits[0];
            this.asset.ParseNumberOrTuple(bits[1], ref this.Min, ref this.Max);
        }

        public static SpawnLocation FromBiome(BiomeAsset asset)
        {
            SpawnLocation result = new SpawnLocation();
            result.Name = asset.Name;
            return result;
        }
    }
}
