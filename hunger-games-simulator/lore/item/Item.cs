using hunger_games_simulator.assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace hunger_games_simulator.lore.item
{
    class Item
    {
        [NonSerialized]
        private ItemAsset asset;

        public string AssetName
        { get { return asset.AssetName; } }
        public int Value, Mass;

        public Item(ItemAsset a)
        {
            this.asset = a;
            this.Value = 0;
        }
        public void Generate(Random rnd)
        {
            this.Mass = asset.Mass.Evaluate(rnd);
        }
    }
}
