using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using hunger_games_simulator.level;

namespace hunger_games_simulator.core
{
    class GameState
    {
        public Arena Arena;
        public GamePhase Phase;
        public int MaxPlayers, Port;
        public string GameName;

        public GameState(Arena arena, int maxPlayers, int port, string gameName)
        {
            this.GameName = gameName;
            this.Port = port;
            this.Arena = arena;
            this.MaxPlayers = maxPlayers;
        }
    }

    public enum GamePhase
    {
        Lobby = 0,
        Turn,
        DaySum,
    }
}
