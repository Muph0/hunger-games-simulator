using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace hunger_games_simulator.core.networking
{
    [Serializable]
    class ClientsideServerInfo
    {
        public ServersideClientInfo[] Playerlist;
        public string GameName;
        public GamePhase GamePhase;

        public ClientsideServerInfo()
        {

        }
        public ClientsideServerInfo(GameServer serv)
        {
            GameName = serv.CurrentGame.GameName;
            Playerlist = serv.Clients.ToArray();
        }
    }
}
