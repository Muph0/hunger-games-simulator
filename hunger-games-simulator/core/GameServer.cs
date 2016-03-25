using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using hunger_games_simulator.level;
using System.Net.Sockets;
using System.Net;
using tcpServer;
using hunger_games_simulator.core.networking;

namespace hunger_games_simulator.core
{
    class GameServer
    {
        public const int DEFAULT_PORT = 42011;
        public GameAssets GameAssets;
        public GameState CurrentGame { get { return cur_game; } }
        GameState cur_game;
        List<ServersideClientInfo> clients;

        TcpServer server;


        public void LoadAssets()
        {
            GameAssets = new GameAssets();
            GameAssets.LoadLocal();
        }

        public GameServer()
        {
            clients = new List<ServersideClientInfo>();

            server = new TcpServer();
            server.OnConnect += new tcpServerConnectionChanged(OnConnect);
            server.OnDataAvailable += new tcpServerConnectionChanged(OnDataAvailable);
        }

        public void Open(GameState gs)
        {
            this.cur_game = gs;
            server.Port = gs.Port;
            server.Open();
        }

        void OnConnect(TcpServerConnection connection)
        {
            NetworkStream stream = connection.Socket.GetStream();
            ClientRequest req = null;
            try
            {
                req = ClientRequest.ReceiveFrom(stream);
            }
            catch
            {
                return;
            }

            if (req.Purpose == ClientRequest.RequestType.Connect)
            {
                ServersideClientInfo client = new ServersideClientInfo();
                client.PlayerName = (string)req.Data[0];
                client.ID = clients.Count;
                clients.Add(client);

                ServerResponse toSend = new ServerResponse();
                toSend.Purpose = ServerResponse.ResponseType.Handshake;
                toSend.Data = new object[] { client.ID };
                toSend.SendTo(stream);
            }
        }

        void OnDataAvailable(TcpServerConnection connection)
        {
            NetworkStream stream = connection.Socket.GetStream();
            ClientRequest req = ClientRequest.ReceiveFrom(stream);


        }
    }
}
