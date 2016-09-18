
function ChatManager(server)
{
    //this.History = [];

    this.AcceptMessage = function(client, msg)
    {
        // add received message to chat history
        /*
        this.History.push({
            time: new Date().getTime(),
            text: msg.text,
            client_token: client.info.Token,
        });
        */
        // broadcast it to all clients

        var response = {
            msg: {
                text: msg.text,
                token: client.info.Token,
            },
        };

        server.Broadcast(JSON.stringify(response));
    }
}