using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Muphcode.Xna2D;

namespace HungerGames.world.zone
{
    public class ZoneWoods : Zone
    {
        public ZoneWoods(Biome parent, int index)
            : base(parent, index)
        {

        }
        public override void generateTiles(Random rnd)
        {
            base.generateTiles(rnd);
            string chars = "↑`'.!|\",░▒▓";
            Color[] fcolors = new Color[]
            {
                Color.FromArgb(0x429E24),
                Color.FromArgb(0x266600),
                Color.FromArgb(0x57A80F),
                Color.FromArgb(0x384502),
                Color.FromArgb(0x663300),
                Color.FromArgb(0xC2850A),
                Color.FromArgb(0xC78238),
                Color.FromArgb(0x5EAB08),
                Color.FromArgb(0x384502),
            };

            double h = rnd.NextDouble() * .2 + 0.2;
            double s = rnd.NextDouble() * .2 + 0.65;
            double l = rnd.NextDouble() * .2 + .2;

            for (int i = 0; i < dlazdickyInfo.Length; i++)
            {
                Color fore = fcolors[rnd.Next(fcolors.Length)];
                Color back;

                back = new HSLColor(rnd.NextDouble() * 0.0 + h, rnd.NextDouble() * .0 + s, rnd.NextDouble() * .0 + l);
                
                char c = chars[rnd.Next(chars.Length)];
                dlazdickyInfo[i] = new CharInfo(fore, back, c);
            }
        }
    }
}
