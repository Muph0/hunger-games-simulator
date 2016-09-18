ErrorCode = {
    InvalidVersion: 4001,
    ProtocolFailure: 4002,
    BrokenJSON: 4003,
}

Object.prototype.merge = function(obj2)
{
    for (key in obj2)
    {
        this[key] = obj2[key];
    }

    return this;
}
String._padding = Array(80).join(' ');
String.prototype.padLeft = function(width) {
    var pad = String._padding.substring(0, width);
    if (typeof this === 'undefined')
        return pad;
    return (pad + this).slice(-pad.length);
}
String.prototype.padRight = function(width) {
    var pad = String._padding.substring(0, width);
    if (typeof this === 'undefined')
        return pad;
    return (this + pad).substring(0, pad.length);
}


function hash(obj)
{
    var str = JSON.stringify(obj);
    str = str + str + str + str + str + str;
    var hash = 0, i, chr, len;
    if (str.length === 0) return hash;

    for (i = 0, len = str.length; i < len; i++)
    {
        chr   = str.charCodeAt(i);
        hash  = ((hash << 5) - hash) + chr;
        hash |= 0; // Convert to 32bit integer
    }

    return hash.toString(36);
}

function inherit(that, parent)
{
    for (key in parent)
    {
        that[key] = parent[key];
    }
}

window["main"] = function(body)
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
            canvas['css']("margin-left", 0);

            canvas['css']("width", "100%");
            canvas['css']("margin-top", (winH - canvas.height()) / 2 + "px");
        }
        else
        {
            canvas['css']("margin-top", 0);

            canvas['css']("width", winH * aspect_ratio - 10);
            canvas['css']("margin-left", (winW - canvas.width()) / 2 + "px");
        }
    };

    $(window).resize(resize_adjust); resize_adjust();

    game = new GameBase(Console);
    game.Start();
}