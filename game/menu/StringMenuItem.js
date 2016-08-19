
function StringMenuItem(game, text)
{
    inherit(this, new MenuItem(game, text));

    this.Value = "";
    this.DisplayLength = 10;
    this.CursorPos = 0;
    this.Wrap = false;

    var lastLeft = 0;
    var lastRight = 0;
    var lastBackspace = 0;

    this.ClampedValue = function(length)
    {
        if (typeof length === 'undefined' || this.Value.length < length)
            return this.Value;

        var half = Math.floor(length / 2);

        var start = Math.min(Math.max(0, this.CursorPos - half), this.Value.length - length);
        return this.Value.substr(start, length);
    }

    this.toString = function(length)
    {
        return this.ClampedValue(length);
    }

    this.OnSelect = function()
    {
        Keyboard.Buffer = [];
    }

    this.Update = function()
    {
        while (Keyboard.Buffer.length)
        {
            var chr = Keyboard.Buffer.splice(0,1)[0];
            if (chr.length === 1)
            {
                this.Value = this.Value.substring(0, this.CursorPos) + chr + this.Value.substring(this.CursorPos);
                this.CursorPos++;
            }
        }

        if (game.IsKeyPressed(Keyboard.Keys.Backspace) && this.CursorPos > 0)
        {
            this.Value = this.Value.substring(0, this.CursorPos - 1) + this.Value.substring(this.CursorPos);
            this.CursorPos--;
            lastBackspace = game.Time;
        }
        if (game.IsKeyDown(Keyboard.Keys.Backspace) && game.Time > lastBackspace + 300)
        {
            this.Value = this.Value.substring(0, this.CursorPos - 1) + this.Value.substring(this.CursorPos);
            this.CursorPos--;
        }

        if (game.IsKeyPressed(Keyboard.Keys.Enter) || game.IsKeyPressed(Keyboard.Keys.Right))
        {
            this.CursorPos++;
            lastRight = game.Time;
        }
        if (game.IsKeyPressed(Keyboard.Keys.Left))
        {
            this.CursorPos--;
            lastLeft = game.Time;
        }
        if (game.IsKeyDown(Keyboard.Keys.Right) && game.Time > lastRight + 300)
            this.CursorPos++;
        if (game.IsKeyDown(Keyboard.Keys.Left) && game.Time > lastLeft + 300)
            this.CursorPos--;


        if (this.Wrap && this.Value.length > 0)
        {
            this.CursorPos = (this.CursorPos + this.Value.length + 1) % (this.Value.length + 1);
        }
        else
        {
            this.CursorPos = Math.max(this.CursorPos, 0);
            this.CursorPos = Math.min(this.CursorPos, this.Value.length);
        }
    }

    this.Draw = function(Console, menu)
    {
        var selected = menu.GetSelected() === this;
        Console.Write(this.Text + " ");

        var viewport = menu.Width - 1 - this.Text.length;
        var half = Math.floor(viewport / 2);
        Console.Write(this.toString(viewport).padLeft(viewport))

        if (selected)
        {
            if (this.toString(viewport).length !== viewport)
            {
                Console.CursorX -= this.Value.length - this.CursorPos;
            }
            else
            {
                if (this.CursorPos > half)
                    Console.CursorX -= Math.min(this.Value.length - this.CursorPos, half);
                else
                    Console.CursorX -= viewport - this.CursorPos;
            }
            Console.CursorVisible = true;
        }
    }
}