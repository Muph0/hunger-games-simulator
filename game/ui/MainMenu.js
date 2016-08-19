
function MainMenu(Console, game)
{
    var self = this;
    var img = document.getElementById('ascii');

    var menu = new Menu(Console, game, 10);
    menu.Itemlist = [
        new MenuItem(game, 'Join game'),
        new MenuItem(game, '').merge({Skip: true}),
        new MenuItem(game, 'Edit character'),
        new MenuItem(game, 'Options'),
        new MenuItem(game, 'About'),
        new MenuItem(game, 'Exit'),
    ];
    menu.MaxHeight = 6;

    this.LogoBuffer = new CanvasConsole(52, 16, img);
    this.LogoBuffer.CreateCanvas(null);

    this.LogoBuffer.Foreground = [255, 0, 10];
    this.LogoBuffer.Write(
" ██░ ██  █    ██  ███▄    █   ▄████ ▓█████  ██▀███  \n" +
"▓██░ ██▒ ██  ▓██▒ ██ ▀█   █  ██▒ ▀█▒▓█   ▀ ▓██ ▒ ██▒\n" +
"▒██▀▀██░▓██  ▒██░▓██  ▀█ ██▒▒██░▄▄▄░▒███   ▓██ ░▄█ ▒\n" +
"░▓█ ░██ ▓▓█  ░██░▓██▒  ▐███▒░▓█  ██▓▒▓█  ▄ ▒██▀▀█▄  \n" +
"░▓█▒░██▓▒▒█████▓ ▒██ ░  ▓██░░▒▓███▀▒░▒████▒░██▓ ▒██▒\n" +
" ▒ ░░▒░▒░▒▓▒ ▒ ▒ ░ ▒ ░  ▒ ▒  ░▒   ▒ ░░ ▒░ ░░ ▒▓ ░▒▓░\n" +
" ▒ ░▒░ ░░░▒░ ░░░ ░ ░ ▒  ░ ▒░  ░   ░  ░ ░  ░  ░▒ ░ ▒░\n" +
" ░  ░░ ░ ░░░ ░ ░    ░░  ░ ░ ░ ░   ░    ░     ░░   ░ \n" +
" ░  ░  ░   ░              ░       ░    ░  ░   ░     \n");


    this.LogoBuffer.Foreground = [110, 200, 225];
    this.LogoBuffer.Write("\n" +
"   ██████╗ ███╗   ██╗██╗     ██╗███╗   ██╗███████╗  \n" +
"  ██╔═══██╗████╗  ██║██║     ██║████╗  ██║██╔════╝  \n" +
"  ██║   ██║██╔██╗ ██║██║     ██║██╔██╗ ██║█████╗    \n" +
"  ██║   ██║██║╚██╗██║██║     ██║██║╚██╗██║██╔══╝    \n" +
"  ╚██████╔╝██║ ╚████║███████╗██║██║ ╚████║███████╗  \n" +
"   ╚═════╝ ╚═╝  ╚═══╝╚══════╝╚═╝╚═╝   ╚══╝╚══════╝  ");


    self.Show = function()
    {
        return DrawState.MainMenu;
    }


    self.Draw = function()
    {
        Console.Clear();
        var logo_buffer = this.LogoBuffer;

        //draws game logo
        Console.SetCursor((Console.Width - logo_buffer.Width) / 2, 1  );
        Console.WriteImage(logo_buffer.GetCanvas());

        //draws game menu
        Console.Foreground = [200, 200, 200];
        Console.CursorX += ((logo_buffer.Width / 2) - 7);
        Console.CursorY += logo_buffer.Height + 1;
        menu.Draw();
    }


    self.Update = function()
    {
        menu.Update();

        if (game.IsKeyPressed(Keyboard.Keys.Enter))
        {
            switch (menu.Selected)
            {
                case 0:
                    return game.RenderManager.browse_menu.Show();
                    break;
                case 2:
                    return game.RenderManager.create_character_menu.Show();
                    break;
                case 3:
                    return game.RenderManager.options_menu.Show();
                    break;
            }
        }

        return DrawState.MainMenu;
    }
}