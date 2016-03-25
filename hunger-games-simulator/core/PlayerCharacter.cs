using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace hunger_games_simulator.core
{
    [Serializable]
    class PlayerCharacter
    {
        public string Name;

        public int StatPoints = 15;
        public int Strength, Agility, Intelligence, Metabolic;

        public int SkillPoints = 30;
        public int Crafting, Archery, LightWep, HeavyWep, Cooking, Sneaking, Running, Climbing;

        public int CanCarry
        {
            get
            {
                return 12 + Strength;
            }
        }
    }
}
