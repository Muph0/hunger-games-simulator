
/**
 * @constructor
 */
function ConnectionManager(game)
{
    this.Update = function(defaultState)
    {
        if (game.client.State !== ClientState.Connected)
        {
            return game.renderManager.connecting_menu.Show();
        }

        // handle networking
        while (game.client.Responses.length > 0)
        {
            var resp = game.client.Responses.splice(0, 1)[0];

            if (resp.lobby)
            {
                game.serverInfo.AcceptPlayerList(resp.lobby);
            }
            if (resp.token && game.serverInfo.Token === null)
            {
                game.serverInfo.Token = resp.token;
            }
            if (resp.msg)
            {
                game.serverInfo.AcceptMessage(resp.msg);
            }
            if (resp.map)
            {
                resp.map.tiles.forEach(game.serverInfo.AcceptTile);
            }
        }

        return defaultState;
    }
}