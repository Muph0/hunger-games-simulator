using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsoleBufferApi;
using hunger_games_simulator.core;

namespace hunger_games_simulator.ui
{
    class SettingsMenu : Menu
    {
        const string Enabled = "TRUE", Disabled = "FALSE";
        static readonly string[] proitems = new string[] { "Fullscreen", "!", "Edit character...", "!", "Back" };

        public SettingsMenu()
            : base(proitems.ToArray())
        {
            width = 40;
            this.SelectedColor = ConsoleColor.DarkCyan;
            UpdateItems();
        }

        void UpdateItems()
        {
            string fullscreen_stat = ConsoleBuffer.Fullscreen ? Enabled : Disabled;
            Items[0] = proitems[0].PadRight(width - fullscreen_stat.Length - 2) + fullscreen_stat;
        }

        public override ConsoleKeyInfo Read()
        {
            ConsoleKeyInfo k = base.Read();

            if (Selected == 0)
            {
                if (k.Key == ConsoleKey.LeftArrow || k.Key == ConsoleKey.RightArrow || k.Key == ConsoleKey.Enter)
                {
                    ConsoleBuffer.ToggleFullscreen();
                }
            }

            UpdateItems();

            return k;
        }

        public void Show(GameClient client)
        {
            while (true)
            {
                buffer = new ConsoleBuffer();
                buffer.SetCursorPosition(10, 3);
                buffer.Write("Settings");
                buffer.SetCursorPosition(10, 4);
                buffer.ForegroundColor = ConsoleColor.DarkCyan;
                buffer.Write("".PadRight(width, '═'));
                buffer.ResetColor();

                buffer.SetCursorPosition(10, 6);
                this.ReadMenu();

                if (Selected == Items.Length - 3)
                {
                    new CharacterEditMenu(client).Show();
                }
                if (Selected == Items.Length - 1)
                    return;
            }
        }
    }
}
