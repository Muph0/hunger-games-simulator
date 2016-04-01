using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using hunger_games_simulator.core;

namespace hunger_games_simulator.ui
{
    class LobbyMenu : Menu
    {
        static string[] proitems = new string[] { };

        GameClient client;
        public LobbyMenu(GameClient client)
            : base(proitems)
        {
            this.SelectedColor = ConsoleColor.White;
            this.fillBackround = false;
            this.client = client;
            width = 40;
        }

        void UpdateItems()
        {
            Items = new string[client.ServerInfo.Playerlist.Length * 2 + 2];
            Items[0] = "!" + client.ServerInfo.GameName + " hosted on " + client.ServerEp.Address.ToString();
            Items[1] = "!" + "".PadRight(40, '-');

            for (int i = 0; i < client.ServerInfo.Playerlist.Length; i++)
            {
                Items[2 * i + 2] = (i + 1) + ". " + client.ServerInfo.Playerlist[i].PlayerName;
                Items[2 * i + 3] = "!";
            }
        }

        public void Show()
        {
            buffer.SetCursorPosition(6, 3);
            UpdateItems();
            this.ReadMenu();
        }
    }
}
