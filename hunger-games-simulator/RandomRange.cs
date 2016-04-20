using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace hunger_games_simulator
{
    class RandomRange
    {
        public int Min, Max;

        public int Evaluate(Random rnd)
        {
            if (Min < Max)
            {
                int desired_amount = rnd.Next(Min, Max);

                return desired_amount;
            }
            else
            {
                int dice = rnd.Next(Min);
                if (dice < Max)
                    return 1;
            }

            return 0;
        }
    }
}
