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

    }
}