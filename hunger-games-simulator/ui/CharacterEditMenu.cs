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

        PlayerCharacter character;
        public CharacterEditMenu(PlayerCharacter character)
            : base(proitems)
        {
            width = 35;
            this.character = character;
            this.SelectedColor = ConsoleColor.White;
            this.fillBackround = false;
        }

        int o1 = 3;
        int o2 = 9;
        public void UpdateItems()
        {
            Items[2] = "!◘◘2" + (proitems[2] + " ◘a◘" + character.FreeStatPoints).PadRight(width + 3);
            Items[8] = "!◘◘6" + (proitems[8] + " ◘a◘" + character.FreeSkillPoints).PadRight(width + 3);

            for (int i = 0; i < 4; i++)
                Items[i + o1] = proitems[i + o1] + character.Stats[i].ToString().PadLeft(width - proitems[i + o1].Length);
            for (int i = 0; i < 8; i++)
                Items[i + o2] = proitems[i + o2] + character.Skills[i].ToString().PadLeft(width - proitems[i + o2].Length);
        }

        public void Show()
        {
            while (true)
            {
                buffer.SetCursorPosition(30, 2);
                UpdateItems();
                this.ReadMenu();
            }
        }

        public override ConsoleKeyInfo Read()
        {
            ConsoleKeyInfo key = base.Read();
            bool redraw = false;

            if (this.StringSetting(0, key, width - 7, ref character.Name))
                redraw = true;

            // STATS
            for (int i = 0; i < 4; i++)
            {
                int stat = character.Stats[i];
                if (NumberLRSetting(o1 + i, key, 0, character.FreeStatPoints + stat+1, ref stat))
                {
                    redraw = true;
                    character.Stats[i] = stat;
                }
            }

            // SKILLS
            for (int i = 0; i < 8; i++)
            {
                int skill = character.Skills[i];
                if (NumberLRSetting(o2 + i, key, 0, character.FreeSkillPoints + skill+1, ref skill))
                {
                    redraw = true;
                    character.Skills[i] = skill;
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
