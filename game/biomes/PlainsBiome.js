
function PlainsBiome(pivX, pivY)
{
    var self = this;
    self.__proto__ = new BiomeAsset(pivX, pivY);

    self.TempRange = [10, 30];

    self.Optionset = [
        { fg:[255, 0, 0], bg:[0, 0, 0], chr: "░▒▓" },
    ];
}