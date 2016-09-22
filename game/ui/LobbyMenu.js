
/**
 * @constructor
 */
function LobbyMenu(Console, game)
{
    var self = this;
    var sys_menu = new Menu(Console, game, 20);
    sys_menu.Itemlist = [
        new MenuItem(game, 'Ready'),
        new MenuItem(game, 'Edit character'),
        new StringMenuItem(game, 'Chat:'),
        new MenuItem(game, '').merge({Skip: true}),
        new MenuItem(game, 'Start game'),
        new MenuItem(game, 'Leave game'),
    ];

    var playerlist_menu = new Menu(Console, game, 35);
    playerlist_menu.MaxHeight = 5;
    playerlist_menu.Spacing = 2;

    // chat window
    var img = document.getElementById('ascii');
    var chat_buffer = new CanvasConsole(69, 6, img);
    chat_buffer.CreateCanvas(null);

    var lobby_hash, chat_hash = -1;

    var leftmenu = true;

    this.Show = function()
    {
        playerlist_menu.Itemlist = [];
        leftmenu = true;

        return DrawState.LobbyMenu;
    }

    this.Update = function()
    {
        lastmenu = leftmenu;

        if (leftmenu)
        {
            if (game.IsKeyPressed(Keyboard.Keys.Right) && (sys_menu.Selected !== 2 || sys_menu.Itemlist[2].CursorPos === sys_menu.Itemlist[2].Value.length))
            {
                leftmenu = false;
            }
            sys_menu.Update();
        }
        else
        {
            playerlist_menu.Update();
            if (game.IsKeyPressed(Keyboard.Keys.Left))
            {
                leftmenu = true;
                sys_menu.Show();
            }
        }

        if (game.IsKeyPressed(Keyboard.Keys.Enter))
        {
            switch (sys_menu.GetSelected().Text)
            {
                case 'Ready':
                    var data = {
                        ready: 1,
                    }
                    game.client.Send(JSON.stringify(data));
                    break;
                case 'Chat:':
                    var data = {
                        msg: {
                            text: sys_menu.GetSelected().Value,
                        }
                    }
                    sys_menu.GetSelected().Value = "";
                    game.client.Send(JSON.stringify(data));
                    break;
                case 'Leave game':
                    game.client.Close();
                    break;
            }
        }

        if (lobby_hash !== hash(game.serverInfo.Playerlist))
        {
            console.log("Creating new playerlist_menu");
            console.log(game.serverInfo.Playerlist);
            lobby_hash = hash(game.serverInfo.Playerlist);

            playerlist_menu.Itemlist = [];
            for (var i = 0; i < game.serverInfo.Playerlist.length; i++)
            {
                playerlist_menu.Itemlist[i] = new LobbyMenuItem(game, game.serverInfo.Playerlist[i]);
            }

            if (leftmenu)
            {
                lastmenu = false;
                this.Draw();
                lastmenu = true;
            }
        }

        if (chat_hash !== hash(game.serverInfo.ChatHistory.slice(-1)[0]))
        {
            chat_hash = hash(game.serverInfo.ChatHistory.slice(-1)[0]);

            chat_buffer.Foreground = [200, 200, 200];
            chat_buffer.Background = [6, 6, 6];
            chat_buffer.Clear();

            var display_count = chat_buffer.getHeight();
            var lines = [];

            for (var i = 0; i < display_count; i++)
            {
                var offset = Math.max(game.serverInfo.ChatHistory.length - display_count, 0);
                if (i + offset >= game.serverInfo.ChatHistory.length)
                    break;

                var msg = game.serverInfo.ChatHistory[i + offset];
                var time = new Date(msg.time);
                var playername = game.serverInfo.getPlayerByToken(msg.token).Character.Name;

                var line = ''+time.getHours()+':'+time.getMinutes()+' <'+playername+'> ' + msg.text;
                lines.push(line);
            }

            chat_buffer.Write(lines.join('\n'));
        }

        return game.connectionManager.Update(DrawState.LobbyMenu);
    }

    this.Draw = function()
    {
        var half_w = Math.floor(Console.getWidth() * 1/3);

        // set where to highlight
        playerlist_menu.HighlightSelected = !leftmenu;
        sys_menu.HighlightSelected = leftmenu;

        Console.Clear();

        // Draw chat window
        Console.setCursor(5, Console.getHeight() - chat_buffer.getHeight() - 1);
        Console.WriteImage(chat_buffer.getCanvas());

        Console.setCursor(half_w + 3, 2)
        playerlist_menu.Draw();

        Console.setCursor(3, 2)
        sys_menu.Draw();

    }
}




