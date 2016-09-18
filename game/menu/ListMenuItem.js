

/**
 * @constructor
 */
function ListMenuItem(game, text, list)
{
    inherit(this, new MenuItem(game, text));

    this.Value = 0;
    this.Wrap = true;
    this.List = list;

    this.toString = function()
    {
        return this.List[this.Value];
    }

    this.Update = function()
    {
        if (game.IsKeyPressed(Keyboard.Keys.Enter) || game.IsKeyPressed(Keyboard.Keys.Right))
            this.Value++;
        if (game.IsKeyPressed(Keyboard.Keys.Left))
            this.Value--;

        if (this.Wrap)
        {
            var range = this.List.length;
            this.Value = (this.Value + range) % range;
        }
        else
        {
            this.Value = Math.max(0, this.Value);
            this.Value = Math.min(this.List.length - 1, this.Value);
        }
    }

    this.Draw = function(Console, menu)
    {
        var selected = this === menu.GetSelected();

        Console.Write(this.Text)
        var str_value = this.toString();

        var whitespace = "".padLeft(menu.Width - this.Text.length - str_value.length - (selected ? 4 : 2));
        Console.Write(whitespace);

        var FG = Console.Foreground;
        var fg2 = [60, 60, 60];

        if (selected)
        {
            if (game.IsKeyDown(Keyboard.Keys.Left)) Console.Foreground = fg2;
            Console.Write('< ');
        }

        Console.Foreground = FG;
        Console.Write(str_value);

        if (selected)
        {
            if (game.IsKeyDown(Keyboard.Keys.Right)) Console.Foreground = fg2;
            Console.Write(' >');
        }
        else
        {
            Console.Write('  ');
        }

        Console.Foreground = FG;
    }
}