
/**
 * @constructor
 */
function Menu(Console, game, width) {
    var self = this;

    this.Selected = 0;
    this.HighlightFG = [255, 255, 255];
    this.HighlightBG = [100,0,0];
    this.Itemlist = [];
    this.Width = width;
    this.MaxHeight = Infinity;
    this.ViewPortPos = 0;
    this.HighlightSelected = true;
    this.Spacing = 1;

    this.GetSelected = function()
    {
        return this.Itemlist[this.Selected];
    }

    this.Show = function()
    {
        this.Itemlist[this.Selected].OnSelect();
    }

    this.Draw = function()
    {
        if (this.Itemlist.length === 0)
            return Console.Write("[empty menu]");

        while (this.Itemlist[this.Selected].Skip) this.Selected++;

        var height = this.Itemlist.length;
        if (this.MaxHeight !== Infinity)
        {
            height = Math.min(this.MaxHeight, this.Itemlist.length);
        }

        var X = Console.getCursorX();
        var Y = Console.getCursorY();

        for (var i = 0; i < height; i++)
        {
            var item = this.Itemlist[i + this.ViewPortPos];
            if (this.HighlightSelected && i + this.ViewPortPos === this.Selected || (item.Text === '' && item.Skip)) continue;

            Console.setCursor(X, Y + i * this.Spacing);
            Console.Write('  ');

            item.Draw(Console, this);
        }

        if (this.HighlightSelected && this.ViewPortPos <= this.Selected && this.Selected < this.ViewPortPos + height)
        {
            var FG = Console.Foreground;
            var BG = Console.Background;
            Console.Foreground = this.HighlightFG;
            Console.Background = this.HighlightBG;

            Console.setCursor(X, Y + (this.Selected - this.ViewPortPos) * this.Spacing);
            Console.Write("> ");

            this.GetSelected().Draw(Console, this);
            if (!(this.GetSelected() instanceof StringMenuItem))
                Console.CursorVisible = false;

            Console.Foreground = FG;
            Console.Background = BG;
        }
    }

    this.Update = function()
    {
        if (this.Itemlist.length === 0)
            return;

        var old_sel = this.Selected;

        //Cursor movement in menu
        do
        {
            if (game.IsKeyPressed(Keyboard.Keys.Up))
            {
                this.Selected--;
            }
            if (game.IsKeyPressed(Keyboard.Keys.Down))
            {
                this.Selected++;
            }
            this.Selected = (this.Selected + this.Itemlist.length) % this.Itemlist.length;
        }
        while (this.Itemlist[this.Selected].Skip);

        if (this.MaxHeight !== Infinity && this.Selected >= 0)
        {
            if (this.Selected < this.ViewPortPos)
                this.ViewPortPos--;
            if (this.Selected >= this.ViewPortPos + this.MaxHeight)
                this.ViewPortPos++;
        }

        if (old_sel !== this.Selected)
            this.Itemlist[this.Selected].OnSelect();

        this.Itemlist[this.Selected].Update();
    }
}