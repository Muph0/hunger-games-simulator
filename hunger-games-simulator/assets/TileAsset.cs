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
            : base (name, Class.tile)
        {
            localBiome = new BiomeAsset(this.Name) { Type = Class.tile };
        }

        public void LoadFrom(IniFile ini)
        {
            this.LoadSpawn(ini);
            localBiome.LoadFrom(ini);
        }

        public level.Tile GenerateTile(Random rnd)
        {
            level.Tile result = localBiome.GenerateTile(rnd);
            return result;
        }
    }
}
