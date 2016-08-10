
function MainMenu(Console, game)
{
    var self = this;
    var img = document.getElementById('ascii');
    var logo_buffer = new CanvasConsole(52, 16, img);
    var menu = new Menu(Console, game, ["Join game", "!", "Options", "Credits", "Exit"]);
    logo_buffer.CreateCanvas(null);

        logo_buffer.Foreground = [255, 0, 10];
        logo_buffer.Write(
" ██░ ██  █    ██  ███▄    █   ▄████ ▓█████  ██▀███  \n" +
"▓██░ ██▒ ██  ▓██▒ ██ ▀█   █  ██▒ ▀█▒▓█   ▀ ▓██ ▒ ██▒\n" +
"▒██▀▀██░▓██  ▒██░▓██  ▀█ ██▒▒██░▄▄▄░▒███   ▓██ ░▄█ ▒\n" +
"░▓█ ░██ ▓▓█  ░██░▓██▒  ▐███▒░▓█  ██▓▒▓█  ▄ ▒██▀▀█▄  \n" +
"░▓█▒░██▓▒▒█████▓ ▒██ ░  ▓██░░▒▓███▀▒░▒████▒░██▓ ▒██▒\n" +
" ▒ ░░▒░▒░▒▓▒ ▒ ▒ ░ ▒ ░  ▒ ▒  ░▒   ▒ ░░ ▒░ ░░ ▒▓ ░▒▓░\n" +
" ▒ ░▒░ ░░░▒░ ░░░ ░ ░ ▒  ░ ▒░  ░   ░  ░ ░  ░  ░▒ ░ ▒░\n" +
" ░  ░░ ░ ░░░ ░ ░    ░░  ░ ░ ░ ░   ░    ░     ░░   ░ \n" +
" ░  ░  ░   ░              ░       ░    ░  ░   ░     \n");


        logo_buffer.Foreground = [110, 200, 225];
        logo_buffer.Write("\n" +
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

        //draws game logo
        Console.SetCursor((Console.GetWidth() - logo_buffer.GetWidth()) / 2, 1);
        Console.WriteImage(logo_buffer.GetCanvas());

        //draws game menu
        Console.Foreground = [200, 200, 200];
        Console.CursorX += ((logo_buffer.GetWidth() / 2) - 7);
        Console.CursorY += logo_buffer.GetHeight() + 1;
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
                    game.render_mgr.lobby_menu.Connect();
                    return game.render_mgr.lobby_menu.Show();
                    break;
                case 2:
                    return game.render_mgr.options_menu.Show();
                    break;
                case 4:
                    document.location.href = 'https://www.youtube.com/watch?v=oHg5SJYRHA0';
            }
        }

        return DrawState.MainMenu;
    }
}