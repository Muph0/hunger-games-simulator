
function LobbyMenu(Console, game)
{
    var self = this;
    var sys_menu = new Menu(Console, game, 20);
    sys_menu.Itemlist = [
        new MenuItem(game, 'Edit character'),
        new StringMenuItem(game, 'Chat:'),
        new MenuItem(game, '').merge({Skip: true}),
        new MenuItem(game, 'Leave game'),
    ];

    var playerlist_menu = new Menu(Console, game, 35);
    playerlist_menu.MaxHeight = 5;
    playerlist_menu.Spacing = 2;

    var leftmenu = true;

    this.Show = function()
    {
        playerlist_menu.Itemlist = [];
        leftmenu = true;

        return DrawState.LobbyMenu;
    }

    this.Update = function()
    {
        if (leftmenu)
        {
            sys_menu.Update();
        }
        else
        {
            playerlist_menu.Update();
        }

        if (game.IsKeyPressed(Keyboard.Keys.Enter))
        {
            switch (sys_menu.Selected)
            {
                case 1:
                    game.Client.Send(sys_menu.Itemlist[0].Value);
                    sys_menu.Itemlist[0].Value = "";
                    break;
                case 3:
                    game.Client.Close();
                    break;
            }
        }

        if (leftmenu && game.IsKeyPressed(Keyboard.Keys.Right) && sys_menu.Selected !== 1)
            leftmenu = false;
        if (!leftmenu && game.IsKeyPressed(Keyboard.Keys.Left))
            leftmenu = true;

        if (playerlist_menu.Itemlist.length !== game.ServerInfo.Playerlist.length)
        {
            playerlist_menu.Itemlist = [];
            for (var i = 0; i < game.ServerInfo.Playerlist.length; i++)
            {
                playerlist_menu.Itemlist[i] = new LobbyMenuItem(game, game.ServerInfo.Playerlist[i]);
            }

            if (leftmenu)
            {
                leftmenu = false;
                this.Draw();
                leftmenu = true;
            }
        }

        return game.ConnectionManager.Update(DrawState.LobbyMenu);
    }

    this.Draw = function()
    {
        var half_w = Math.floor(Console.Width * 1/3);

        if (leftmenu || sys_menu.HighlightSelected !== leftmenu)
        {
            sys_menu.HighlightSelected = leftmenu;
            Console.SetCursor(0, 0);
            Console.ClearRectangle(half_w, 25);

            Console.CursorX += 3;
            Console.CursorY += 2;
            sys_menu.Draw();
        }
        if (!leftmenu || playerlist_menu.HighlightSelected === leftmenu)
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




