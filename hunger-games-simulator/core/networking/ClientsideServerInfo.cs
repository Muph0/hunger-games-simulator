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



        public static ClientsideServerInfo FromServer(GameServer serv)
        {
            ClientsideServerInfo info = new ClientsideServerInfo();

            info.GameName = serv.CurrentGame.GameName;
            info.Playerlist = serv.Clients.ToArray();

            return info;
        }
    }
}
