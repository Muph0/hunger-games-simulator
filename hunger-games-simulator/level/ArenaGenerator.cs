using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using hunger_games_simulator.core;
using hunger_games_simulator.assets;
using hunger_games_simulator.assets.info;

namespace hunger_games_simulator.level
{
    class ArenaGenerator
    {
        public static Arena Generate(GameAssets gameAssets, int seed, int biome_count)
        {
            // Prepare stuff
            Arena arena = new Arena(50, 25);
            arena.Biomes = new Biome[biome_count];
            arena.Seed = seed;
            List<int>[] biome_tiles_list = new List<int>[biome_count];

            // halton sequence is used for generating evenly distributed points
            Random rnd = new Random(seed);
            HaltonSet hlt = new HaltonSet(seed);

            // set locations of biome pivots
            for (int i = 0; i < biome_count; i++)
            {
                double halton2 = hlt.Seq2(i + 1);
                double halton3 = hlt.Seq3(i + 1);

                arena.Biomes[i] = new Biome((int)(halton2 * arena.Width), (int)(halton3 * arena.Height));
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
                    Point noise = new Point((int)((rnd.NextDouble() - 0.5) * 2.5), (int)((rnd.NextDouble() - 0.5) * 2));
                    Point pivot = new Point(arena.Biomes[p].Pivot.X, arena.Biomes[p].Pivot.Y) + noise;
                    // add a bit of noise


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

            // time to populate   *BIOMES*
            for (int b = 0; b < biome_count; b++)
            {
                Biome biome = arena.Biomes[b];
                BiomeAsset biome_asset = gameAssets.BiomeAssets[biome.AssetName];

                List<TileAsset> special_tiles = new List<TileAsset>();
                foreach (TileAsset t in gameAssets.TileAssets.Values)
                {
                    if (t.SpawnLocations.Length > 0 && 
                        t.SpawnLocations.Where(a => a.Name == biome_asset.Name).Count() > 0)
                    {
                        special_tiles.Add(t);
                    }
                }

                for (int t = 0; t < biome.TilesOwned.Length; t++)
                {
                    Tile tile = biome_asset.GenerateTile(rnd);

                    // test, if current tile shouldn't be special tile
                    foreach (TileAsset ta in special_tiles)
                    {
                        SpawnLocation local = ta.SpawnLocations.Where(a => a.Name == biome_asset.Name).First();

                        if (local.Min < local.Max)
                        {
                            // SOME HARDCORE MATH SHIT HERE

                            // approx. how many tiles i want in this biome
                            int desired_amount = rnd.Next(local.Min, local.Max);

                            // what is the perfect probability to get the desired amount
                            double desired_probability = desired_amount / (double)biome.TilesOwned.Length;

                            // roll a dice of (1/des_prob) sides
                            int dice = rnd.Next((int)(1 / desired_probability));

                            if (dice == 1)
                            {
                                //tile.
                            }
                        }
                    }

                    arena.Tiles[biome.TilesOwned[t]] = tile;
                }
            }

            // time to populate   *TILES*
            for (int i = 0; i < arena.Tiles.Length; i++)
            {

            }

            return arena;
        }
    }
}
