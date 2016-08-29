
function Server()
{
    var self = this;
    this.__defineGetter__('PROTOCOL_VERSION', function() { return "0.1"; });

    var server;
    var ID_autoincrement = 0;

    this.Verifier = new ClientVerifier(this);
    this.LobbyManager = new LobbyManager(this);

    this.Arena = null;
    this.Clients = [];

    this.Start = function(port)
    {
        try {
            fs.accessSync(SETTINGS.game.arena_name + ".arena", fs.F_OK);
            // Do something
        } catch (e) {
            console.log('Arena "' + SETTINGS.game.arena_name + '" not found,\nGENERATING NEW ONE.\n')
            self.Arena = LevelGenerator.Generate(1, 20);

            fs.writeFile("out.txt", JSON.stringify(self.Arena, null, 4), function(err) {
                if (err) {
                    return console.log(err);
                }

                console.log("Map saved!");
            });
        }

        console.log('\nStarting WebSocketServer on port ' + port);
        server = new WebSocketServer({'port': port});
        server.on('connection', accept_client);
    }
    this.Stop = function()
    {
        console.log('Shutting down...');
        server.close();
    }

    this.Broadcast = function(data, exclude)
    {
        for (var i = 0; i < this.Clients.length; i++)
        {
            var client = this.Clients[i];
            if (typeof exclude === 'undefined' || exclude.indexOf(client) === -1)
            {
                client.send(data);
            }
        }
    }

    var accept_client = function(client)
    {
        self.Clients.push(client);

        client.info = new ClientInfo(client);
        client.info.ID = ID_autoincrement++;

        console.log('Client ' + client.info.ID + ' joined!');
        client.on('message', accept_message);
        client.on('close', close_client);

        client.info.Stage = ClientStage.Verifying;
    }

    var accept_message = function(data)
    {
        var client = this;
        console.log(client.info.ID + ' sent: ' + data);

        var parsed_data = {};
        try
        {
            parsed_data = JSON.parse(data);
        }
        catch (e)
        {
            console.log('ERROR: Client sent broken JSON.');
            client.close(ErrorCode.BrokenJSON)
            return;
        }

        switch (client.info.Stage)
        {
            case ClientStage.Verifying:
                self.Verifier.AcceptMessage(client, parsed_data);
                break;
            case ClientStage.Lobby:
                self.LobbyManager.AcceptMessage(client, parsed_data);
                break;
        }
    }

    var close_client = function()
    {
        var client = this;

        // remove client from list
        var index = self.Clients.indexOf(client);
        if (index >= 0) self.Clients.splice(index, 1);

        // broadcast new playerlist
        response = {
            lobby: self.LobbyManager.GetPlayerlist(),
        }
        self.Broadcast(JSON.stringify(response));

        // echo to the console
        console.log('Client ' + client.info.ID + ' left.');
    }
}

