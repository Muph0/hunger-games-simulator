
/**
 * @constructor
 */
function DesertBiome(pivX, pivY)
{
    inherit(this, new Biome(pivX, pivY));

    this.TempRange = [20, 60];

    this.Optionset = [
        { fg:[255, 0, 0], bg:[0, 0, 0], chars: "░▒▓" },
    ];

}