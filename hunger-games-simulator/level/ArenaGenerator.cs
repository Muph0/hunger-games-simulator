using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using hunger_games_simulator.core;
using hunger_games_simulator.assets;

namespace hunger_games_simulator.level
{
    class ArenaGenerator
    {
        public static Arena Generate(GameAssets gameAssets, int seed, int biome_count)
        {
            // Prepare stuff
            Arena arena = new Arena(50, 25);
            arena.Biomes = new Biome[biome_count];
            List<int>[] biome_tiles_list = new List<int>[biome_count];

            // halton sequence is used for generating evenly distributed points
            Random rnd = new Random(seed);
            HaltonSet hlt = new HaltonSet(seed);

            // set locations of biome pivots
            for (int i = 0; i < biome_count; i++)
            {
                arena.Biomes[i] = new Biome((int)(hlt.Seq2(i + 1) * arena.Width), (int)(hlt.Seq3(i + 1) * arena.Height));
                biome_tiles_list[i] = new List<int>();
            }

            // calc which tile belongs in which biome
            for (int i = 0; i < arena.Tiles.Length; i++)
            {
                Point tile_pos = new Point(i % arena.Width, i / arena.Width);

                int minDist2 = 40 * 40;
                int owner = 0;

                // go thru all the pivots and pick the closest to current tile
                for (int p = 0; p < biome_count; p++)
                {
                    Point pivot = arena.Biomes[p].Pivot;
                    // add a bit of noise
                    pivot.X += (int)((rnd.NextDouble() - 0.5) * 2.5);
                    pivot.Y += (int)((rnd.NextDouble() - 0.5) * 2);

                    int dist2 = tile_pos.distanceSquared(pivot);
                    if (dist2 < minDist2)
                    {
                        minDist2 = dist2;
                        owner = p;
                    }
                }

                biome_tiles_list[owner].Add(i);
            }

            // assign the calculated tiles to actual biomes
            for (int i = 0; i < biome_count; i++)
            {
                arena.Biomes[i].TilesOwned = biome_tiles_list[i].ToArray();
                arena.Biomes[i].AssetName = gameAssets.PickBiomeAmountBased(rnd);
            }

            // time to POPULATE 
            //                 *biomes*
            for (int b = 0; b < biome_count; b++)
            {
                Biome biome = arena.Biomes[b];
                BiomeAsset biome_asset = gameAssets.BiomeAssets[biome.AssetName];

                for (int t = 0; t < biome.TilesOwned.Length; t++)
                {
                    Tile tile = biome_asset.GenerateTile(rnd);
                    arena.Tiles[biome.TilesOwned[t]] = tile;
                }
            }


            return arena;
        }
    }
}
