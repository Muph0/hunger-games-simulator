
function ClientVerifier(server)
{
    this.AcceptMessage = function(client, data)
    {
        if (data.protocol !== server.PROTOCOL_VERSION)
        {
            debugger;
            console.log('ERROR: InvalidVersion.');
            client.close(ErrorCode.InvalidVersion);
            return;
        }

        if (data.character)
        {
            client.info.Character = new PlayerCharacter();
            try {
                client.info.Character.Import(data.character);

                // TODO: check free points >= 0

            } catch (e) {
                console.log('ERROR: ProtocolFailure.');
                client.close(ErrorCode.ProtocolFailure);
                return;
            }
        }

        // Verified

        client.info.Stage = ClientStage.Lobby;
        client.info.Token = ClientInfo.RandomToken();

        console.log("Client " + client.info.ID + " verified. Assigning token " + client.info.Token);

        var response = {
            token: client.info.Token,
            lobby: server.LobbyManager.GetPlayerlist(),
        };

        client.send(JSON.stringify(response));

        // send new playerlist to others
        response = {
            lobby: server.LobbyManager.GetPlayerlist(),
        }
        server.Broadcast(JSON.stringify(response), [client]);
    }
}