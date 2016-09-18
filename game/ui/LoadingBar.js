
/**
 * @constructor
 */
function LoadingBar(Console, game, width)
{
    this.Percent = 0;
    this.Foreground = Console.Foreground;
    this.Background = Console.Background;
    this.Width = width;

    this.Update = function()
    {

    }

    this.Draw = function()
    {
        var fg = Console.Foreground;
        var bg = Console.Background;

        if (this.Percent < 0)
        {
            var X = Console.getCursorX();
            var Y = Console.getCursorY();

            var x_offset = Math.floor(width * (Math.sin(game.Time / 200) + 1) / 2);

            Console.setCursor(X + x_offset, Y);
            Console.Foreground = Color.FromHSL((game.Time/2000) % 1, 1, 0.5);
            Console.Write("x");
        }
        else
        {
            var middle = this.Percent + '%';
            var str = new Array(Math.floor(width / 2) - 1).join(" ");
            var result = (middle + str).padLeft(width);

            for (var i = 0; i < result.length; i++)
            {
                if (i / result.length < this.Percent / 100)
                {
                    Console.Foreground = this.Background;
                    Console.Background = this.Foreground;
                }
                else
                {
                    Console.Foreground = this.Foreground;
                    Console.Background = this.Background;
                }

                Console.Write(result[i]);
            }
        }

        Console.Foreground = fg;
        Console.Background = bg;
    }
}