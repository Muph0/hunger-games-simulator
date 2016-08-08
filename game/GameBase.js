
function GameBase(_Console)
{
    var self = this;
    var Console = _Console;
    var MainMenu = new Menu(["Start","Options","Allahu akbar","Exit"], Console);
    var kbs = [], lkbs = [];

    self.Start = function()
    {
        window.requestAnimationFrame(self.Run);
    }

    self.Run = function(time)
    {
        self.Update(time);
        self.Draw(time);
        if (true) window.requestAnimationFrame(self.Run);
    }

    self.Draw = function(time)
    {
        Console.Clear();
        for (var key in kbs)
        {
            if(kbs[key])
                Console.WriteLine(key);
        }


        Console.SetCursor(Math.floor(Math.sin(time/300)*10 + 40), Math.floor(Math.cos(time/300)*5 + 12));
        Console.Write('x');

        if (!kbs[Keyboard.Keys.Spacebar] && lkbs[Keyboard.Keys.Spacebar])
        {
            var r = Math.floor(Math.random() * 20);
            var g = Math.floor(Math.random() * 20);
            var b = Math.floor(Math.random() * 20);

            Console.Background = [r,g,b];
        }
        Console.SetCursor(5,1);
        MainMenu.Draw();


    }
    self.Update = function(time)
    {
        lkbs = kbs;
        kbs = Keyboard.GetState();
        MainMenu.Update();

    }
}