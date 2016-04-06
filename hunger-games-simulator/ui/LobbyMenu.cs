﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using hunger_games_simulator.core;
using hunger_games_simulator.core.networking;

namespace hunger_games_simulator.ui
{
    class LobbyMenu : Menu
    {
        static string[] proitems = new string[] { };
        private long last_R = 0;

        GameClient client;
        public LobbyMenu(GameClient client)
            : base(proitems)
        {
            this.SelectedColor = ConsoleColor.White;
            this.fillBackround = false;
            this.client = client;
            width = 40;
        }

        public void UpdateItems()
        {
            Items = new string[client.ServerInfo.Playerlist.Length * 2 + 2];
            Items[0] = "!" + client.ServerInfo.GameName + " hosted on " + client.ServerEp.Address.ToString();
            Items[1] = "!" + "".PadRight(41, '-');

            for (int i = 0; i < client.ServerInfo.Playerlist.Length; i++)
            {
                ServersideClientInfo remotePlayer = client.ServerInfo.Playerlist[i];
                if (i == client.LocalID)
                    remotePlayer = ServersideClientInfo.FromClient(client);

                Items[2 * i + 2] = "◘◘1" + (i + 1) + ". " + remotePlayer.PlayerName;
                Items[2 * i + 2] = Items[2 * i + 2].PadRight(24 + 3);
                Items[2 * i + 2] += (!remotePlayer.Ready ? "◘c◘NOT " : "◘a◘    ") + "READY◘7◘";
                Items[2 * i + 2] += "".PadRight(2) + remotePlayer.Ping + " ms◘◘0 ";

                Items[2 * i + 3] = "! " + client.ServerInfo.Playerlist[i].CharacterToString;

            }
        }

        public void Show()
        {
            while (true)
            {
                buffer.SetCursorPosition(6, 3);
                UpdateItems();
                this.ReadMenu();

                if (Selected == Items.Length - 1 && client.LocalID == 0)
                {

                }
            }
        }

        public override ConsoleKeyInfo Read()
        {
            ConsoleKeyInfo key = base.Read();

            if (key.Key == ConsoleKey.R && Program.Time > last_R + 200)
            {
                last_R = Program.Time;
                client.Ready = !client.Ready;
                UpdateItems();
                buffer.Clear();
                Draw();
            }

            return key;
        }
    }
}