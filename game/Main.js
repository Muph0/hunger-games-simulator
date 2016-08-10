
function main(body)
{
    var img = document.getElementById('ascii');

    var Console = new CanvasConsole(80, 25, img);
    Console.CreateCanvas();

    // Keep the canvas in the middle
    var resize_adjust = function() {
        var winW = $(window).width(), winH = $(window).height();

        var canvas = $("canvas").first();
        var aspect_ratio = canvas.width() / canvas.height();

        if (winW / winH <= aspect_ratio)
        {
            canvas.css("margin-left", 0);

            canvas.css("width", "100%");
            canvas.css("margin-top", (winH - canvas.height()) / 2 + "px");
        }
        else
        {
            canvas.css("margin-top", 0);

            canvas.css("width", winH * aspect_ratio - 10);
            canvas.css("margin-left", (winW - canvas.width()) / 2 + "px");
        }
    };

    $(window).resize(resize_adjust); resize_adjust();

    var game = new GameBase(Console);
    game.Start();
}