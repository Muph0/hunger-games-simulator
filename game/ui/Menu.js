
function Menu(Console, game, itemlist) {
    var self = this;

    self.Selected = 0;
    self.HighlightFG = [255, 255, 255];
    self.HighlightBG = [80,0,0];
    self.Itemlist = itemlist;

    self.Draw = function()
    {
        var cur = Console.GetCursor();
        var curx = Console.CursorX;
        var cury = Console.CursorY;
        var stdFG = Console.Foreground;
        var stdBG = Console.Background;


        for (var i = 0; i < itemlist.length; i++)
        {
            Console.SetCursor(curx, cury + i);
            if (i === self.Selected)
            {
                Console.Foreground = self.HighlightFG;
                Console.Background = self.HighlightBG;
                Console.Write("> ");
            }
            else Console.Write("  ");

            // Don't draw leading '!'
            var option = itemlist[i];
            if (option[0] === '!')
                option = option.substring(1);

            Console.Write(option);

            Console.Foreground = stdFG;
            Console.Background = stdBG;
        }
    }

    self.Update = function()
    {

        //Cursor movement in menu
        do
        {
            if (game.IsKeyPressed(Keyboard.Keys.Up))
                self.Selected--;
            if (game.IsKeyPressed(Keyboard.Keys.Down))
                self.Selected++;
            self.Selected = (self.Selected + itemlist.length) % itemlist.length;
        }
        while (itemlist[self.Selected][0] === '!');
    }
}