using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using tcpServer;

namespace hunger_games_simulator.core.networking
{
    [Serializable]
    class ServersideClientInfo
    {
        public int ClientID;
        public string PlayerName;
        public string CharacterToString;
        public long LastRequestTime;
        public bool Ready;

        [NonSerialized]
        public TcpServerConnection Connection;

        public ServersideClientInfo()
        {

        }
    }
}
