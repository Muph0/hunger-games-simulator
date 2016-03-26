using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using hunger_games_simulator.core;

namespace hunger_games_simulator.ui
{
    class CharacterEditMenu : Menu
    {
        static string[] proitems = new string[] 
        { 
            "Name:", "!",
            "Stat points left:",
            "Strength", "Agility", "Inteligence", "Metabolic", 
            "!", "Skill points left:",
            "Crafting", "Archery", "Light weapons", "Heavy weapons", "Cooking", "Sneaking", "Running", "Climbing",
            "!", "Random", "Done"
        };

        GameClient client;
        public CharacterEditMenu(GameClient client)
            : base(proitems)
        {
            width = 35;
            this.client = client;
            this.SelectedColor = ConsoleColor.White;
            this.fillBackround = false;
        }

        int o1 = 3;
        int o2 = 9;
        public void UpdateItems()
        {
            Items[0] = proitems[0] + client.Character.Name.PadLeft(width - proitems[0].Length);
            Items[2] = "!◘◘2" + (proitems[2] + " ◘a◘" + client.Character.FreeStatPoints).PadRight(width + 3);
            Items[8] = "!◘◘6" + (proitems[8] + " ◘a◘" + client.Character.FreeSkillPoints).PadRight(width + 3);

            for (int i = 0; i < 4; i++)
                Items[i + o1] = proitems[i + o1] + client.Character.Stats[i].ToString().PadLeft(width - proitems[i + o1].Length);
            for (int i = 0; i < 8; i++)
                Items[i + o2] = proitems[i + o2] + client.Character.Skills[i].ToString().PadLeft(width - proitems[i + o2].Length);
        }

        public void Show()
        {
            while (true)
            {
                buffer.SetCursorPosition(30, 2);
                UpdateItems();
                this.ReadMenu();

                if (Selected == Items.Length - 2)
                    client.Character.Randomize();
                if (Selected == Items.Length - 1)
                    return;
            }
        }

        public override ConsoleKeyInfo Read()
        {
            ConsoleKeyInfo key = base.Read();
            bool redraw = false;

            if (this.StringSetting(0, key, width - 7, ref client.Character.Name))
                redraw = true;

            // STATS
            for (int i = 0; i < 4; i++)
            {
                int stat = client.Character.Stats[i];
                if (NumberLRSetting(o1 + i, key, 0, client.Character.FreeStatPoints + stat + 1, ref stat))
                {
                    redraw = true;
                    client.Character.Stats[i] = stat;
                }
            }

            // SKILLS
            for (int i = 0; i < 8; i++)
            {
                int skill = client.Character.Skills[i];
                if (NumberLRSetting(o2 + i, key, 0, client.Character.FreeSkillPoints + skill + 1, ref skill))
                {
                    redraw = true;
                    client.Character.Skills[i] = skill;
                }
            }

            if (redraw)
            {
                UpdateItems();
            }

            return key;
        }
    }
}
