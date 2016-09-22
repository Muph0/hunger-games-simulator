/**
 * @constructor
 */
function StickItem()
{
    inherit(this, new Item());
    inherit(this, new Weapon());

    this.Name = "Stick";
    this.Data;
}
StickItem.Generator = null;
