
function LobbyMenuItem(game, client_info)
{
    inherit(this, new MenuItem(game, client_info.toString()));

    this.ClientInfo = client_info;

    this.Update = function()
    {

    }

    this.Draw = function(Console, menu)
    {
        var X = Console.CursorX;
        var Y = Console.CursorY;

        var name = this.ClientInfo.Character.Name;
        Console.Write(name.padRight(12));

        var desc = this.ClientInfo.Character.toString();
        Console.Write(desc.padRight(21));

        var ping = game.Client.Ping.toString();
        Console.Write(ping.padRight(4));

        var ready = this.ClientInfo.LobbyReady;
        var bg = Console.Background;
        Console.Background = ready? [0, 160, 0] : [220, 0, 0];
        Console.Write('    ')
        Console.Background = bg;
    }
}