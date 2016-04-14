using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace hunger_games_simulator.assets
{
    class Asset
    {
        protected Asset(string name, Class type) 
        {
            this.Name = name;
            this.Type = type;
        }

        public string Name;
        public Class Type;

        

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
            else throw new Exception("Error parsing " + this.Type + " " + this.Name + "."); ;
        }

        public enum Class
        {
            item = 0,
            biome,
            container,
            tile
        }
    }
}
