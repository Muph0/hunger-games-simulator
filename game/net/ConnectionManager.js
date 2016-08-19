function ConnectionManager(game)
{
    this.Update = function(defaultState)
    {
        if (game.Client.State !== ClientState.Connected)
        {
            return game.RenderManager.connecting_menu.Show();
        }

        // handle networking
        while (game.Client.Responses.length > 0)
        {
            var resp = game.Client.Responses.splice(0, 1)[0];

            if (resp.lobby)
            {
                game.ServerInfo.AcceptPlayerList(resp.lobby);
            }
            if (resp.token && game.ServerInfo.Token === null)
            {
                game.ServerInfo.Token = resp.token;
            }
        }

        return defaultState;
    }
}