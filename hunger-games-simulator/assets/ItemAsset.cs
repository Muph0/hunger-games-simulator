using hunger_games_simulator.lore.item;
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
        public RandomRange Mass;

        public ItemAsset(string name)
            : base(name, Asset.AssetType.item)
        {

        }
        public ItemAsset(string name, Asset.AssetType type)
            : base(name, type)
        {

        }

        public Item GenerateItem()
        {
            Item item = new Item(this);

            return item;
        }

        public override void LoadFrom(IniFile ini)
        {
            base.LoadFrom(ini);

            this.FancyName = ini.GetEntryValue(this.ToString(), "fancyname").ToString();
            string mass = ini.GetEntryValue(this.ToString(), "mass").ToString();
            this.Mass = new RandomRange();
            this.ParseNumberOrTuple(mass, ref Mass.Min, ref Mass.Max);
        }
    }
}
