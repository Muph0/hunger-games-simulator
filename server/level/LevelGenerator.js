
function LevelGenerator()
{
    this.Generate = function(seed, biome_count)
    {
        var width = 50;
        var height = 25;

        var arena = new Arena(width, height);
        arena.Biomes = [];
        arena.Seed = seed;

        var rnd = new Random(seed);
        var hlt = new HaltonSet(seed);


        // generate temperature map
        var heatmap = perlin.generatePerlinNoise(width, height);
        for (var i = 0; i < width * height; i++)
        {
            arena.Heatmap[i] = Math.floor((heatmap[i] - 0.5) * 20 + 0.8 * (i % width));
        }

        // decide which biome to use & set locations of biome pivots
        for (var i = 0; i < biome_count; i++)
        {
            var halton2 = hlt.Seq2(i+1);
            var halton3 = hlt.Seq3(i+1);

            var pivX = Math.floor(halton2 * width);
            var pivY = Math.floor(halton3 * height);

            var temp = arena.Heatmap[pivX + pivY * width];

            var biomes_filtered = Biome.Types.filter(function(val) {
                return temp >= new val(0,0).TempRange[0] && temp <= new val(0,0).TempRange[1];
            });

            var selected_type = rnd.choice(biomes_filtered);

            arena.Biomes[i] = new selected_type(pivX, pivY);
        }

        // calculate which tile belongs in which biome
        for (var i = 0; i < width * height; i++)
        {
            var tile_pos = [i % width, Math.floor(i / width)];

            var mindist2 = Math.sqrt(width*width + height*height);
            var owner = 0;

            // goes thru all the pivots and pick the closest to current tile
            for (var p = 0; p < biome_count; p++)
            {
                var pivot = [arena.Biomes[p].PivotX, arena.Biomes[p].PivotY];

                // add a bit of noise
                var noise = [(rnd.nextFloat() - 0.5) * 2.5, (rnd.nextFloat() - 0.5) * 2];
                pivot[0] += noise[0];
                pivot[1] += noise[1];

                var dist2 = tile_pos * tile_pos;
                if (dist2 < mindist2)
                {
                    mindist2 = dist2;
                    owner = p;
                }
            }


            arena.Biomes[owner].TilesOwned.push(i);

            arena.Tiles[i] = arena.Biomes[owner].GenerateTile(rnd);
        }

        return arena;
    }
}
LevelGenerator = new LevelGenerator();