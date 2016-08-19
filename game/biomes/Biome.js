
function Biome(pivX, pivY)
{
    this.X = pivX;
    this.Y = pivY;

    this.TilesOwned = [];
    this.Optionset = [];
    this.TempRange = [];

    this.GenerateTile = function(rnd)
    {
        var opt = rnd.choice(this.Optionset);

        var fg = opt.fg;
        var bg = opt.bg;
        var chr = rnd.choice(opt.chars.split(''));

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