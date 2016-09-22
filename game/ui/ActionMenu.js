
/**
 * @constructor
 */
function ActionMenu(Console, game)
{
	var menu = new Menu(Console, game, 50);
	menu.Itemlist = [new StringMenuItem(game, ">")];

	// chat window
    var img = document.getElementById('ascii');
    var text_buffer = new CanvasConsole(69, 6, img);
    text_buffer.CreateCanvas(null);
    text_buffer.Clear();
 	

 	this.Show = function()
 	{
 		input.OnSelect();
        return DrawState.ActionMenu;
 	}	

 	this.Update = function()
 	{
		var a = new Vec3(0,0,0);


 	}

 	this.Draw = function()
 	{
 	    Console.setCursor(6, 3);
        Console.WriteImage(text_buffer.getCanvas());

 		Console.setCursor(6, 22);
 		menu.Draw();
 	}
}