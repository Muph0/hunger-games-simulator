
/**
 * @constructor
 */
function GameBase(Console)
{
    var self = this;
    var kbs = [], lkbs = [];

    this.client = new Client(this);
    this.character = new PlayerCharacter();

    this.connectionManager = new ConnectionManager(this);
    this.renderManager = new RenderManager(Console, this);
    this.serverInfo = new ServerInfo();

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

        this.renderManager.Update();
    }
    this.Draw = function()
    {
        this.renderManager.Draw();

        Console.BlinkCursor(this.Time / 200);
    }
}