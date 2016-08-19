
function ClientInfo(client)
{
    this.ID = 0;
    this.Stage = 0;
    this.Character = null;
    this.Token = null;
    this.LobbyReady = false;
}
ClientInfo.RandomToken = function()
{
    var result = "";

    for (var i = 0; i < 11; i++)
    {
        result += (Math.floor(Math.random() * 36)).toString(36);
    }

    return result + new Date().getTime().toString(36) + '==';
}

ClientStage = {
    Verifying: 1,
    Lobby: 2,
};