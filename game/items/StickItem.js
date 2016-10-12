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

if (typeof server !== 'undefined')
{
    StickItem.Generator = ItemGenerator.populateTiles(StickItem, [ForestBiome], 10, 100);
}

