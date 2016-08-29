// serverside
function LobbyManager(server)
{
    this.GetPlayerlist = function()
    {
        var result = [];

        for (var i = 0; i < server.Clients.length; i++) {
            result.push(server.Clients[i].info);
        }

        return result;
    }

    this.AcceptMessage = function(client, data)
    {
        if (data.msg)
        {
            // TODO: chat
        }
        if (data.ready)
        {
            client.info.LobbyReady = !client.info.LobbyReady;

            var response = {
                lobby: this.GetPlayerlist(),
            }

            server.Broadcast(JSON.stringify(response));
        }
        if (data.start)
        {
            // TODO: start the game
        }
    }
}