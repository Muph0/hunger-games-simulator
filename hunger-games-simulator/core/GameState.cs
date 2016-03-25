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
        public TurnState State;
        public int MaxPlayers, Port;

        public GameState(Arena arena, int maxPlayers, int port)
        {
            this.Port = port;
            this.Arena = arena;
            this.MaxPlayers = maxPlayers;
        }

        public enum TurnState
        {
            Lobby = 0,
            Turn,
            DaySum,
        }
    }
}
