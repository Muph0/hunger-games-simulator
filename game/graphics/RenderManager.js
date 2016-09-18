
/**
 * @constructor
 */
function RenderManager(Console, game)
{
    var self = this;
    var state = DrawState.MainMenu;

    // User interface
    self.create_character_menu = new CreateCharacterMenu(Console, game);
    self.guest_login_menu = new GuestLoginMenu(Console, game);
    self.connecting_menu = new ConnectingMenu(Console, game);
    self.options_menu = new OptionsMenu(Console, game);
    self.browse_menu = new BrowseMenu(Console, game);
    self.login_menu = new LoginMenu(Console, game);
    self.lobby_menu = new LobbyMenu(Console, game);
    self.main_menu = new MainMenu(Console, game);

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
            case DrawState.ConnectingMenu:
                state = self.connecting_menu.Update();
                break;
            case DrawState.BrowseMenu:
                state = self.browse_menu.Update();
                break;
            case DrawState.LoginMenu:
                state = self.login_menu.Update();
                break;
            case DrawState.GuestLoginMenu:
                state = self.guest_login_menu.Update();
                break;
            case DrawState.CreateCharacterMenu:
                state = self.create_character_menu.Update();
                break;
            default:
                throw new Error("Someone just set the state to NaN.");
        }
    }
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
            case DrawState.ConnectingMenu:
                self.connecting_menu.Draw();
                break;
            case DrawState.BrowseMenu:
                self.browse_menu.Draw();
                break;
            case DrawState.LoginMenu:
                self.login_menu.Draw();
                break;
            case DrawState.GuestLoginMenu:
                self.guest_login_menu.Draw();
                break;
            case DrawState.CreateCharacterMenu:
                self.create_character_menu.Draw();
        }
    }
}

var DrawState = {
    MainMenu: 0,
    ConnectingMenu: 1,
    OptionsMenu: 2,
    ServerMenu: 3,
    LoginMenu: 4,
    GuestLoginMenu: 5,
    CreateCharacterMenu: 6,
    BrowseMenu: 7,
    LobbyMenu: 666,
}