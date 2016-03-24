using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using hunger_games_simulator.level;

namespace hunger_games_simulator.core
{
    class GameServer
    {
        public GameAssets GameAssets;
        public Arena Arena;

        public void LoadAssets()
        {
            GameAssets = new GameAssets();
            GameAssets.LoadLocal();
        }
    }
}
