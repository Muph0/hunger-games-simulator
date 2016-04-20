using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using hunger_games_simulator.assets;

namespace hunger_games_simulator.level
{
    [Serializable]
    class Biome
    {
        public Biome(int pivX, int pivY)
        {
            Pivot = new Point(pivX, pivY);
        }

        public string AssetName { get { return Asset.AssetName; } }
        public Point Pivot;
        public int[] TilesOwned;

        [NonSerialized]
        public BiomeAsset Asset;
    }
}
