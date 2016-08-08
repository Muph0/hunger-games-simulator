
function Menu(itemlist, Console) {
    var self = this;
    var selected = 0;
    var kbs = [];
    var lkbs = [];
    self.HighlightFG = [255, 255, 255];
    self.HighlightBG = [80,0,0];

    self.Draw = function()
    {
        var cur = Console.GetCursor();
        var curx = Console.CursorX;
        var cury = Console.CursorX;
        var stdFG = Console.Foreground;
        var stdBG = Console.Background;

        for (var i = 0; i < itemlist.length; i++)
        {
            Console.SetCursor(curx, cury + i)

            if (i === selected)
            {
                Console.Foreground = self.HighlightFG;
                Console.Background = self.HighlightBG;
                Console.Write("> ");
            }
            else Console.Write("  ");

            Console.Write(itemlist[i]);

            Console.Foreground = stdFG;
            Console.Background = stdBG;
        }
    }

    self.Update = function()
    {
        //keyboard state
        lkbs = kbs;
        kbs = Keyboard.GetState();

        //Cursor movement in menu
        if (kbs[Keyboard.Keys.Up] && !lkbs[Keyboard.Keys.Up])
            selected--;
        if (kbs[Keyboard.Keys.Down] && !lkbs[Keyboard.Keys.Down])
            selected++;
        selected = (selected + itemlist.length) % itemlist.length;
    }
}