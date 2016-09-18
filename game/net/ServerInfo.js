
/**
 * @constructor
 */
function ServerInfo()
{
    this.Playerlist = [];
    this.Token = null;

    this.getLocalInfo = function() {
        if (this.Token === null) return null;
        var token = this.Token;
        return this.Playerlist.filter(function(obj) {
            return obj.Token === token;
        });
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
}