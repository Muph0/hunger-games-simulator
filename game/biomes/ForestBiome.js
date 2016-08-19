
function ForestBiome(pivX, pivY)
{

    inherit(this, new Biome(pivX, pivY));

    this.TempRange = [10, 30];

    this.Optionset = [
        { fg:[255, 0, 0], bg:[0, 0, 0], chars: "░▒▓" },
    ];

}