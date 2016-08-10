function LobbyMenu(Console, game)
{
    var self = this;
    var menu = new Menu(Console, game, ["", "Back to menu"]);
    var server = new Server();

    self.Show = function()
    {
        return DrawState.LobbyMenu;
    }

    self.Connect = function()
    {
        server.Connect('192.168.1.253', 12321);
    }

    self.Draw = function()
    {
        Console.Clear();
        Console.SetCursor(3,1);
        menu.Draw();
    }

    self.Update = function()
    {
        menu.Update();

        if (game.IsKeyPressed(Keyboard.Keys.Enter))
        {
            switch (menu.Selected)
            {
                case 1:
                    menu.Selected = 0;
                    server.Close();
                    return game.render_mgr.main_menu.Show();

            }
        }

        if (menu.Selected == 0)
        {
            var last_enter = Keyboard.Buffer.lastIndexOf('Enter');
            Keyboard.Buffer = Keyboard.Buffer.slice(last_enter + 1);

            if (game.IsKeyPressed(Keyboard.Keys.Enter))
            {
                server.Send(menu.Itemlist[0]);
            }

            menu.Itemlist[0] = Keyboard.Buffer.join('');
        }

        return DrawState.LobbyMenu;
    }
}