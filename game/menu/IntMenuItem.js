
function IntMenuItem(game, text)
{
    inherit(this, new MenuItem(game, text));

    this.Value = 0;
    this.Min = 0;
    this.Max = Infinity;
    this.Wrap = false;

    var lastLeft = 0;
    var lastRight = 0;
    var lastBackspace = 0;

    this.toString = function()
    {
        return this.Value.toString();
    }

    this.OnSelect = function()
    {
        lastBackspace = lastRight = lastLeft = game.Time;
    }

    this.Update = function()
    {
        while (Keyboard.Buffer.length > 0)
        {
            var chr = Keyboard.Buffer.splice(0,1)[0];
            if (!isNaN(chr))
            {
                this.Value = this.Value * 10 + chr * 1;
            }
        }

        if (game.IsKeyPressed(Keyboard.Keys.Backspace))
        {
            this.Value = Math.floor(this.Value / 10);
            lastBackspace = game.Time;
        }
        if (game.IsKeyDown(Keyboard.Keys.Backspace) && game.Time > lastBackspace + 300)
            this.Value = Math.floor(this.Value / 10);


        if (game.IsKeyPressed(Keyboard.Keys.Enter) || game.IsKeyPressed(Keyboard.Keys.Right))
        {
            this.Value++;
            lastRight = game.Time;
        }
        if (game.IsKeyPressed(Keyboard.Keys.Left))
        {
            this.Value--;
            lastLeft = game.Time;
        }

        if (game.IsKeyDown(Keyboard.Keys.Right) && game.Time > lastRight + 300)
        {
            this.Value++;
            if (game.Time > lastRight + 3000)
                this.Value += 5;
        }
        if (game.IsKeyDown(Keyboard.Keys.Left) && game.Time > lastLeft + 300)
        {
            this.Value--;
            if (game.Time > lastLeft + 3000)
                this.Value -= 5;
        }

        if (this.Wrap)
        {
            var range = this.Max - this.Min;
            this.Value = (this.Value - this.Min + range) % range + this.Min;
        }
        else
        {
            this.Value = Math.max(this.Min, this.Value);
            this.Value = Math.min(this.Max, this.Value);
        }
    }

    this.Draw = function(Console, menu)
    {
        var selected = (menu.GetSelected() === this);
        Console.Write(this.Text);
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