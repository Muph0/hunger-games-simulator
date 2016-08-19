
function Client(game)
{
    var self = this;
    var client;
    var connection_started = 0;
    var connection_ended = null;

    this.State = ClientState.Disconnected;
    this.LastEvent = null;
    this.Error = null;
    this.Ping = 0;

    this.Responses = [];

    this.__defineGetter__('ServerIP', function() {
        return client.url.split('/')[2].split(':')[0];
    });
    this.__defineGetter__('Duration', function() {
        if (connection_ended)
            return connection_ended - connection_started;
        return game.Time - connection_started;
    });

    this.Connect = function(IP, Port)
    {
        self.State = ClientState.Connecting;

        client = new WebSocket("ws://" + IP + ':' + Port);
        connection_started = game.Time;

        client.onopen = on_open;
        client.onmessage = on_message;
        client.onclose = on_close;
        client.onerror = on_error;
    }

    this.Send = function(data)
    {
        client.send(data);
    }

    this.Close = function()
    {
        client.close();
    }

    var on_open = function(evt)
    {
        self.State = ClientState.Connected;
        self.LastEvent = evt;
        client.send(JSON.stringify({protocol: "0.1", character: game.Character}));
    }

    var on_message = function(evt)
    {
        try
        {
            var data = JSON.parse(evt.data);

            if (data.Error)
            {
                self.Error = evt;

                console.log("Client on_message (user error):");
                console.log(evt);

                client.close();
            }

            self.Responses.push(data);
        }
        catch (e)
        {
            console.error(e);
            console.error("Server sent broken JSON.")
        }

        self.LastEvent = evt;
    }

    var on_close = function(evt)
    {
        self.State = ClientState.Disconnected;
        self.LastEvent = evt;
        console.log("Client on_close: (lasted " + Math.floor(self.Duration) / 1000 + " seconds)" );
        console.log(evt);
        self.LastEvent = evt;
        connection_ended = game.Time;
    }

    var on_error = function(evt)
    {
        self.State = ClientState.Disconnected;
        console.log("Client on_error:");
        console.log(evt);
        self.Error = evt;
    }
}

ClientState = {
    Disconnected: 0,
    Connected: 1,
    Connecting: 2,
}