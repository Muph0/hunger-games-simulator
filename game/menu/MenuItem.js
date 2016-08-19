
function MenuItem(game, text)
{
    this.Text = text;
    this.Skip = false;

    this.OnSelect = function(){};
    this.Update = function(){};

    this.Draw = function(Console, menu)
    {
        Console.Write(this.Text);

        if (typeof this.Value !== 'undefined')
        {
            var str = this.toString();
            str = str.padLeft(menu.Width - this.Text.length);
            Console.Write(str);
        }
    }
}