
function GameBase(Console)
{
    var self = this;
    var kbs = [], lkbs = [];
    self.render_mgr = new RenderManager(Console, self);

    self.Time = 0;

    self.Start = function()
    {
        Biome.RegisterBiomes();

        window.requestAnimationFrame(self.Run);
    }

    self.Run = function(time)
    {
        self.Time = time;
        self.Update();
        self.Draw();
        if (true) window.requestAnimationFrame(self.Run);
    }

    self.IsKeyDown = function(key)
    {
        return kbs[key];
    }
    self.IsKeyPressed = function(key)
    {
        return kbs[key] && !lkbs[key];
    }
    self.IsKeyReleased = function(key)
    {
        return !kbs[key] && lkbs[key];
    }

    self.Update = function()
    {
        lkbs = kbs;
        kbs = Keyboard.GetState();

        self.render_mgr.Update();
    }
    self.Draw = function()
    {
        self.render_mgr.Draw();
    }
}