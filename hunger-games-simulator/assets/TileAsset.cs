using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace hunger_games_simulator.assets
{
    class TileAsset : SpawnableAsset
    {
        public TileAsset(string name)
            : base (name, Class.tile)
        {
            
        }

        public void LoadFrom(IniFile ini)
        {
            this.LoadSpawn(ini);
        }
    }
}
