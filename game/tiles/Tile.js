
function Tile(chr, foreground, background)
{
    var self = this;
    self.Char = chr;
    self.Foreground = foreground;
    self.Background = background;
    self.Entities = [];

    self.Asset = null;
}
