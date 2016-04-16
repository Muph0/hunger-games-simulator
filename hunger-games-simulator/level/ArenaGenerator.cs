using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using hunger_games_simulator.core;
using hunger_games_simulator.assets;
using hunger_games_simulator.assets.info;
using System.Drawing;

namespace hunger_games_simulator.level
{
    class ArenaGenerator
    {
        public const int WIDTH = 50, HEIGHT = 25;
        public static Arena Generate(GameAssets gameAssets, int seed, int biome_count)
        {
            // Prepare stuff
            Arena arena = new Arena(WIDTH, HEIGHT);
            arena.Biomes = new Biome[biome_count];
            arena.Seed = seed;
            List<int>[] biome_tiles_list = new List<int>[biome_count];
            // halton sequence is used for generating evenly distributed points
            Random rnd = new Random(seed);
            HaltonSet hlt = new HaltonSet(seed);

            // generate heatmap
            double[] heatmap = GenerateNoise(seed, 3);
            for (int i = 0; i < arena.Heatmap.Length; i++)
            {
                arena.Heatmap[i] = (int)((heatmap[i] - 0.5) * 30 - 5 + 0.9 * (i % arena.Width));
            }

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
                int pivotPos = arena.Biomes[i].Pivot.X + arena.Biomes[i].Pivot.Y * arena.Width;

                arena.Biomes[i].TilesOwned = biome_tiles_list[i].ToArray();
                arena.Biomes[i].AssetName = gameAssets.PickBiomeAmountBased(rnd, arena.Heatmap[pivotPos]);
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

        public static double[] GenerateNoise(int seed, int depth, int startDepth = 1)
        {
            string name = "heatmaps/heatmap-" + seed;
            Random rnd = new Random(seed);

            // Function that returns bitmap with grayscale noise
            Func<Random, int, int, Bitmap> getNoise = new Func<Random, int, int, Bitmap>(delegate(Random r, int side, int alpha)
            {
                Bitmap res = new Bitmap(side * WIDTH / HEIGHT, side);
                for (int i = 0; i < res.Width * res.Height; i++)
                {
                    byte clr = (byte)r.Next(256);
                    res.SetPixel(i % res.Width, i / res.Width, Color.FromArgb(alpha, clr, clr, clr));
                }
                return res;
            });

            Bitmap result_bmp = new Bitmap(WIDTH, HEIGHT);
            for (int level = 0; level < depth; level++)
            {
                // prepare to draw on result buffer
                Graphics rg = Graphics.FromImage(result_bmp);

                // calculate properities of new layer
                int side = (int)Math.Pow(2, level + startDepth);
                int alpha = 255 / (level + 1);
                // generate new layer of noise
                Bitmap new_layer = getNoise(rnd, side, alpha);
                //new_layer.Save(name + "-lvl" + level + ".bmp");

                // draw the new layer onto the result buffer
                rg.DrawImage(new_layer, new Rectangle(0, 0, result_bmp.Width, result_bmp.Height));
            }

            //result_bmp.Save(name + ".bmp");

            double[] result = new double[WIDTH * HEIGHT];
            for (int i = 0; i < result.Length; i++)
                result[i] = result_bmp.GetPixel(i % WIDTH, i / WIDTH).R / 255.0;

            return result;
        }
    }
}
