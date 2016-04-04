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
        public long Ping;
        public bool Ready;

        [NonSerialized]
        public TcpServerConnection Connection;

        public ServersideClientInfo()
        {

        }

        public static ServersideClientInfo FromClient(GameClient client)
        {
            ServersideClientInfo info = new ServersideClientInfo();

            info.ClientID = client.LocalID;
            info.PlayerName = client.Character.Name;
            info.CharacterToString = client.Character.ToString();
            info.Ping = client.Ping;
            info.Ready = client.Ready;

            return info;
        }
    }
}
