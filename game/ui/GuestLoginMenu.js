
function GuestLoginMenu(Console, game)
{
    var menu = new Menu(Console, game, 40);
    menu.Itemlist = [
        new StringMenuItem(game, 'Your name:'),
        new MenuItem(game, '').merge({Skip: true}),
        new MenuItem(game, 'Create character'),
        new MenuItem(game, 'Back'),
    ];

    this.Show = function()
    {
        return DrawState.GuestLoginMenu;
    }

    this.Update = function()
    {
        menu.Update();

        if (game.IsKeyPressed(Keyboard.Keys.Enter))
        {
            switch (menu.Selected)
            {
                case 2:
                    //game.Character.Randomize();
                    if (menu.Itemlist[0].Value)
                        game.Character.Name = menu.Itemlist[0].Value;
                    else
                        game.Character.Name = "Guest";

                    return game.RenderManager.create_character_menu.Show();
                    break;
                case 3:
                    return game.RenderManager.login_menu.Show();
            }
        }

        return DrawState.GuestLoginMenu;
    }

    this.Draw = function()
    {
        Console.Clear()
        Console.SetCursor(20, 8);
        menu.Draw();
    }
}