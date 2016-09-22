
/**
 * @constructor
 */
function LoginMenu(Console, game)
{
    var menu = new Menu(Console, game, 16);
    menu.Itemlist = [
        new MenuItem(game, 'Login'),
        new MenuItem(game, 'Play as guest'),
        new MenuItem(game, '').merge({Skip: true}),
        new MenuItem(game, 'Register'),
        new MenuItem(game, '').merge({Skip: true}),
        new MenuItem(game, 'Exit'),
    ];

    this.Show = function()
    {
        return DrawState.LoginMenu;
    }

    this.Update = function()
    {
        menu.Update();

        if (game.IsKeyPressed(Keyboard.Keys.Enter))
        {
            switch (menu.Selected)
            {
                case 1:
                    return game.renderManager.guest_login_menu.Show();
                case 5:
                    document.location.href = 'https://www.youtube.com/watch?v=oHg5SJYRHA0';
                    break;
            }
        }

        return DrawState.LoginMenu;
    }

    this.Draw = function()
    {
        Console.Clear();

        //draws game logo
        var logo_buffer = game.renderManager.main_menu.LogoBuffer;
        Console.setCursor((Console.getWidth() - logo_buffer.Width) / 2, 1);
        Console.WriteImage(logo_buffer.getCanvas());

        Console.setCursor((Console.getWidth() - menu.Width) / 2, 18)
        menu.Draw();
    }
}