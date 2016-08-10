function OptionsMenu(Console, game)
{
    var self = this;
    var menu = new Menu(Console, game, ["kek", "bur", "rofl", "!", "Back"]);

    self.Show = function()
    {
        return DrawState.OptionsMenu;
    }


    self.Draw = function()
    {
        Console.Clear();
        Console.SetCursor(5, 2);
        menu.Draw();
    }


    self.Update = function()
    {
        menu.Update();

        if (game.IsKeyPressed(Keyboard.Keys.Escape))
        {
            return game.render_mgr.main_menu.Show();
        }

        if (game.IsKeyPressed(Keyboard.Keys.Enter))
        {
            switch (menu.Selected)
            {
            case 4:
                menu.Selected = 0;
                return game.render_mgr.main_menu.Show();
            }
        }

        return DrawState.OptionsMenu;
    }

}