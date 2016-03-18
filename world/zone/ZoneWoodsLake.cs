using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace HungerGames.world.zone
{
    public class ZoneWoodsLake : ZoneWoods
    {
        public ZoneWoodsLake(Biome parent, int index)
            : base(parent, index)
        {

        }

        public override void generateTiles(Random rnd)
        {
            base.generateTiles(rnd);
            float sumX = 0, sumY = 0;
            for (int i = 0; i < this.mojeDlazdicky.Length; i++)
            {
                sumX += mojeDlazdicky[i] % Biome._tiles_wide;
                sumY += mojeDlazdicky[i] / Biome._tiles_wide;
            }

            sumX /= mojeDlazdicky.Length;
            sumY /= mojeDlazdicky.Length;
            int biome_index = ((int)sumX) + ((int)sumY) * Biome._tiles_wide;

            int local_index = Array.IndexOf(mojeDlazdicky, biome_index);
            dlazdickyInfo[local_index].Back = Color.Blue;
        }
    }
}
