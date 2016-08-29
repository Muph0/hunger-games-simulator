
function ConnectingMenu(Console, game)
{
    var img = document.getElementById('ascii');
    this.LogoBuffer = new CanvasConsole(28, 3, img);
    this.LogoBuffer.CreateCanvas(null);

    var connecting_str =
"╔═╗╔═╗╔╗╔╔╗╔╔═╗╔═╗╔╦╗╦╔╗╔╔═╗\n" +
"║  ║ ║║║║║║║║╣ ║   ║ ║║║║║ ╦\n" +
"╚═╝╚═╝╝╚╝╝╚╝╚═╝╚═╝ ╩ ╩╝╚╝╚═╝";

    this.LogoBuffer.Foreground = [100, 200, 255];
    this.LogoBuffer.Write(connecting_str);

    var loading_bar = new LoadingBar(Console, game, 40);
    loading_bar.Percent = -9;
    var back_menu = new Menu(Console, game, 5);
    back_menu.Itemlist = [
        new MenuItem(game, 'Back'),
    ];

    this.Show = function(ip, port)
    {
        if (typeof port === 'undefined') port = 7887;
        if (ip || ip === "")
        {
            game.Client = new Client(game);
            game.Client.Connect(ip, port);
        }


        return DrawState.ConnectingMenu;
    }

    this.Update = function()
    {
        switch (game.Client.State)
        {
            case ClientState.Connected:
                Console.Foreground = [200, 200, 200];
                return game.RenderManager.lobby_menu.Show();

            case ClientState.Connecting:
            case ClientState.Disconnected:
                if (game.IsKeyPressed(Keyboard.Keys.Enter))
                {
                    game.Client.Close();
                    return game.RenderManager.main_menu.Show();
                }
                break;
        }

        return game.ConnectionManager.Update(DrawState.ConnectingMenu);
    }

    this.Draw = function()
    {
        Console.Clear();

        Console.CursorX = Math.floor(Console.Width / 2 - back_menu.Width / 2);
        Console.CursorY = Math.floor(Console.Height / 2 + 6);
        back_menu.Draw();

        switch (game.Client.State)
        {
            case ClientState.Connecting:
                // Draw logo
                Console.SetCursor((Console.Width - this.LogoBuffer.Width) / 2, Console.Height / 2 - 4);
                Console.WriteImage(this.LogoBuffer.GetCanvas());

                // write IP
                var msg = "to " + game.Client.ServerIP;
                Console.SetCursor((Console.Width - msg.length) / 2, Console.Height / 2);
                Console.Write(msg);

                // Draw loading bar
                Console.SetCursor((Console.Width - loading_bar.Width) / 2, Console.Height / 2 + 3);
                loading_bar.Draw();
                break;

            case ClientState.Disconnected:

                var msg = 'unhandled';

                if (game.Client.LastEvent.type === "close")
                {
                    switch (game.Client.LastEvent.code)
                    {
                        case 1000:
                            msg = "You left the game.";
                            break;
                        case 1006:
                            if (game.Client.Error)
                            {
                                if (game.Client.Duration > 8000)
                                    msg = "Server didn't respond in time. (Timeout)";
                                else
                                    msg = "Server refused connection. (Connection refused)";
                            }
                            else
                            {
                                msg = "Connection lost. (Server closed)";
                            }

                            break;
                        case ErrorCode.InvalidVersion: // ClientVerifier: wrong version
                            msg = "Wrong client version."
                            break;
                        case ErrorCode.ProtocolFailure:
                            msg = "Protocol failure."
                            break;
                        case ErrorCode.BrokenJSON:
                            msg = "Server received broken JSON."
                            break;
                    }
                }

                Console.SetCursor(Math.floor(Console.Width / 2 - msg.length / 2), Console.Height / 2 - 2);
                Console.Write(msg);

                if (msg === 'unhandled')
                {
                    debugger;
                    console.log(game.Client);
                    null.end();
                }

                break;

        }
    }
}