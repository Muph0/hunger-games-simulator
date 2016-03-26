using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace hunger_games_simulator.core
{
    [Serializable]
    class PlayerCharacter
    {
        public string Name;

        int statPoints = 15;
        public int FreeStatPoints { get { return statPoints - Stats.Strength - Stats.Agility - Stats.Intelligence - Stats.Metabolic; } }
        public StatsSt Stats;
        public struct StatsSt
        {
            public int Strength, Agility, Intelligence, Metabolic;
            public int this[int i]
            {
                get
                {
                    switch (i)
                    {
                        case 0:
                            return Strength;
                        case 1:
                            return Agility;
                        case 2:
                            return Intelligence;
                        case 3:
                            return Metabolic;
                        default:
                            throw new Exception();
                    }
                }
                set
                {
                    switch (i)
                    {
                        case 0:
                            Strength = value;
                            break;
                        case 1:
                            Agility = value;
                            break;
                        case 2:
                            Intelligence = value;
                            break;
                        case 3:
                            Metabolic = value;
                            break;
                        default:
                            throw new Exception();
                    }
                }
            }
        }

        int skillPoints = 30;
        public int FreeSkillPoints { get { return skillPoints - Skills.Crafting - Skills.Archery - Skills.LightWep - Skills.HeavyWep - Skills.Cooking - Skills.Sneaking - Skills.Running - Skills.Climbing; } }
        public SkillsSt Skills;
        public struct SkillsSt
        {
            public int Crafting, Archery, LightWep, HeavyWep, Cooking, Sneaking, Running, Climbing;
            public int this[int i]
            {
                get
                {
                    switch (i)
                    {
                        case 0:
                            return Crafting;
                        case 1:
                            return Archery;
                        case 2:
                            return LightWep;
                        case 3:
                            return HeavyWep;
                        case 4:
                            return Cooking;
                        case 5:
                            return Sneaking;
                        case 6:
                            return Running;
                        case 7:
                            return Climbing;
                        default:
                            throw new Exception();
                    }
                }
                set
                {
                    switch (i)
                    {
                        case 0:
                            Crafting = value;
                            break;
                        case 1:
                            Archery = value;
                            break;
                        case 2:
                            LightWep = value;
                            break;
                        case 3:
                            HeavyWep = value;
                            break;
                        case 4:
                            Cooking = value;
                            break;
                        case 5:
                            Sneaking = value;
                            break;
                        case 6:
                            Running = value;
                            break;
                        case 7:
                            Climbing = value;
                            break;
                        default:
                            throw new Exception();
                    }
                }
            }
        }

        public int CanCarry
        {
            get
            {
                return 10 + Stats.Strength / 2;
            }
        }

        public static PlayerCharacter Random(Random rnd)
        {
            PlayerCharacter c = new PlayerCharacter();

            while (c.FreeStatPoints > 0)    
            {
                int i = rnd.Next(4);
                c.Stats[i] += 1;
            }

            while (c.FreeSkillPoints > 0)
                c.Skills[rnd.Next(8)] += 1;

            byte[] buf = new byte[8];
            rnd.NextBytes(buf);
            c.Name = "Nameless " + Convert.ToBase64String(buf);

            return c;
        }
    }
}
