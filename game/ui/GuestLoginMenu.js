
/**
 * @constructor
 */
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
                    //game.character.Randomize();
                    if (menu.Itemlist[0].Value)
                        game.character.Name = menu.Itemlist[0].Value;
                    else
                        game.character.Name = "Guest";

                    return game.renderManager.create_character_menu.Show();
                    break;
                case 3:
                    return game.renderManager.login_menu.Show();
            }
        }

        return DrawState.GuestLoginMenu;
    }

    this.Draw = function()
    {
        Console.Clear()
        Console.setCursor(20, 8);
        menu.Draw();
    }
}