using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsoleBufferApi;

namespace hunger_games_simulator.ui
{
    class SettingsMenu : Menu
    {
        const string Enabled = "TRUE", Disabled = "FALSE";
        int width = 40;
        static readonly string[] proitems = new string[] { "Fullscreen", "!", "Back" };

        public SettingsMenu()
            : base(proitems.ToArray())
        {
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

            if (k.Key == ConsoleKey.Enter && Selected != Items.Length - 1)
                k = new ConsoleKeyInfo();
            return k;
        }

        public void Show()
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
        }
    }
}
