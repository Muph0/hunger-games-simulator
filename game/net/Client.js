function Server()
{
    var self = this;
    var server;

    self.Connect = function(IP, port)
    {
        server = new WebSocket("ws://" + IP + ":" + port);
        server.onopen = on_open; 
        server.onmessage = on_message;
        server.onclose = on_close;
    }

    self.Send = function(data)
    {
        server.send(data);
    }

    self.Close = function()
    {
        server.close();
    }

    var on_open = function(data)
    {
        console.log(data);
    }

    var on_message = function(data)
    {
        console.log(data);
    }

    var on_close = function(data)
    {
        console.log(data);
    }
}