/*
* This file features algorithm copied from http://mo.mff.cuni.cz/p/64/src/p11.c
* 
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace hunger_games_simulator
{
    class TextBreaker
    {
        private TextBreaker()
        { }

        struct Slovo
        {
            public string sl;
            public int delka;
            public double cena_zlomu;
            public int predchozi_zlom;
            public bool zlomit;
        }

        Slovo[] text = new Slovo[10000];
        int pocet_slov;

        StringBuilder result = new StringBuilder();
        int delka_radku;

        void vypis(int posledni_zlom)
        {
            int z = posledni_zlom, i;
            string sep = "";

            while (z > 0)
            {
                text[z].zlomit = true;
                z = text[z].predchozi_zlom;
            }

            for (i = 0; i < pocet_slov; i++)
            {
                if (text[i].zlomit)
                    sep = "\n";
                result.Append(sep + text[i].sl);
                sep = " ";
            }
            result.Append("\n");
        }

        double chyba_radku(int znaku, int mezer)
        {
            double delka_mezery = (delka_radku - znaku + mezer) / mezer;
            double chyba_mezery = delka_mezery - 1;
            return mezer * chyba_mezery * chyba_mezery;
        }


        void nejmensi_chyba(int pred)
        {
            int znaku = text[pred - 1].delka;
            int mezer = 0;
            double nejlepsi_cena = double.PositiveInfinity, akt_cena;
            int nejlepsi_zlom = -1, apred;

            for (apred = pred - 2; apred >= 0; apred--)
            {
                znaku += 1 + text[apred].delka;
                if (znaku > delka_radku)
                    break;
                mezer++;
                akt_cena = text[apred].cena_zlomu + chyba_radku(znaku, mezer);
                if (akt_cena < nejlepsi_cena)
                {
                    nejlepsi_cena = akt_cena;
                    nejlepsi_zlom = apred;
                }
            }

            text[pred].cena_zlomu = nejlepsi_cena;
            text[pred].predchozi_zlom = nejlepsi_zlom;
        }

        public static string BreakText(string s, int line_len)
        {
            string[] paragraphs = s.Split('\n');

            for (int i = 0; i < paragraphs.Length; i++)
            {
                paragraphs[i] = new TextBreaker().Break(paragraphs[i], line_len);
            }

            paragraphs[paragraphs.Length - 1] = paragraphs.Last().TrimEnd("\n ".ToArray());

            return string.Join("", paragraphs);
        }

        public static string BreakParagraph(string s, int line_len)
        {
            return new TextBreaker().Break(s, line_len);
        }

        string Break(string str, int line_len)
        {
            str = str.Replace('\n', ' ');
            string[] buf = str.Split(' ').ToArray();
            int i, posledni, nejlepsi_zlom;
            double nejlepsi_cena;
            delka_radku = line_len;

            pocet_slov = 0;

            while (pocet_slov < buf.Length)
            {
                text[pocet_slov].sl = buf[pocet_slov];
                text[pocet_slov].delka = buf[pocet_slov].Length;
                pocet_slov++;
            }

            text[0].cena_zlomu = 0;
            text[1].cena_zlomu = double.PositiveInfinity;
            for (i = 2; i < pocet_slov; i++)
                nejmensi_chyba(i);

            nejlepsi_cena = text[pocet_slov - 1].cena_zlomu;
            nejlepsi_zlom = pocet_slov - 1;
            posledni = text[pocet_slov - 1].delka;
            for (i = pocet_slov - 2; i >= 0; i--)
            {
                posledni += 1 + text[i].delka;
                if (posledni > delka_radku)
                    break;
                if (text[i].cena_zlomu < nejlepsi_cena)
                {
                    nejlepsi_cena = text[i].cena_zlomu;
                    nejlepsi_zlom = i;
                }
            }

            vypis(nejlepsi_zlom);

            return result.ToString();
        }
    }
}
