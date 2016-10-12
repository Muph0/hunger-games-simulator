function CreditsMenu(Console, game)
{
	var menu = new Menu(Console, game, 20)
	menu.Itemlist = [
        new MenuItem(game, 'Designed by:        Your father').merge({Skip: true}),
        new MenuItem(game, '             Your second father').merge({Skip: true}),
        new MenuItem(game, '                               ').merge({Skip: true}),
		new MenuItem(game, 'Programmed by:      Your mother').merge({Skip: true}),
        new MenuItem(game, '                    Your father').merge({Skip: true}),
        new MenuItem(game, '                               ').merge({Skip: true}),
        new MenuItem(game, 'Concept by:         Your granny').merge({Skip: true}),
        new MenuItem(game, '                               ').merge({Skip: true}),
        new MenuItem(game, 'Back                           '),
    ];

	this.Show = function()
	{
		return DrawState.CreditsMenu;
	}

	this.Draw = function()
 	{
 	    Console.Clear();
   		Console.setCursor(5, 2);

 		menu.Draw();
 	}

 	this.Update = function()
    {
        menu.Update();

        if (game.IsKeyPressed(Keyboard.Keys.Enter))
        {
            switch (menu.Selected)
            {
                case 8:
                    return game.renderManager.main_menu.Show();
                    break;
                
            }
        }

        return DrawState.CreditsMenu;
    }
}