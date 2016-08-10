
function TundraBiome(pivX, pivY)
{
    var self = this;
    self.__proto__ = new BiomeAsset(pivX, pivY);

    self.TempRange = [-20, 10];

    self.Optionset = [
        { fg:[255, 0, 0], bg:[0, 0, 0], chr: "░▒▓" },
    ];
}