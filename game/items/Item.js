
/**
 * @constructor
 */
function Item()
{
    this.Name = "";
    this.Data = [];

}

Item.RegisterItems = function()
{
    Item.Types = [];

    Item.Types.push(StickItem);
}