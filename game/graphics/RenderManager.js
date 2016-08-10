
function RenderManager(Console, game)
{
    var self = this;
    var state = DrawState.MainMenu;

    // User interface vars
    self.main_menu = new MainMenu(Console, game);
    self.options_menu = new OptionsMenu(Console, game);
    self.lobby_menu = new LobbyMenu(Console, game);

    self.Draw = function()
    {
        switch (state)
        {
            case DrawState.MainMenu:
                self.main_menu.Draw();
                break;
            case DrawState.OptionsMenu:
                self.options_menu.Draw();
                break;
            case DrawState.LobbyMenu:
                self.lobby_menu.Draw();
                break;
        }
    }
    self.Update = function()
    {
        switch (state)
        {
            case DrawState.MainMenu:
                state = self.main_menu.Update();
                break;
            case DrawState.OptionsMenu:
                state = self.options_menu.Update();
                break;
            case DrawState.LobbyMenu:
                state = self.lobby_menu.Update();
                break;
        }
    }
}

window.DrawState = {
    MainMenu: 0,
    OptionsMenu: 1,
    LobbyMenu: 666,
}