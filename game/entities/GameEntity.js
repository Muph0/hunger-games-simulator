
/**
 * @constructor
 */
function GameEntity()
{
    this.Name = "";

    this.Pos = new Vec3(0, 0, 0);
}
GameEntity.RegisterEntities = function()
{
    GameEntity.Types = [];

}