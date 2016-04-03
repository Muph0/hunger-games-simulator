using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace hunger_games_simulator.core
{
    [Serializable]
    class PlayerCharacter
    {
        public PlayerCharacter()
        {
            this.Name = "";
            this.Stats = new int[4];
            this.Skills = new int[8];
        }

        public string Name;

        int statPoints = 15;
        public int FreeStatPoints { get { return statPoints - Stats.Sum(); } }
        public int[] Stats;

        public int StrengthStat
        { get { return Stats[0]; } set { Stats[0] = value; } }
        public int AgilityStat
        { get { return Stats[1]; } set { Stats[1] = value; } }
        public int IntelligenceStat
        { get { return Stats[2]; } set { Stats[2] = value; } }
        public int MetabolicStat
        { get { return Stats[3]; } set { Stats[3] = value; } }


        int skillPoints = 30;
        public int FreeSkillPoints { get { return skillPoints - Skills.Sum(); } }
        public int[] Skills;

        public int CraftingSkill
        { get { return Skills[0]; } set { Skills[0] = value; } }
        public int ArcherySkill
        { get { return Skills[1]; } set { Skills[1] = value; } }
        public int LightWepSkill
        { get { return Skills[2]; } set { Skills[2] = value; } }
        public int GunSkill
        { get { return Skills[3]; } set { Skills[3] = value; } }
        public int CookingSkill
        { get { return Skills[4]; } set { Skills[4] = value; } }
        public int SneakingSkill
        { get { return Skills[5]; } set { Skills[5] = value; } }
        public int RunningSkill
        { get { return Skills[6]; } set { Skills[6] = value; } }
        public int ClimbingSkill
        { get { return Skills[7]; } set { Skills[7] = value; } }


        public int CanCarry
        {
            get
            {
                return 10 + StrengthStat / 2;
            }
        }

        public void Randomize()
        {
            Random rnd = new System.Random((int)Program.Time);

            for (int i = 0; i < 4; i++)
                Stats[i] = 0;
            for (int i = 0; i < 8; i++)
                Skills[i] = 0;

            while (FreeStatPoints > 0)
            {
                int i = rnd.Next(4);
                Stats[i] += 1;
            }

            while (FreeSkillPoints > 0)
                Skills[rnd.Next(8)] += 1;

            int cap = (int)Math.Pow(10, 7);
            Name = "Nameless " + (rnd.Next(cap / 10 * 9) + cap / 10);
        }

        public override string ToString()
        {
            string result = "";
            string[] stat_names = new string[] { "Strong", "Agile", "Inteligent", "Undemanding" };
            string[] skill_names = new string[] { "Craftsman", "Archer", "Swordsman", "Gunman", "Cook", "Sneak", "Runner", "Climber" };

            int i = Stats.OrderByDescending(x => x).First();
            i = Array.IndexOf(Stats, i);
            result = stat_names[i] + ", ";

            i = Stats.OrderByDescending(x => x).Skip(1).First();
            i = Array.LastIndexOf(Stats, i);
            result += stat_names[i].ToLower() + " ";

            i = Skills.Max();
            i = Array.IndexOf(Skills, i);
            result += skill_names[i].ToLower();

            return result;
        }

        public void WriteToFile(string filename)
        {
            IFormatter f = new BinaryFormatter();
            FileStream stream = new FileStream(filename, FileMode.Create);
            f.Serialize(stream, this);
            stream.Close();
        }
        public static PlayerCharacter ReadFromFile(string filename)
        {
            IFormatter f = new BinaryFormatter();
            FileStream stream = new FileStream(filename, FileMode.Open);
            object o = f.Deserialize(stream);
            stream.Close();

            return (PlayerCharacter)o;
        }
    }
}
