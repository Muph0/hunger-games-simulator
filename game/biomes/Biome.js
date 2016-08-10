
function Biome(pivX, pivY)
{
    var self = this;

    self.X = pivX;
    self.Y = pivY;

    self.TilesOwned = [];
    self.Optionset = [];
    self.TempRange = [];

    self.__defineGetter__('Optionset', function() { throw new Error("Not implemented."); });
    self.__defineGetter__('TempRange', function() { throw new Error("Not implemented."); });

    self.GenerateTile = function(seed)
    {
        var rnd = new Random(seed);

        var chr = rnd.choice(self.CharSet);
        var fg = rnd.choice(self.ForegroundSet);
        var bg = rnd.choice(self.BackgroundSet);

        var result = new Tile(chr, fg, bg);

        return result;
    }
}
Biome.RegisterBiomes = function()
{
    Biome.Types = [];

    Biome.Types.push(ForestBiome);
    Biome.Types.push(PlainsBiome);
    Biome.Types.push(DesertBiome);
    Biome.Types.push(TundraBiome);
}