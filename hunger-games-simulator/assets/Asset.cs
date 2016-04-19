using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace hunger_games_simulator.assets
{
    class Asset
    {
        protected Asset(string name, AssetType type) 
        {
            this.AssetName = name;
            this.Type = type;
        }

        public string AssetName;
        public AssetType Type;

        public List<string> SpawnsHere = new List<string>();
        

        public void ParseNumberOrTuple(string str, ref int n0, ref int n1)
        {
            string[] split = str.Split('-');
            if (split.Length == 1)
            {
                n1 = 1 + (n0 = int.Parse(split[0]));
            }
            else if (split.Length == 2)
            {
                n0 = int.Parse(split[0]);
                n1 = int.Parse(split[1]);
            }
            else throw new Exception("Error parsing " + this.Type + " " + this.AssetName + "."); ;
        }

        public virtual void LoadFrom(IniFile ini)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return this.Type + ":" + this.AssetName;
        }

        public enum AssetType
        {
            item = 0,
            biome,
            container,
            tile,
            _last
        }
    }
}
