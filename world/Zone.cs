using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HungerGames.items;
using System.Drawing;

namespace HungerGames.world
{
    public class Zone
    {
        public Misto[] submista;
        public List<Item> itemy;
        public int[] mojeDlazdicky;
        public CharInfo[] dlazdickyInfo;
        public int Index;
        protected Biome ParentBiome;

        public Zone(Biome parent, int index)
        {
            this.ParentBiome = parent;
            this.Index = index;
            init();
        }
        void init()
        {
            itemy = new List<Item>();
            List<int> mojeDlaz_list = new List<int>();
            for (int i = 0; i < ParentBiome.tilesOwner.Length; i++)
            {
                if (ParentBiome.tilesOwner[i] == this.Index)
                    mojeDlaz_list.Add(i);
            }
            mojeDlazdicky = mojeDlaz_list.ToArray();
        }

        public virtual void generateTiles(Random rnd)
        {
            dlazdickyInfo = new CharInfo[mojeDlazdicky.Length];
        }
        protected void generateTiles(Random rnd, CharInfo[] list)
        {
            generateTiles(rnd);
            for (int i = 0; i < dlazdickyInfo.Length; i++)
            {
                dlazdickyInfo[i] = list[rnd.Next(list.Length)];
            }
        }
        public bool isFloodable(ref int point)
        {
            int min_index = 0, min_height = 256;
            for (int i = 0; i < this.mojeDlazdicky.Length; i++)
            {
                int index_in_biome = mojeDlazdicky[i];
                if (this.ParentBiome.heightMap[index_in_biome] < min_height)
                {
                    min_index = i;
                    min_height = this.ParentBiome.heightMap[index_in_biome];
                }
            }
            point = min_index;
            List<int> processed = new List<int>();
            List<int> toProcess = new List<int>();
            toProcess.Add(mojeDlazdicky[min_index]);
            while (toProcess.Count > 0)
            {
                int curr_glob_index = toProcess[0];
                toProcess.RemoveAt(0);
                processed.Add(curr_glob_index);

                int curr_height = ParentBiome.heightMap[curr_glob_index];
                int delta_h = 40;

                int x_in_biome = curr_glob_index % Biome._tiles_wide;
                int y_in_biome = curr_glob_index / Biome._tiles_wide;

                // UP
                if (y_in_biome > 0 && ParentBiome.heightMap[x_in_biome + (y_in_biome - 1) * Biome._tiles_wide] > curr_height - delta_h)
                {
                    int index_to_add = x_in_biome + (y_in_biome - 1) * Biome._tiles_wide;
                    if (mojeDlazdicky.Contains(index_to_add))
                        if (!(toProcess.Contains(index_to_add) || processed.Contains(index_to_add)))
                            toProcess.Add(index_to_add);
                        else
                            return false;
                }
                // LEFT
                if (x_in_biome > 0 && ParentBiome.heightMap[x_in_biome - 1 + y_in_biome * Biome._tiles_wide] > curr_height - delta_h)
                {
                    int index_to_add = x_in_biome - 1 + y_in_biome * Biome._tiles_wide;
                    if (mojeDlazdicky.Contains(index_to_add))
                        if (!(toProcess.Contains(index_to_add) || processed.Contains(index_to_add)))
                            toProcess.Add(index_to_add);
                        else
                            return false;
                }
                // DOWN
                if (y_in_biome < Biome._tiles_high - 1 && ParentBiome.heightMap[x_in_biome + (y_in_biome + 1) * Biome._tiles_wide] > curr_height - delta_h)
                {
                    int index_to_add = x_in_biome - 1 + y_in_biome * Biome._tiles_wide;
                    if (mojeDlazdicky.Contains(index_to_add))
                        if (!(toProcess.Contains(index_to_add) || processed.Contains(index_to_add)))
                            toProcess.Add(index_to_add);
                        else
                            return false;
                }
                // RIGHT
                if (x_in_biome < Biome._tiles_wide && ParentBiome.heightMap[x_in_biome + 1 + y_in_biome * Biome._tiles_wide] > curr_height - delta_h)
                {
                    int index_to_add = x_in_biome - 1 + y_in_biome * Biome._tiles_wide;
                    if (mojeDlazdicky.Contains(index_to_add))
                        if (!(toProcess.Contains(index_to_add) || processed.Contains(index_to_add)))
                            toProcess.Add(index_to_add);
                        else
                            return false;
                }
            }

            return true;
        }
    }
}
