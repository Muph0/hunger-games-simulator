using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace HungerGames.world
{
    class BiomeGenerator
    {
        public static Biome Generate(Biome.Type biomeType, Random rnd)
        {
            Biome result = new Biome();

            int pocet_zon = rnd.Next() % 3 + 5;
            result.zony = new Zone[pocet_zon];

            Point[] pivoty = new Point[pocet_zon];
            if (pocet_zon == 5)
            {
                pivoty[0] = new Point(7 + rnd.Next() % 3, 2 + rnd.Next() % 3);
                pivoty[1] = new Point(19 + rnd.Next() % 3, 2 + rnd.Next() % 3);
                pivoty[2] = new Point(10 + rnd.Next() % 8, 8 + rnd.Next() % 5);
                pivoty[3] = new Point(7 + rnd.Next() % 3, 17 + rnd.Next() % 3);
                pivoty[4] = new Point(19 + rnd.Next() % 3, 17 + rnd.Next() % 3);
            }
            if (pocet_zon == 6)
            {
                pivoty[0] = new Point(3 + rnd.Next() % 5, 3 + rnd.Next() % 3);
                pivoty[1] = new Point(9 + rnd.Next() % 5, 9 + rnd.Next() % 3);
                pivoty[2] = new Point(15 + rnd.Next() % 5, 3 + rnd.Next() % 3);
                pivoty[3] = new Point(20 + rnd.Next() % 5, 9 + rnd.Next() % 3);
                pivoty[4] = new Point(3 + rnd.Next() % 5, 15 + rnd.Next() % 3);
                pivoty[5] = new Point(15 + rnd.Next() % 5, 15 + rnd.Next() % 3);
            }
            if (pocet_zon == 7)
            {
                pivoty[0] = new Point(5 + rnd.Next() % 5, 1 + rnd.Next() % 3);
                pivoty[1] = new Point(19 + rnd.Next() % 5, 1 + rnd.Next() % 3);
                pivoty[2] = new Point(1 + rnd.Next() % 5, 9 + rnd.Next() % 3);
                pivoty[3] = new Point(12 + rnd.Next() % 5, 9 + rnd.Next() % 3);
                pivoty[4] = new Point(22 + rnd.Next() % 5, 9 + rnd.Next() % 3);
                pivoty[5] = new Point(5 + rnd.Next() % 5, 17 + rnd.Next() % 3);
                pivoty[6] = new Point(19 + rnd.Next() % 5, 17 + rnd.Next() % 3);
            }

            int[] dlazdicky_owner = new int[28 * 20];
            for (int i = 0; i < dlazdicky_owner.Length; i++)
            {
                int posX = i % 28, posY = i / 28;
                int nej_pivot = 0;
                double nej_dist = double.MaxValue;

                for (int p = 0; p < pocet_zon; p++)
                {
                    int X = pivoty[p].X - posX + (int)(rnd.NextDouble() * 2.5);
                    int Y = pivoty[p].Y - posY + (int)(rnd.NextDouble() * 2);
                    double dist = Math.Sqrt(X * X + Y * Y);

                    if (dist < nej_dist)
                    {
                        nej_dist = dist;
                        nej_pivot = p;
                    }
                }

                dlazdicky_owner[i] = nej_pivot;
            }
            result.tilesOwner = dlazdicky_owner;

            switch (biomeType)
            {
                case Biome.Type.Woods:
                    for (int i = 0; i < pocet_zon; i++)
                    {
                        int sel = rnd.Next(2);
                        if (sel == 0)
                        {
                            result.zony[i] = new zone.ZoneWoods(result, i);
                        }
                        if (sel == 1)
                        {
                            result.zony[i] = new zone.ZoneWoodsLake(result, i);
                        }
                        result.zony[i].generateTiles(rnd);
                    }
                    break;
            }

            return result;
        }
    }
}
