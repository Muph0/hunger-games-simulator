using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using hunger_games_simulator.level;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using hunger_games_simulator.core.networking;
using hunger_games_simulator.ui;
using System.Diagnostics;

namespace hunger_games_simulator.core
{
    class GameClient
    {
        public Arena ClientArena;
        public PlayerCharacter Character;
        public int LocalID = 0, Ping = 10;
        public ClientsideServerInfo ServerInfo;
        public LobbyMenu LobbyMenu;

        TcpClient tcpClient;
        public IPEndPoint ServerEp;
        public bool Ready = false;
        public bool LoggedIn;
        public bool Connected { get { return tcpClient.Connected; } }
        public string ErrorMessage = "<err>";

        int work = 0;
        Timer UpdateTimer;

        public GameClient()
        {
            LobbyMenu = new LobbyMenu(this);
            ServerInfo = new ClientsideServerInfo();
            tcpClient = new TcpClient();
            Character = new PlayerCharacter();
            Character.Randomize();
        }

        public void Connect(IPEndPoint ip)
        {
            LoggedIn = false;
            ServerEp = ip;
            tcpClient.BeginConnect(ServerEp.Address, ServerEp.Port, new AsyncCallback(OnConnect), this);
            UpdateTimer = new Timer(new TimerCallback(Update), null, 500, 1000);
        }

        public void Close()
        {
            tcpClient.Close();
            tcpClient = new TcpClient();
        }

        public void Sync()
        {
            while (work > 0)
            {
                Thread.Sleep(50);
            }
        }

        public void Send(ClientRequest req)
        {
            NetworkStream stream = tcpClient.GetStream();
            req.SendTo(stream);
        }

        void Update(object o)
        {
            if (LoggedIn && Connected)
            {
                NetworkStream stream = tcpClient.GetStream();
                ClientRequest req = new ClientRequest(this.LocalID);

                if (ServerInfo.GamePhase == GamePhase.Lobby)
                {
                    Stopwatch stopky = new Stopwatch();
                    stopky.Start();

                    req.Purpose = RequestPurpose.LobbyStatus;
                    req.Data = new object[] { ServersideClientInfo.FromClient(this) };
                    req.SendTo(stream);

                    ServerResponse resp = ServerResponse.ReceiveFrom(stream);
                    this.ServerInfo = (ClientsideServerInfo)resp.Data[0];
                    this.LobbyMenu.UpdateItems();
                    this.LobbyMenu.Draw();

                    stopky.Stop();
                    this.Ping = (int)stopky.ElapsedMilliseconds;
                }
            }
        }

        void OnConnect(IAsyncResult result)
        {
            work++;
            if (tcpClient.Connected)
            {
                NetworkStream stream = tcpClient.GetStream();

                ClientRequest req = new ClientRequest(-1);
                req.Purpose = RequestPurpose.Login;
                req.Data = new object[] { Character.Name, Character.ToString() };
                req.SendTo(stream);

                ServerResponse resp = ServerResponse.ReceiveFrom(stream);
                if (resp.Purpose == ResponseType.LoginAccepted)
                {
                    this.LocalID = (int)resp.Data[0];
                    this.ServerInfo = (ClientsideServerInfo)resp.Data[1];
                    LoggedIn = true;
                }
                else // resp.Purpose == ResponseType.ActionDenied;
                {
                    this.ErrorMessage = (string)resp.Data[0];
                }
            }
            work--;
        }

        public void ProcessResponse(ServerResponse resp)
        {

        }   
    }
}
