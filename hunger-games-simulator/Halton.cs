using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace hunger_games_simulator
{
    class HaltonSet
    {
        int k2, k3;
        public HaltonSet(int seed)
        {
            Random rnd = new Random(seed);
            k2 = 2 * rnd.Next(10000) + 1;
            k3 = 3 * rnd.Next(10000) + 1 + rnd.Next(2);
        }

        public double Seq2(int i)
        {
            i *= k2;

            int b = 2;
            double r = 0;
            double f = 1;
            while (i > 0)
            {
                f /= b;
                r += f * (i % b);
                i /= b;
            }
            return r;
        }
        public double Seq3(int i)
        {
            i *= k3;

            int b = 3;
            double r = 0;
            double f = 1;
            while (i > 0)
            {
                f /= b;
                r += f * (i % b);
                i /= b;
            }
            return r;
        }
    }
}
