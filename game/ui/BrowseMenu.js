
function BrowseMenu(Console, game)
{
    var menu = new Menu(Console, game, 40);
    menu.Itemlist = [
        new StringMenuItem(game, 'Enter IP:'),
        new MenuItem(game, '').merge({Skip: true}),
        new MenuItem(game, 'Back'),
    ];


    this.Show = function()
    {
        menu.Selected = 0;
        menu.Itemlist[0].Value = '127.0.0.1';
        menu.Itemlist[0].CursorPos = menu.Itemlist[0].Value.length;

        return DrawState.BrowseMenu;
    }

    this.Update = function()
    {
        menu.Update();

        if (game.IsKeyPressed(Keyboard.Keys.Enter))
        {
            switch (menu.Selected)
            {
                case 0:
                    var ip = menu.Itemlist[0].Value;

                    var port = ip.split(':')[1];
                    ip = ip.split(':')[0];

                    return game.RenderManager.connecting_menu.Show(ip, port);
                case 2:
                    return game.RenderManager.main_menu.Show();

            }
        }

        return DrawState.BrowseMenu;
    }


    this.Draw = function()
    {
        Console.Clear();
        Console.SetCursor(Console.Width / 2 - menu.Width / 2, Console.Height / 2);
        menu.Draw();
    }
}