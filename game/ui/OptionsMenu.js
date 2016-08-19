function OptionsMenu(Console, game)
{
    var self = this;
    var menu = new Menu(Console, game, 65);

    menu.Itemlist = [
        new ListMenuItem(game, "Graphics", ["Blurred", "Pixelated"]),
        new IntMenuItem(game, "IntMenuItem"),
        new StringMenuItem(game, "StringMenuItem"),
        new IntMenuItem(game, "IntMenuItem"),
        new StringMenuItem(game, "StringMenuItem"),
        new IntMenuItem(game, "IntMenuItem"),
        new StringMenuItem(game, "StringMenuItem"),

        new MenuItem(game, "").merge({Skip: true}),
        new MenuItem(game, "Back"),
    ];

    self.Show = function()
    {
        menu.Selected = 0;
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
            return game.RenderManager.main_menu.Show();
        }

        if (game.IsKeyPressed(Keyboard.Keys.Enter))
        {
            switch (menu.GetSelected().Text)
            {
                case 'Back':
                    return game.RenderManager.main_menu.Show();
                    break;
            }
        }

        switch (menu.Itemlist[0].Value)
        {
            case 0:
                Console.GetCanvas().style.imageRendering = 'initial';
                break;
            case 1:
                Console.GetCanvas().style.imageRendering = 'pixelated';
                break;
        }


        return DrawState.OptionsMenu;
    }

}