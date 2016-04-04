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
        public List<ServersideClientInfo> Clients;

        TcpServer server;

        public int FreeSlots
        {
            get
            {
                return CurrentGame.MaxPlayers - Clients.Count;
            }
        }

        public void LoadAssets()
        {
            GameAssets = new GameAssets();
            GameAssets.LoadLocal();
        }

        public GameServer()
        {
            Clients = new List<ServersideClientInfo>();
            server = new TcpServer();
        }

        public void Open(GameState gs)
        {
            this.cur_game = gs;
            server.Port = gs.Port;
            server.OnConnect += new tcpServerConnectionChanged(OnConnect);
            server.OnDataAvailable += new tcpServerConnectionChanged(OnDataAvailable);
            server.Open();
        }

        void Update()
        {
            for (int i = 0; i < Clients.Count; i++)
            {
                ServersideClientInfo client = Clients[i];


            }
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

            if (req.Purpose == RequestPurpose.Login)
            {
                if (FreeSlots > 0 && CurrentGame.Phase == GamePhase.Lobby)
                {
                    ServersideClientInfo client = new ServersideClientInfo();
                    client.Connection = connection;
                    client.PlayerName = (string)req.Data[0];
                    client.CharacterToString = (string)req.Data[1];
                    client.ClientID = Clients.Count;
                    Clients.Add(client);

                    ServerResponse toSend = new ServerResponse();
                    toSend.Purpose = ResponseType.LoginAccepted;
                    toSend.Data = new object[] { client.ClientID, ClientsideServerInfo.FromServer(this) };
                    toSend.SendTo(stream);
                }
                else
                {
                    ServerResponse toSend = new ServerResponse();
                    toSend.Purpose = ResponseType.ActionDenied;
                    toSend.Data = new object[] { "" };
                    if (FreeSlots == 0)
                    {
                        toSend.Data[0] = "Server full";
                    }
                    else
                    {
                        toSend.Data[0] = "Game in progress";
                    }

                    toSend.SendTo(stream);
                    server.Connections.Remove(connection);
                }
            }
        }
        void OnDataAvailable(TcpServerConnection connection)
        {
            NetworkStream stream = connection.Socket.GetStream();
            ClientRequest req = ClientRequest.ReceiveFrom(stream);

            ServerResponse resp = ProcessRequest(req);
            resp.SendTo(stream);
        }

        public ServerResponse ProcessRequest(ClientRequest req)
        {
            if (CurrentGame.Phase == GamePhase.Lobby)
            {
                if (req.Purpose == RequestPurpose.LobbyStatus)
                {
                    this.Clients[req.ClientID] = (ServersideClientInfo)req.Data[0];

                    ServerResponse toSend = new ServerResponse();
                    toSend.Purpose = ResponseType.LobbyInfo;
                    toSend.Data = new object[] { ClientsideServerInfo.FromServer(this) };
                    return toSend;
                }
            }

            ServerResponse denied = new ServerResponse();
            denied.Purpose = ResponseType.ActionDenied;
            denied.Data = new object[] { "" };
            return denied;
        }

        public void Close()
        {
            server.Close();
        }
    }
}
