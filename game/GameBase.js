
function GameBase(Console)
{
    var self = this;
    var kbs = [], lkbs = [];

    this.Client = new Client(this);
    this.Character = new PlayerCharacter();

    this.ConnectionManager = new ConnectionManager(this);
    this.RenderManager = new RenderManager(Console, this);
    this.ServerInfo = new ServerInfo();

    this.Time = 0;

    this.Start = function()
    {
        Biome.RegisterBiomes();

        window.requestAnimationFrame(this.Run);
    }

    this.Run = function(time)
    {
        self.Time = time;
        self.Update();
        self.Draw();
        if (true) window.requestAnimationFrame(self.Run);
    }

    this.IsKeyDown = function(key)
    {
        return kbs[key];
    }
    this.IsKeyPressed = function(key)
    {
        return kbs[key] && !lkbs[key];
    }
    this.IsKeyReleased = function(key)
    {
        return !kbs[key] && lkbs[key];
    }

    this.Update = function()
    {
        Console.CursorVisible = false;
        lkbs = kbs;
        kbs = Keyboard.GetState();

        this.RenderManager.Update();
    }
    this.Draw = function()
    {
        this.RenderManager.Draw();

        Console.BlinkCursor(this.Time / 200);
    }
}