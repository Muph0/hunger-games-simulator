
function Server()
{
    var self = this;

    var server;
    var clients = [];
    var ID_autoincrement = 0;

    self.Start = function(port)
    {
        console.log('Starting WebSocketServer on port ' + port);
        server = new WebSocketServer({'port': port});
        server.on('connection', accept_client);
    }
    self.Stop = function()
    {
        console.log('Shutting down...');
        server.close();
    }

    var accept_client = function(client)
    {
        clients.push(client);

        client.myId = ID_autoincrement++;

        console.log('Client ' + client.myId + ' joined!');
        client.on('message', accept_message);
        client.on('close', close_client);
    }

    var accept_message = function(data)
    {
        var client = this;
        var ID = client.myId;
        console.log(ID + ' sent: ' + data);
    }

    var close_client = function()
    {
        var client = this;

        // remove client from list
        var index = clients.indexOf(client);
        if (index >= 0) clients.splice(index, 1);

        // echo to the console
        console.log('Client ' + client.myId + ' left.');
    }
}