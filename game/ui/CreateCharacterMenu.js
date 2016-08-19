function CreateCharacterMenu(Console, game)
{
    var character = new PlayerCharacter();

    var menu = new Menu(Console, game, 25);
    menu.HighlightBG = [10, 5, 80];
    menu.Itemlist = [];

    for (var i = 0; i < character.Stats.length; i++)
    {
        menu.Itemlist.push(new IntMenuItem(game, character.Stats.Names[i]).merge({Min: 1}));
    }
    menu.Itemlist.push(new MenuItem(game, '').merge({Skip: true}));
    menu.Itemlist.push(new MenuItem(game, '').merge({Skip: true}));
    for (var i = 0; i < character.Skills.length; i++)
    {
        menu.Itemlist.push(new IntMenuItem(game, character.Skills.Names[i]).merge({Min: 1}));
    }
    menu.Itemlist.push(new MenuItem(game, '').merge({Skip: true}));
    menu.Itemlist.push(new StringMenuItem(game, 'Name:'));
    menu.Itemlist.push(new MenuItem(game, 'Confirm changes'));
    menu.Itemlist.push(new MenuItem(game, 'Randomize'));
    menu.Itemlist.push(new MenuItem(game, 'Cancel'));

    this.Show = function()
    {
        menu.Selected = 0;
        this.ImportCharacter();

        return DrawState.CreateCharacterMenu;
    }

    this.ImportCharacter = function()
    {
        character = new PlayerCharacter();
        character.Import(game.Character);
        for (var i = 0; i < character.Stats.length; i++)
        {
            menu.Itemlist[i].Value = character.Stats[i];
        }
        for (var i = 0; i < character.Skills.length; i++)
        {
            menu.Itemlist[i + character.Stats.length + 2].Value = character.Skills[i];
        }

        // import name
        menu.Itemlist[0 + character.Stats.length + character.Skills.length + 3].Value = character.Name;
        menu.Itemlist[0 + character.Stats.length + character.Skills.length + 3].CursorPos = character.Name.length;
    }

    this.Update = function()
    {
        menu.Update();

        if (game.IsKeyPressed(Keyboard.Keys.Enter))
        {
            switch (menu.Selected)
            {
                case 1 + character.Stats.length + character.Skills.length + 3:  //confirm
                    game.Character = character;
                    character = null;
                    return game.RenderManager.main_menu.Show();
                case 2 + character.Stats.length + character.Skills.length + 3:  //randomize
                    game.Character.Name = character.Name;
                    game.Character.Randomize();
                    this.ImportCharacter();
                    break;
                case 3 + character.Stats.length + character.Skills.length + 3:  //cancel
                    return game.RenderManager.main_menu.Show();
            };

        }


        for (var i = 0; i < character.Stats.length; i++)
        {
            character.Stats[i] = menu.Itemlist[i].Value;
            menu.Itemlist[i].Max = menu.Itemlist[i].Value + character.Stats.FreePoints;
        }
        for (var i = 0; i < character.Skills.length; i++)
        {
            var x = i + character.Stats.length + 2;
            character.Skills[i] = menu.Itemlist[x].Value;
            menu.Itemlist[x].Max = menu.Itemlist[x].Value + character.Skills.FreePoints;
        }

        character.Name = menu.Itemlist[0 + character.Stats.length + character.Skills.length + 3].Value;

        return DrawState.CreateCharacterMenu;
    }

    this.Draw = function()
    {
        var fg = Console.Foreground;
        var bg = Console.Background;

        Console.Clear();

        Console.Foreground = [255,255,255];
        Console.Background = [0, 90, 128];
        Console.SetCursor(5, 2);
        Console.Write(("  Free stat points: " + character.Stats.FreePoints).padRight(menu.Width + 2));
        Console.SetCursor(5, 4 + character.Stats.length);
        Console.Write(("  Free skill points: " + character.Skills.FreePoints).padRight(menu.Width + 2));

        Console.Foreground = fg;
        Console.Background = bg;
        Console.SetCursor(5,1);
        Console.Write(character);
        Console.SetCursor(5,3);
        menu.Draw();
    }

}
