
/**
 * @constructor
 */
function ServerInfo()
{
    this.Playerlist = [];
    this.Token = null;
    this.ChatHistory = [];

    this.getPlayerByToken = function(token)
    {
        return this.Playerlist.filter(function(obj) {
            return obj.Token === token;
        })[0];
    }
    this.getLocalInfo = function() {
        if (this.Token === null) return null;
        return this.getPlayerByToken(this.Token);
    }

    this.AcceptPlayerList = function(list)
    {
        for (var i = 0; i < list.length; i++)
        {
            var final_character = new PlayerCharacter();
            final_character.Import(list[i].Character);

            list[i].Character = final_character;
        }

        this.Playerlist = list;
    }
    this.AcceptMessage = function(msg)
    {
        msg.time = new Date().getTime();
        this.ChatHistory.push(msg);
    }
}