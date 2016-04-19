using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace hunger_games_simulator.assets
{
    class TileAsset : SpawnableAsset
    {
        private BiomeAsset localBiome;

        public TileAsset(string name)
            : base(name, AssetType.tile)
        {
            localBiome = new BiomeAsset(this.AssetName) { Type = AssetType.tile };
        }

        public void LoadFrom(IniFile ini)
        {
            localBiome.LoadFrom(ini);

            base.LoadFrom(ini);
        }

        public level.Tile GenerateTile(Random rnd)
        {
            level.Tile result = localBiome.GenerateTile(rnd);
            return result;
        }
    }
}
