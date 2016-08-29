
function LobbyMenu(Console, game)
{
    var self = this;
    var sys_menu = new Menu(Console, game, 20);
    sys_menu.Itemlist = [
        new MenuItem(game, 'Ready'),
        new MenuItem(game, 'Edit character'),
        new StringMenuItem(game, 'Chat:'),
        new MenuItem(game, '').merge({Skip: true}),
        new MenuItem(game, 'Leave game'),
    ];

    var playerlist_menu = new Menu(Console, game, 35);
    playerlist_menu.MaxHeight = 5;
    playerlist_menu.Spacing = 2;

    var lobby_hash;

    var leftmenu = true;
    var lastmenu = true;

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
            if (game.IsKeyPressed(Keyboard.Keys.Right) && (sys_menu.Selected !== 1 || sys_menu.Itemlist[1].CursorPos === sys_menu.Itemlist[1].Value.length))
                leftmenu = false;
            sys_menu.Update();
        }
        else
        {
            playerlist_menu.Update();
            if (game.IsKeyPressed(Keyboard.Keys.Left))
                leftmenu = true;
        }

        if (game.IsKeyPressed(Keyboard.Keys.Enter))
        {
            switch (sys_menu.GetSelected().Text)
            {
                case 'Ready':
                    var data = {
                        ready: 1,
                    }
                    game.Client.Send(JSON.stringify(data));
                    break;
                case 'Leave game':
                    game.Client.Close();
                    break;
            }
        }

        if (lobby_hash !== hash(game.ServerInfo.Playerlist))
        {
            console.log("Creating new playerlist_menu");
            console.log(game.ServerInfo.Playerlist);
            lobby_hash = hash(game.ServerInfo.Playerlist);

            playerlist_menu.Itemlist = [];
            for (var i = 0; i < game.ServerInfo.Playerlist.length; i++)
            {
                playerlist_menu.Itemlist[i] = new LobbyMenuItem(game, game.ServerInfo.Playerlist[i]);
            }

            if (leftmenu)
            {
                lastmenu = false;
                this.Draw();
                lastmenu = true;
            }
        }

        return game.ConnectionManager.Update(DrawState.LobbyMenu);
    }

    this.Draw = function()
    {
        var half_w = Math.floor(Console.Width * 1/3);

        if (leftmenu || lastmenu)
        {
            sys_menu.HighlightSelected = leftmenu;
            Console.SetCursor(0, 0);
            Console.ClearRectangle(half_w, 25);

            Console.CursorX += 3;
            Console.CursorY += 2;
            sys_menu.Draw();
        }
        if (!leftmenu || !lastmenu)
        {
            playerlist_menu.HighlightSelected = !leftmenu;
            Console.SetCursor(half_w, 0);
            Console.ClearRectangle(Console.Width - half_w, 25);

            Console.CursorX += 1;
            Console.CursorY += 2;
            playerlist_menu.Draw();
        }
    }
}




