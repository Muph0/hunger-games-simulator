
function TundraBiome(pivX, pivY)
{
    inherit(this, new Biome(pivX, pivY));

    this.TempRange = [-30, 5];

    this.Optionset = [
        { fg:[255, 0, 0], bg:[0, 0, 0], chars: "░▒▓" },
    ];

}